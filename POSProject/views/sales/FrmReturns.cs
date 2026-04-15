using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSProject.repositories.notifications;
using POSProject.repositories.returns;
using POSProject.services;
using POSProject.services.notifications;
using POSProject.services.returns;

namespace POSProject
{
    public partial class FrmReturns : Form
    {

        private readonly IReturnService _returnService;
        private DataTable _saleLinesTable;
        private int _currentSaleId = 0;
        private AutoCompleteStringCollection invoiceList = new AutoCompleteStringCollection();
        private readonly INotificationService _notifsService;

        public FrmReturns() : this(
            new ReturnService(new ReturnRepository()))
        {
            
        }

        public FrmReturns(IReturnService returnService)
        {
            InitializeComponent();
            INotificationRepository notifsRepo = new NotificationRepository();
            _notifsService = new NotificationService(notifsRepo);
            _returnService = returnService;
            InitializeForm();
        }

        private void InitializeForm()
        {
            ConfigureGrid();
            LoadRefundMethods();
            ResetFormState();

            btnSearchSale.Click += btnSearchSale_Click;
            btnSaveReturn.Click += btnSaveReturn_Click;
            comboBoxRefundMethod.SelectedIndexChanged += comboBoxRefundMethod_SelectedIndexChanged;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            txtBoxInvoiceNumber.KeyDown += txtBoxInvoiceNumber_KeyDown;

            txtBoxRefundAmount.ReadOnly = true;
            txtBoxReason.Multiline = true;
            txtBoxReason.Height = 60;

            txtBoxInvoiceNumber.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBoxInvoiceNumber.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBoxInvoiceNumber.TextChanged += txtBoxInvoiceNumber_TextChanged;
            LoadInvoiceAutoComplete();
        }
        private void btnSearchSale_Click(object sender, EventArgs e)
        {
            SearchSale();
        }

        private void btnSaveReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if(_currentSaleId <= 0)
                {
                    ShowInfo("Ngarko fillimisht një shitje.");
                    return;
                }

                if(comboBoxRefundMethod.SelectedIndex == -1)
                {
                    ShowInfo("Zgjedh mënyrën e rimbursimit.");
                    return;
                }

                var selectedRows = _saleLinesTable.AsEnumerable().Where(r => ParseDecimal(r["SasiaPerKthim"]) > 0).ToList();

                if(selectedRows.Count == 0)
                {
                    ShowInfo("Zgjidh të paktën një artikull për kthim.");
                    return;
                }

                decimal totalReturn = selectedRows.Sum(r => ParseDecimal(r["VleraKthimit"]));
                if(totalReturn <= 0)
                {
                    ShowInfo("Totali i kthimit duhet të jetë më i madh se zero.");
                    return;
                }

                ReturnModel model = new ReturnModel
                {
                    ShitjaId = _currentSaleId,
                    NumriKthimit = "KTH-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    TotaliKthimit = totalReturn,
                    Arsyeja = txtBoxReason.Text.Trim(),
                    PerdoruesiId = Session.UserId
                };

                foreach(var row in selectedRows)
                {
                    model.Details.Add(new ReturnDetailModel
                    {
                        ShitjaDetaliId = Convert.ToInt32(row["ShitjaDetaliId"]),
                        ArtikulliId = Convert.ToInt32(row["ArtikulliId"]),
                        Sasia = ParseDecimal(row["SasiaPerKthim"]),
                        Cmimi = ParseDecimal(row["Cmimi"]),
                        Vlera = ParseDecimal(row["VleraKthimit"])
                    });
                }

                model.RefundPayments.Add(new RefundPaymentModel
                {
                    MenyraPagesesId = Convert.ToInt32(comboBoxRefundMethod.SelectedValue),
                    Shuma = totalReturn,
                    ReferenceNr = txtBoxReferenceNr.Text.Trim(),
                    PerdoruesiId = Session.UserId
                });

