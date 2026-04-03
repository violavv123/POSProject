using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace POSProject
{
    public partial class FrmPagesa : Form
    {
        private decimal _totali;
        private string _komenti;
        private decimal _paguarFillestar;
        private DataTable menyraTable = new DataTable();
        private DataTable menyraTablePrimary = new DataTable();
        private DataTable menyraTableSecondary = new DataTable();


        public List<PaymentExecutionModel> PaymentResults { get; private set; } = new List<PaymentExecutionModel>();
        public bool PaymentConfirmed { get; private set; } = false;
        private int? selectedMenyraPagesesId = null;
        private bool kerkonReference = false;
        private string valutaDefault = "EUR";
        private string tipiPageses = "";
        private List<InvoiceItem> _items;
        private int? selectedMenyraPagesesId2 = null;
        private bool kerkonReference2 = false;
        private string valutaDefault2 = "EUR";
        private string tipiPageses2 = "";
        private bool splitPaymentEnabled = false;
        private int collapsedWidth = 785;
        private int expandedWidth = 1310;

        private decimal kursiKembimit2 = 0;
        private decimal kursiKembimit1 = 1;

        private bool isUpdatingSplitFields = false;

        private decimal _totaliPaZbritje;
        private decimal _zbritjaTotale;
        private enum SplitInputMode
        {
            None,
            FromPaguar,
            FromOtherCurrency
        }
        private SplitInputMode splitInputMode = SplitInputMode.None;
        private bool suppressPrimaryFocus = false;

        public FrmPagesa(decimal totaliFinal, decimal paguarFillestar, string komenti = "", List<InvoiceItem> items = null, decimal totaliPaZbritje = 0m, decimal zbritjaTotale = 0m)
        {
            InitializeComponent();
            _totaliPaZbritje = totaliPaZbritje;
            _zbritjaTotale = zbritjaTotale;
            _totali = totaliFinal;
            _komenti = komenti;
            _paguarFillestar = paguarFillestar;
            _items = items ?? new List<InvoiceItem>();

            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            SetupPaymentGrid();
            LoadSplitPaymentControls();

            txtBoxPaguar.Text = paguarFillestar.ToString("0.00");
            if (!suppressPrimaryFocus)
            {
                txtBoxPaguar.Focus();
                txtBoxPaguar.SelectAll();
            }
            labelMenyra.Text = "Zgjedh mënyrën e pagesës.";
            this.AcceptButton = btnOK;
            txtBoxPaguar.TextChanged += txtBoxPaguar_TextChanged;

            btnSplitPayment.Click += btnSplitPayment_Click;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            this.Width = collapsedWidth;

            txtBoxShuma1.Text = _totali.ToString("0.00");
            txtBoxShuma1.ReadOnly = true;
            txtBoxShuma2.ReadOnly = true;
            txtBoxShuma2Tjeter.ReadOnly = true;
            txtBoxShuma2.Text = "0.00";
            txtBoxShuma2Tjeter.TextChanged += txtBoxShuma2Tjeter_TextChanged;

            labelMenyra2.Visible = false;
            comboBox1.Visible = false;
            labelShuma1.Visible = false;
            txtBoxShuma1.Visible = false;
            labelShuma2.Visible = false;
            txtBoxShuma2.Visible = false;
            labelNrReference2.Visible = false;
            txtBoxNrReference2.Visible = false;
            btnSplitPayment.Visible = true;
        }

        private void FrmPagesa_Load(object sender, EventArgs e)
        {
            LoadMenyraPageses();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            if (dataGridView1.CurrentRow.Index < 0) return;
            if (dataGridView1.CurrentRow.DataBoundItem == null) return;

            SelectPaymentMethod(dataGridView1.CurrentRow);
        }
        private void SetupPaymentGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Rendorja",
                HeaderText = "Nr.",
                DataPropertyName = "Rendorja",
                Width = 60
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Shkurtesa",
                HeaderText = "Shkurt.",
                DataPropertyName = "Shkurtesa",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pershkrimi",
                HeaderText = "Pershkrimi",
                DataPropertyName = "Pershkrimi",
                Width = 180
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValutaDefault",
                HeaderText = "Valuta",
                DataPropertyName = "ValutaDefault",
                Width = 65
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipi",
                HeaderText = "Tipi.",
                DataPropertyName = "Tipi",
                Width = 100
            });
            dataGridView1.Columns["Id"].Visible = false;

        }

        private void LoadMenyraPageses()
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ""Id"",""Pershkrimi"",""Shkurtesa"",""Tipi"",""ValutaDefault"",""KerkonReference"",""Rendorja""
                                FROM ""MenyratPageses""
                                WHERE ""Aktiv"" = TRUE
                                ORDER BY COALESCE (""Rendorja"", 999),""Pershkrimi"";";
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                using (var da = new Npgsql.NpgsqlDataAdapter(cmd))
                {
                    menyraTable = new DataTable();
                    da.Fill(menyraTable);
                }
            }
            ApplyPaymentSources();
        }

        private void BuildPaymentSources()
        {
            menyraTablePrimary = menyraTable.Clone();
            menyraTableSecondary = menyraTable.Clone();
            foreach(DataRow row in menyraTable.Rows)
            {
                menyraTablePrimary.ImportRow(row);
                menyraTableSecondary.ImportRow(row);
            }
        }

        private void ApplyPaymentSources()
        {
            BuildPaymentSources();
            if (splitPaymentEnabled)
            {
                dataGridView1.DataSource = menyraTablePrimary;
                comboBox1.DataSource = null;
                comboBox1.DataSource = menyraTableSecondary;
                comboBox1.DisplayMember = "Pershkrimi";
                comboBox1.ValueMember = "Id";
                comboBox1.SelectedIndex = -1;
            }
            else
            {
                dataGridView1.DataSource = menyraTable;
                comboBox1.DataSource = null;
                comboBox1.Items.Clear();
            }

            if(dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["Shkurtesa"];
                SelectPaymentMethod(dataGridView1.Rows[0]);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            SelectPaymentMethod(row);
        }

        private void SelectPaymentMethod(DataGridViewRow row)
        {
            DataRowView drv = row.DataBoundItem as DataRowView;
            if (drv == null) return;

            selectedMenyraPagesesId = Convert.ToInt32(drv["Id"]);
            string pershkrimi = drv["Pershkrimi"]?.ToString() ?? "";
            string shkurtesa = drv["Shkurtesa"]?.ToString() ?? "";
            tipiPageses = drv["Tipi"]?.ToString() ?? "";
            valutaDefault = drv["ValutaDefault"]?.ToString() ?? "EUR";
            kursiKembimit1 = MerrKursinPerValuten(valutaDefault);

            kerkonReference = drv["KerkonReference"] != DBNull.Value
                ? Convert.ToBoolean(drv["KerkonReference"])
                : false;

            labelMenyra.Text = pershkrimi;
            btnValuta.Text = valutaDefault;
            if (!tipiPageses.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase) && kerkonReference)
                txtBoxNrReference.Text = $"RF-{DateTime.Now:yyyyMMddHHmmss}";
            else
                txtBoxNrReference.Clear();

            UpdateCashFieldsVisibility();
        }

        private void AppendToPaguar(string value)
        {
            if (txtBoxPaguar.ReadOnly)
                return;
            if (txtBoxPaguar.Text == "0.00")
                txtBoxPaguar.Clear();
            txtBoxPaguar.Text += value;
            txtBoxPaguar.SelectionStart = txtBoxPaguar.Text.Length;
        }

        private void btnNr1_Click(object sender, EventArgs e) => AppendToPaguar("1");
        private void btnNr2_Click(object sender, EventArgs e) => AppendToPaguar("2");
        private void btnNr3_Click(object sender, EventArgs e) => AppendToPaguar("3");
        private void btnNr4_Click(object sender, EventArgs e) => AppendToPaguar("4");
        private void btnNr5_Click(object sender, EventArgs e) => AppendToPaguar("5");
        private void btnNr6_Click(object sender, EventArgs e) => AppendToPaguar("6");
        private void btnNr7_Click(object sender, EventArgs e) => AppendToPaguar("7");
        private void btnNr8_Click(object sender, EventArgs e) => AppendToPaguar("8");
        private void btnNr9_Click(object sender, EventArgs e) => AppendToPaguar("9");
        private void btnNr0_Click(object sender, EventArgs e) => AppendToPaguar("0");

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (!txtBoxPaguar.Text.Contains("."))
                AppendToPaguar(".");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxPaguar.Text))
                txtBoxPaguar.Text = txtBoxPaguar.Text.Substring(0, txtBoxPaguar.Text.Length - 1);
            if (string.IsNullOrWhiteSpace(txtBoxPaguar.Text))
                txtBoxPaguar.Text = "0";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxPaguar.Text = "";
            txtBoxPaguar.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExact_Click(object sender, EventArgs e)
        {
            if (!txtBoxPaguar.Text.Contains("."))
                txtBoxPaguar.Text += ".00";
            txtBoxPaguar.SelectionStart = txtBoxPaguar.Text.Length;
            txtBoxPaguar.Focus();

        }

        private decimal ParseDecimal(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;
            text = text.Trim();
            if (decimal.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal value))
                return value;
            if (decimal.TryParse(text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out value))
                return value;
            if (decimal.TryParse(text.Replace(".", ","), System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("de-DE"), out value))
                return value;

            return 0;
        }

        private bool ValidatePayment()
        {

            if (splitPaymentEnabled)
            {
                if (!selectedMenyraPagesesId2.HasValue)
                {
                    AutoClosingMessageBox.Show("Zgjedh mënyrën e dytë të pagesës.", "Informacion", 900);
                    comboBox1.Focus();
                    return false;
                }
                if(selectedMenyraPagesesId == selectedMenyraPagesesId2)
                {
                    AutoClosingMessageBox.Show("Mënyra 1 dhe mënyra 2 nuk mund të jenë të njëjta.", "Informacion", 900);
                    comboBox1.Focus();
                    return false;
                }
                decimal shuma2Eur = ParseDecimal(txtBoxShuma2.Text);
                decimal shuma1 = ParseDecimal(txtBoxShuma1.Text);
                if(shuma1 <= 0)
                {
                    AutoClosingMessageBox.Show("Shuma 1 duhet të jetë më e madhe se zero.", "Informacion", 900);
                    return false;
                }
                if(shuma2Eur <= 0)
                {
                    AutoClosingMessageBox.Show("Shuma 2 duhet të jetë më e madhe se zero.", "Informacion", 900);
                    return false;
                }
                if(Math.Round(shuma1 + shuma2Eur, 2) != Math.Round(_totali, 2))
                {
                    AutoClosingMessageBox.Show("Shuma 1 + Shuma 2 duhet të jetë e barabartë me totalin.", "Informacion", 900);
                    return false;
                }
                bool isCash1 = tipiPageses.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase);
                bool isCash2 = tipiPageses2.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase);
                decimal paguar1 = ParseDecimal(txtBoxPaguar.Text);
                decimal paguar2 = ParseDecimal(txtBoxShuma2Tjeter.Text);
                if(isCash1 && paguar1 < shuma1)
                {
                    AutoClosingMessageBox.Show("Për CASH 1, vlera e paguar duhet të jetë së paku sa shuma 1", "Informacion", 900);
                    txtBoxPaguar.Focus();
                    return false;
                }
                if(!isCash1 && paguar1 != shuma1)
                {
                    AutoClosingMessageBox.Show("Për metodën 1 jo-cash, Paguar duhet të jetë e barabartë me shuma 1", "Informacion", 900);
                    txtBoxPaguar.Focus();
                    return false;
                }
                if(isCash2 && paguar2 < GetSecondAmountInMethodCurrency())
                {
                    AutoClosingMessageBox.Show("Për CASH 2, vlera e paguar duhet të jetë të paktën sa shuma 2", "Informacion", 900);
                    txtBoxShuma2Tjeter.Focus();
                    return false;
                }
                if(!isCash2 && paguar2 != GetSecondAmountInMethodCurrency())
                {
                    AutoClosingMessageBox.Show("Për metodën 2 jo-cash, vlera duhet të jetë e barabartë me shuma 2", "Informacion", 900);
                    txtBoxShuma2Tjeter.Focus();
                    return false;
                }
                if(!isCash1 && kerkonReference && string.IsNullOrWhiteSpace(txtBoxNrReference.Text))
                {
                    AutoClosingMessageBox.Show("Mënyra 1 kërkon numër reference.", "Informacion", 900);
                    txtBoxNrReference.Focus();
                    return false;
                }
                if(!isCash2 && kerkonReference2 && string.IsNullOrWhiteSpace(txtBoxNrReference2.Text))
                {
                    AutoClosingMessageBox.Show("Mënyra 2 kërkon numër reference.", "Informacion", 900);
                    txtBoxNrReference2.Focus();
                    return false;
                }
            }
            return true;
        }

        private decimal GetSecondAmountInMethodCurrency()
        {
            decimal shuma2Eur = ParseDecimal(txtBoxShuma2.Text);
            if(string.IsNullOrWhiteSpace(valutaDefault2) || valutaDefault2.Trim().Equals("EUR", StringComparison.OrdinalIgnoreCase))
            {
                return shuma2Eur;
            }
            if (kursiKembimit2 <= 0)
                return 0;
            return Math.Round(shuma2Eur / kursiKembimit2, 2);
        }

        private decimal GetMethod1AmountInOwnCurrency()
        {
            decimal shuma1Eur = ParseDecimal(txtBoxShuma1.Text);
            if (string.IsNullOrWhiteSpace(valutaDefault) || valutaDefault.Trim().Equals("EUR", StringComparison.OrdinalIgnoreCase))
                return shuma1Eur;
            if (kursiKembimit1 <= 0)
                return 0;
            return Math.Round(shuma1Eur / kursiKembimit1, 2);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidatePayment())
                return;
            bool isCash1 = tipiPageses.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase);
            bool isCash2 = tipiPageses2.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase);

            decimal kusuri1 = 0m;
            decimal kusuri2 = 0m;

            if (isCash1)
            {
                decimal shuma1PerKrahasim = splitPaymentEnabled ? ParseDecimal(txtBoxShuma1.Text) : _totali;
                if (!string.IsNullOrWhiteSpace(valutaDefault) && !valutaDefault.Trim().Equals("EUR", StringComparison.OrdinalIgnoreCase))
                {
                    if (kursiKembimit1 > 0)
                        shuma1PerKrahasim = shuma1PerKrahasim / kursiKembimit1;
                    else
                        shuma1PerKrahasim = 0;
                }

                decimal paguar1Kontroll = ParseDecimal(txtBoxPaguar.Text);
                if (paguar1Kontroll > shuma1PerKrahasim)
                    kusuri1 = paguar1Kontroll - shuma1PerKrahasim;
            }

            if(splitPaymentEnabled && isCash2)
            {
                decimal shuma2PerKrahasim = GetSecondAmountInMethodCurrency();
                decimal paguar2Kontroll = ParseDecimal(txtBoxShuma2Tjeter.Text);

                if (paguar2Kontroll > shuma2PerKrahasim)
                    kusuri2 = paguar2Kontroll - shuma2PerKrahasim;
            }

            decimal kusuriTotal = kusuri1 + kusuri2;

            if (kusuriTotal > 0)
            {
                var shiftService = new ShiftService();
                var activeShift = shiftService.GetOpenShift();

                if (activeShift == null)
                {
                    AutoClosingMessageBox.Show("Nuk ka ndërrim aktiv.", "Info", 900);
                    return;
                }

                decimal availableCash = shiftService.GetAvailableCash(activeShift.Id);

                if (availableCash < kusuriTotal)
                {
                    AutoClosingMessageBox.Show(
                        $"Nuk ka mjaftueshëm cash në arkë për ta kthyer kusurin." +
                        $"\nKusuri: {kusuriTotal:0.00} €" +
                        $"\nGjendja aktuale në arkë: {availableCash:0.00} €" +
                        "\nBëj CASH IN.",
                        "Info",
                        1700);
                    return;
                }
            }

            PaymentResults.Clear();
            string pershkrimi1 = "";
            string shkurtesa1 = "";

            if(dataGridView1.CurrentRow != null)
            {
                pershkrimi1 = dataGridView1.CurrentRow.Cells["Pershkrimi"].Value?.ToString() ?? "";
                shkurtesa1 = dataGridView1.CurrentRow.Cells["Shkurtesa"].Value?.ToString() ?? "";
            }

            decimal shuma1 = splitPaymentEnabled ? ParseDecimal(txtBoxShuma1.Text) : _totali;
            decimal paguar1 = isCash1 ? ParseDecimal(txtBoxPaguar.Text) : GetMethod1AmountInOwnCurrency();
            decimal cashBack1 = 0m;

            if (isCash1)
            {
                decimal shuma1NeValutenEMetodes1 = shuma1;
                if(!string.IsNullOrWhiteSpace(valutaDefault) && !valutaDefault.Trim().Equals("EUR", StringComparison.OrdinalIgnoreCase))
                {
                    if (kursiKembimit1 > 0)
                        shuma1NeValutenEMetodes1 = shuma1 / kursiKembimit1;
                    else
                        shuma1NeValutenEMetodes1 = 0;
                }

                if (paguar1 > shuma1NeValutenEMetodes1)
                    cashBack1 = paguar1 - shuma1NeValutenEMetodes1;
            }

            PaymentResults.Add(new PaymentExecutionModel
            {
                MenyraPagesesId = selectedMenyraPagesesId.Value,
                Pershkrimi = pershkrimi1,
                Shkurtesa = shkurtesa1,
                Tipi = tipiPageses,
                ShumaPaguar = shuma1,
                PaguarMe = paguar1,
                CashBack = cashBack1,
                Valuta = valutaDefault,
                KursiKembimit = kursiKembimit1 > 0 ? kursiKembimit1 : 0,
                ShumaNeValuteBaze = shuma1,
                ReferenceNr = string.IsNullOrWhiteSpace(txtBoxNrReference.Text) ? null : txtBoxNrReference.Text.Trim(),
                Statusi = "Kompletuar",
                Koment = _komenti,
                PerdoruesiId = Session.UserId
            });

            if (splitPaymentEnabled)
            {
                DataRowView drv2 = comboBox1.SelectedItem as DataRowView;
                string pershkrimi2 = drv2?["Pershkrimi"]?.ToString() ?? "";
                string shkurtesa2 = drv2?["Shkurtesa"]?.ToString() ?? "";

                decimal shuma2Eur = ParseDecimal(txtBoxShuma2.Text);
                decimal paguar2 = ParseDecimal(txtBoxShuma2Tjeter.Text);
                decimal cashBack2 = 0m;

                if (isCash2)
                {
                    decimal shuma2NeValutenEMetodes2 = GetSecondAmountInMethodCurrency();
                    if (paguar2 > shuma2NeValutenEMetodes2)
                        cashBack2 = paguar2 - shuma2NeValutenEMetodes2;
                }

                PaymentResults.Add(new PaymentExecutionModel
                {
                    MenyraPagesesId = selectedMenyraPagesesId2.Value,
                    Pershkrimi = pershkrimi2,
                    Shkurtesa = shkurtesa2,
                    Tipi = tipiPageses2,
                    ShumaPaguar = shuma2Eur,
                    PaguarMe = paguar2,
                    CashBack = cashBack2,
                    Valuta = valutaDefault2,
                    KursiKembimit = kursiKembimit2 > 0 ? kursiKembimit2 : 1,
                    ShumaNeValuteBaze = shuma2Eur,
                    ReferenceNr = string.IsNullOrWhiteSpace(txtBoxNrReference2.Text) ? null : txtBoxNrReference2.Text.Trim(),
                    Statusi = "Kompletuar",
                    Koment = _komenti,
                    PerdoruesiId = Session.UserId
                });
            }

            this.Hide();

            using(FrmFaturimi fatura = new FrmFaturimi(_items, _komenti, _totali, _totaliPaZbritje, _zbritjaTotale, PaymentResults))
            {
                fatura.ShowDialog();
                if (fatura.InvoiceSaved)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
            }

            this.Show();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnOK.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                btnCancel.PerformClick();
                return true;
            }
            if (keyData >= Keys.D1 && keyData <= Keys.D9)
            {
                if (dataGridView1.Focused)
                {
                    int index = keyData - Keys.D1;
                    if (index < dataGridView1.Rows.Count)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[index].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells["Shkurtesa"];
                        SelectPaymentMethod(dataGridView1.Rows[index]);
                        return true;
                    }
                }
            }
            if(keyData >= Keys.NumPad1 && keyData <= Keys.NumPad9)
            {
                if (dataGridView1.Focused)
                {
                    int index = keyData - Keys.NumPad1;
                    if(index < dataGridView1.Rows.Count)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[index].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells["Shkurtesa"];
                        SelectPaymentMethod(dataGridView1.Rows[index]);
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UpdateCashFieldsVisibility()
        {
            bool isCash = tipiPageses.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase);
            bool isPOS = tipiPageses.Trim().Equals("POS", StringComparison.OrdinalIgnoreCase);

            panelKusuri.Visible = isCash;
            labelPaguar.Visible = true;
            txtBoxPaguar.Visible = true;

            if (isCash)
            {
                txtBoxPaguar.ReadOnly = false;

                if (ParseDecimal(txtBoxPaguar.Text) <= 0)
                    txtBoxPaguar.Text = _paguarFillestar > 0
                        ? _paguarFillestar.ToString("0.00")
                        : _totali.ToString("0.00");

                if (!suppressPrimaryFocus)
                {
                    txtBoxPaguar.Focus();
                    txtBoxPaguar.SelectAll();
                }
                CalculateKusuri();
                panelKusuri.BringToFront();
            }
            else if (isPOS)
            {
                txtBoxPaguar.ReadOnly = true;
                txtBoxPaguar.Text = _totali.ToString("0.00");
                txtBoxKusuri.Text = "0.00";
            }
            else
            {
                txtBoxPaguar.ReadOnly = true;
                txtBoxKusuri.Text = "0.00";
                txtBoxPaguar.Text = _totali.ToString("0.00");
            }

            bool showReference = !isCash && kerkonReference;
            txtBoxNrReference.Visible = showReference;
            labelNrReference.Visible = showReference;

            if (!showReference)
                txtBoxNrReference.Clear();
        }

        private void CalculateKusuri()
        {
            decimal paguar = ParseDecimal(txtBoxPaguar.Text);
            decimal kusuri = 0;
            decimal bazaKusurit = splitPaymentEnabled ? ParseDecimal(txtBoxShuma1.Text) : _totali;
            decimal bazaNeValutenEMetodes1 = bazaKusurit;

            if(!string.IsNullOrWhiteSpace(valutaDefault) && !valutaDefault.Trim().Equals("EUR", StringComparison.OrdinalIgnoreCase))
            {
                if (kursiKembimit1 > 0)
                    bazaNeValutenEMetodes1 = bazaKusurit / kursiKembimit1;
                else
                    bazaNeValutenEMetodes1 = 0;
            }

            if (paguar > bazaNeValutenEMetodes1)
            {
                kusuri = paguar - bazaNeValutenEMetodes1;
            }
            txtBoxKusuri.Text = kusuri.ToString("0.00");
        }

        private void txtBoxPaguar_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingSplitFields)
                return;

            if (splitPaymentEnabled)
            {
                if(splitInputMode == SplitInputMode.FromOtherCurrency)
                {
                    CalculateKusuri();
                }
                else
                {
                    splitInputMode = SplitInputMode.FromPaguar;
                    RecalculateFromPaguar();
                }
                return;
            }
            if (tipiPageses.Equals("CASH", StringComparison.OrdinalIgnoreCase))
            {
                CalculateKusuri();
            }
        }
        private void LoadSplitPaymentControls()
        {
            splitPaymentEnabled = false;

            labelMenyra2.Visible = false;
            comboBox1.Visible = false;
            labelShuma1.Visible = false;
            txtBoxShuma1.Visible = false;
            labelShuma2.Visible = false;
            txtBoxShuma2.Visible = false;
            labelNrReference2.Visible = false;
            txtBoxNrReference2.Visible = false;

            txtBoxShuma1.Text = _totali.ToString("0.00");
            txtBoxShuma2.Text = "0.00";
            txtBoxNrReference2.Clear();

            selectedMenyraPagesesId2 = null;
            kerkonReference2 = false;
            valutaDefault2 = "EUR";
            tipiPageses2 = "";

            this.Width = collapsedWidth;
        }

        private void txtBoxShuma1_TextChanged(object sender, EventArgs e)
        {
            if (!splitPaymentEnabled)
                return;
            RecalculateSplitAmounts();
        }
        private void btnSplitPayment_Click(object sender, EventArgs e)
        {
            splitPaymentEnabled = !splitPaymentEnabled;

            if (splitPaymentEnabled)
            {
                this.Width = expandedWidth;
                btnSplitPayment.Text = "Largo ndarjen";

                labelMenyra2.Visible = true;
                comboBox1.Visible = true;
                labelShuma1.Visible = true;
                txtBoxShuma1.Visible = true;
                labelShuma2.Visible = true;
                txtBoxShuma2.Visible = true;

                decimal paguarAktual = ParseDecimal(txtBoxPaguar.Text);
                if (paguarAktual <= 0)
                    paguarAktual = 0;
                txtBoxShuma1.Text = Math.Min(paguarAktual, _totali).ToString("0.00");
                txtBoxShuma2.Text = (_totali - Math.Min(paguarAktual, _totali)).ToString("0.00");

                txtBoxNrReference2.Clear();
                labelNrReference2.Visible = false;
                txtBoxNrReference2.Visible = false;

                suppressPrimaryFocus = true;
                ApplyPaymentSources();
                suppressPrimaryFocus = false;
                splitInputMode = SplitInputMode.None;
                txtBoxShuma2Tjeter.ReadOnly = false;
                this.BeginInvoke(new Action(() =>
                {
                    txtBoxShuma2Tjeter.Focus();
                    txtBoxShuma2Tjeter.SelectAll();
                }
                ));

                txtBoxPaguar.ReadOnly = false;
                CalculateKusuri();
            }
            else
            {
                LoadSplitPaymentControls();
                btnSplitPayment.Text = "Ndaj pagesën";
                ApplyPaymentSources();
                splitInputMode = SplitInputMode.None;
                txtBoxShuma2Tjeter.ReadOnly = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            DataRowView drv = comboBox1.SelectedItem as DataRowView;
            if (drv == null)
                return;
            selectedMenyraPagesesId2 = Convert.ToInt32(drv["Id"]);
            tipiPageses2 = drv["Tipi"]?.ToString() ?? "";
            valutaDefault2 = drv["ValutaDefault"]?.ToString() ?? "EUR";
            kerkonReference2 = drv["KerkonReference"] != DBNull.Value ? Convert.ToBoolean(drv["KerkonReference"]) : false;
            bool isCash2 = tipiPageses2.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase);
            bool showReference2 = !isCash2 && kerkonReference2;
            labelNrReference2.Visible = showReference2;
            txtBoxNrReference2.Visible = showReference2;
            txtBoxShuma2Tjeter.Visible = !showReference2;
            labelaShuma2Tjeter.Visible = !showReference2;
            if (showReference2 && string.IsNullOrWhiteSpace(txtBoxNrReference2.Text))
                txtBoxNrReference2.Text = $"RF-{DateTime.Now:yyyyMMddHHmmss}";
            if (!showReference2) 
            {
                txtBoxNrReference2.Clear();
            }
            kursiKembimit2 = MerrKursinPerValuten(valutaDefault2);
            labelaShuma2Tjeter.Text = $"Shuma 2({valutaDefault2.ToUpper()}):";
            if (splitInputMode == SplitInputMode.FromOtherCurrency && ParseDecimal(txtBoxShuma2Tjeter.Text) > 0)
                RecalculateFromShuma2Valute();
            else if (splitInputMode == SplitInputMode.FromPaguar && ParseDecimal(txtBoxPaguar.Text) > 0)
                RecalculateFromPaguar();
            else
            {
                txtBoxShuma1.Text = _totali.ToString("0.00");
                txtBoxShuma2.Text = "0.00";
                txtBoxShuma2Tjeter.Text = "";
            }
               
        }
        private decimal MerrKursinPerValuten(string valuta)
        {
            if (string.IsNullOrWhiteSpace(valuta))
                return 1m;
            string val = valuta.Trim().ToUpper();
            if (val == "EUR")
                return 1m;
            using(var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ""KursiNeEuro""
                                 FROM ""KursetKembimit""
                                 WHERE UPPER(""Valuta"") = @valuta
                                    AND ""Aktiv"" = TRUE
                                 ORDER BY ""CreatedAt"" DESC
                                 LIMIT 1;";
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@valuta", val);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        return Convert.ToDecimal(result);
                }
            }
            return 1m;
        }

        private void RecalculateSplitAmounts()
        {
            RecalculateFromPaguar();
        }

        private void txtBoxShuma2Tjeter_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingSplitFields)
                return;

            if (!splitPaymentEnabled)
                return;

            if (comboBox1.SelectedItem == null)
                return;

            splitInputMode = SplitInputMode.FromOtherCurrency;
            RecalculateFromShuma2Valute();
        }

        private void RecalculateFromPaguar()
        {
            if (!splitPaymentEnabled)
                return;

            isUpdatingSplitFields = true;
            try
            {
                decimal paguarMetoda1 = ParseDecimal(txtBoxPaguar.Text);
                if (paguarMetoda1 < 0)
                    paguarMetoda1 = 0;
                decimal shuma1Eur = paguarMetoda1;
                if(!string.IsNullOrWhiteSpace(valutaDefault) && !valutaDefault.Trim().Equals("EUR", StringComparison.OrdinalIgnoreCase))
                {
                    if (kursiKembimit1 > 0)
                        shuma1Eur = paguarMetoda1 * kursiKembimit1;
                    else
                        shuma1Eur = 0;
                }
                if (shuma1Eur > _totali)
                    shuma1Eur = _totali;
                decimal shuma2Eur = _totali - shuma1Eur;
                if (shuma2Eur < 0)
                    shuma2Eur = 0;
                txtBoxShuma1.Text = shuma1Eur.ToString("0.00");
                txtBoxShuma2.Text = shuma2Eur.ToString("0.00");
                decimal shuma2NeValute = shuma2Eur;
                if(!string.IsNullOrWhiteSpace(valutaDefault2) && !valutaDefault2.Trim().Equals("EUR", StringComparison.OrdinalIgnoreCase))
                {
                    if (kursiKembimit2 > 0)
                        shuma2NeValute = shuma2Eur / kursiKembimit2;
                    else
                        shuma2NeValute = 0;
                }
                txtBoxShuma2Tjeter.Text = shuma2NeValute.ToString("0.00");
                labelShuma1.Text = $"Shuma 1 (EUR nga {valutaDefault}):";
                labelShuma2.Text = "Shuma 2 (EUR):";
                labelaShuma2Tjeter.Text = $"Shuma 2 ({valutaDefault2.ToUpper()}):";
                CalculateKusuri();
            }
            finally
            {
                isUpdatingSplitFields = false;
            }
        }
        private void RecalculateFromShuma2Valute()
        {
            if (!splitPaymentEnabled)
                return;

            isUpdatingSplitFields = true;

            try
            {
                decimal shuma2Valute = ParseDecimal(txtBoxShuma2Tjeter.Text);
                if (shuma2Valute < 0)
                    shuma2Valute = 0;

                decimal shuma2Eur = shuma2Valute;

                if (!string.IsNullOrWhiteSpace(valutaDefault2) &&
                    !valutaDefault2.Trim().Equals("EUR", StringComparison.OrdinalIgnoreCase))
                {
                    if (kursiKembimit2 > 0)
                        shuma2Eur = shuma2Valute * kursiKembimit2;
                    else
                        shuma2Eur = 0;
                }

                if (shuma2Eur > _totali)
                    shuma2Eur = _totali;

                decimal shuma1 = _totali - shuma2Eur;
                if (shuma1 < 0)
                    shuma1 = 0;

                txtBoxShuma2.Text = shuma2Eur.ToString("0.00");
                txtBoxShuma1.Text = shuma1.ToString("0.00");

                decimal paguarAktual = ParseDecimal(txtBoxPaguar.Text);
                if (paguarAktual <= 0)
                    txtBoxPaguar.Text = shuma1.ToString("0.00");

                labelShuma1.Text = "Shuma 1 (EUR):";
                labelShuma2.Text = "Shuma 2 (EUR):";
                labelaShuma2Tjeter.Text = $"Shuma 2 ({valutaDefault2.ToUpper()}):";

                CalculateKusuri();
            }
            finally
            {
                isUpdatingSplitFields = false;
            }
        }
    }
}