                int returnId = _returnService.SaveReturn(model);
                AutoClosingMessageBox.Show($"Kthimi u ruajt me sukses. ID = {returnId}", "Info", 900);
                _notifsService.Create("RETURN_SUCCESS", "Info", "Kthimi u ruajt me sukses", $"Kthimi me id:{returnId} u ruajt me sukses", nameof(model), returnId, Session.UserId);
                ResetFormState();

            }catch(Exception ex)
            {
                MessageBox.Show("Gabim gjatë ruajtjes së kthimit:" + ex.Message);
                _notifsService.Create("RETURN_FAIL", "Warning", "Dështoi ruajtja e kthimit", $"Kthimi nuk u ruajt", null, Session.UserId);
            }
        }

        private decimal ParseDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0m;

            decimal.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result);

            if (result == 0m)
                decimal.TryParse(value.ToString(), out result);

            return result;
        }
        private void comboBoxRefundMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleReferenceField();
        }

        private void ToggleReferenceField()
        {
            bool needsReference = false;
            if(comboBoxRefundMethod.SelectedItem is DataRowView drv)
            {
                string tipi = drv["Tipi"]?.ToString()?.ToUpper() ?? "";
                needsReference = tipi != "CASH";
                if (!tipi.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase) && needsReference)
                    txtBoxReferenceNr.Text = $"RF-{DateTime.Now:yyyyMMddHHmmss}";
                else
                    txtBoxReferenceNr.Clear();
            }
            labelReferenceNr.Visible = needsReference;
            txtBoxReferenceNr.Visible = needsReference;

            if (!needsReference)
                txtBoxReferenceNr.Clear();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dataGridView1.Columns[e.ColumnIndex].Name != "SasiaPerKthim")
                return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            decimal sasiaLejuar = ParseDecimal(row.Cells["SasiaLejuar"].Value);
            decimal cmimi = ParseDecimal(row.Cells["Cmimi"].Value);
            decimal sasiaPerKthim = ParseDecimal(row.Cells["SasiaPerKthim"].Value);

            if (sasiaPerKthim < 0)
                sasiaPerKthim = 0;

            if (sasiaPerKthim > sasiaLejuar)
            {
                MessageBox.Show("Sasia për kthim nuk mund të jetë më e madhe se sasia e lejuar.");
                sasiaPerKthim = sasiaLejuar;
            }

            row.Cells["SasiaPerKthim"].Value = sasiaPerKthim;
            row.Cells["VleraKthimit"].Value = sasiaPerKthim * cmimi;

            RecalculateReturnTotal();
        }

        private void RecalculateReturnTotal()
        {
            decimal total = 0m;
            if(_saleLinesTable != null)
            {
                foreach (DataRow row in _saleLinesTable.Rows)
                    total += ParseDecimal(row["VleraKthimit"]);
            }

            labelTotalReturnCaption.Text = "Totali i kthimit:";
            labelTotalReturn.Text = $"{total:0.00} €";
            txtBoxRefundAmount.Text = total.ToString("0.00");
        }
        private void txtBoxInvoiceNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SearchSale();
                e.SuppressKeyPress = true;
            }
        }
        private void ConfigureGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ShitjaDetaliId",
                DataPropertyName = "ShitjaDetaliId",
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ArtikulliId",
                DataPropertyName = "ArtikulliId",
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Barkodi",
                HeaderText = "Barkodi",
                DataPropertyName = "Barkodi",
                Width = 120,
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Produkti",
                HeaderText = "Produkti",
                DataPropertyName = "Emri",
                Width = 180,
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SasiaShitur",
                HeaderText = "Shitur",
                DataPropertyName = "SasiaShitur",
                Width = 70,
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SasiaKthyer",
                HeaderText = "Kthyer",
                DataPropertyName = "SasiaKthyer",
                Width = 70,
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SasiaLejuar",
                HeaderText = "Lejohet",
                DataPropertyName = "SasiaLejuarPerKthim",
                Width = 80,
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cmimi",
                HeaderText = "Cmimi",
                DataPropertyName = "Cmimi",
                Width = 85,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00"}
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SasiaPerKthim",
                HeaderText = "Kthim",
                DataPropertyName = "SasiaPerKthim",
                Width = 75
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "VleraKthimit",
                HeaderText = "Vlera",
                DataPropertyName = "VleraKthimit",
                Width = 95,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
            });
        }

        private void LoadRefundMethods()
        {
            DataTable dt = _returnService.GetRefundMethods();
            comboBoxRefundMethod.DisplayMember = "Pershkrimi";
            comboBoxRefundMethod.ValueMember = "Id";
            comboBoxRefundMethod.DataSource = dt;
            comboBoxRefundMethod.SelectedIndex = -1;
        }

        private void ResetFormState()
        {
            _currentSaleId = 0;
            _saleLinesTable = null;

            txtBoxInvoiceNumber.Clear();
            txtBoxReason.Clear();
            txtBoxRefundAmount.Text = "0.00";
            txtBoxReferenceNr.Clear();

            labelSaleNumber.Text = "Numri i shitjes:";
            labelSaleDate.Text = "Data e shitjes:";
            labelCashier.Text = "Arkëtari:";
            labelOriginalTotal.Text = "Totali origjinal:";
            labelTotalReturnCaption.Text = "Totali i kthimit:";
            labelTotalReturn.Text = "0.00";
            labelInfo.Text = "";

            dataGridView1.DataSource = null;
            labelReferenceNr.Visible = false;
            txtBoxReferenceNr.Visible = false;
            btnSaveReturn.Enabled = false;
        }

        private void ResetLoadedSaleOnly()
        {
            _currentSaleId = 0;
            _saleLinesTable = null;

            dataGridView1.DataSource = null;

            labelSaleNumber.Text = "Numri i shitjes:";
            labelSaleDate.Text = "Data e shitjes:";
            labelCashier.Text = "Arkëtari:";
            labelOriginalTotal.Text = "Totali origjinal:";
            labelTotalReturn.Text = "0.00";
            txtBoxRefundAmount.Text = "0.00";
            btnSaveReturn.Enabled = false;
        }

        private void SearchSale()
        {
            string nrFatures = txtBoxInvoiceNumber.Text.Trim();
            if (string.IsNullOrEmpty(nrFatures))
            {
                ShowInfo("Shkruaj numrin e faturës.");
                return;
            }

            try
            {
                DataRow saleHeader = _returnService.GetSaleHeaderByInvoiceNumber(nrFatures);
                if(saleHeader == null)
                {
                    ResetLoadedSaleOnly();
                    ShowInfo("Fatura nuk u gjet.");
                    return;
                }
                _currentSaleId = Convert.ToInt32(saleHeader["Id"]);
                labelSaleNumber.Text = $"Numri i shitjes: {saleHeader["NrFatures"]}";
                labelSaleDate.Text = $"Data e shitjes: {Convert.ToDateTime(saleHeader["DataShitjes"]):dd/MM/yyyy HH:mm}";
                labelCashier.Text = $"Arkëtari: {saleHeader["CashierName"]}";
                labelOriginalTotal.Text = $"Totali origjinal: {Convert.ToDecimal(saleHeader["Totali"]):0.00} €";

                _saleLinesTable = _returnService.GetSaleLinesForReturn(_currentSaleId);
                foreach (DataRow row in _saleLinesTable.Rows)
                {
                    row["SasiaPerKthim"] = 0m;
                    row["VleraKthimit"] = 0m;
                }
                dataGridView1.DataSource = _saleLinesTable;
                RecalculateReturnTotal();
                btnSaveReturn.Enabled = true;
                ShowInfo("Shitja u ngarkua me sukses");
            }catch(Exception ex)
            {
                ShowInfo("Gabim gjatë kërkimit:" + ex.Message);
            }
        }

        private void ShowInfo(string message)
        {
            labelInfo.Text = message;
        }

        private void LoadInvoiceAutoComplete()
        {
            invoiceList.Clear();

            using(var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ""NrFatures""
                                 FROM ""Shitjet""
                                 ORDER BY ""DataShitjes"" DESC
                                 LIMIT 200;";
                using(var cmd = new Npgsql.NpgsqlCommand(query,conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoiceList.Add(reader["NrFatures"].ToString());
                    }
                }
            }
            txtBoxInvoiceNumber.AutoCompleteCustomSource = invoiceList;
        }

        private void txtBoxInvoiceNumber_TextChanged(object sender, EventArgs e)
        {
            string text = txtBoxInvoiceNumber.Text.Trim();
            bool exactMatch = invoiceList.Cast<string>().Any(x => x.Equals(text, StringComparison.OrdinalIgnoreCase));
            if (exactMatch)
            {
                SearchSale();
            }
        }
    }
}
