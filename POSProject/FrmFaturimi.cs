using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace POSProject
{
    public partial class FrmFaturimi : Form
    {

        private AutoCompleteStringCollection subjectList = new AutoCompleteStringCollection();
        private List<InvoiceItem> invoiceitems = new List<InvoiceItem>();
        private int? selectedSubjektiId = null;
        private int savedShitjaId = 0;
        private PrintDocument printDocument = new PrintDocument();
        public bool InvoiceSaved { get; private set; } = false;

        private List<PaymentExecutionModel> paymentInfoList = new List<PaymentExecutionModel>();

        private decimal _totaliFinal;
        private decimal _totaliPaZbritje;
        private decimal _zbritjaTotale;

        public FrmFaturimi(List<InvoiceItem> items, string komenti, decimal totaliFinal, decimal totaliPaZbritje, decimal zbritjaTotale, List<PaymentExecutionModel> payments)
        {
            InitializeComponent();
            printDocument.PrintPage += PrintDocument_PrintPage;
            invoiceitems = items ?? new List<InvoiceItem>();
            txtBoxSubjekti.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBoxSubjekti.AutoCompleteSource = AutoCompleteSource.CustomSource;
            LoadSubjectAutoComplete();
            SetupInvoiceGrid();
            LoadInvoiceItems();
            txtBoxKoment.Text = komenti;
            _totaliFinal = totaliFinal;
            _totaliPaZbritje = totaliPaZbritje;
            _zbritjaTotale = zbritjaTotale;
            txtBoxTotali.Text = _totaliFinal.ToString("0.00");
            txtBoxTotaliPaZbritje.Text = _totaliPaZbritje.ToString("0.00");
            txtBoxZbritjaTotale.Text = _zbritjaTotale.ToString("0.00");
            paymentInfoList = payments ?? new List<PaymentExecutionModel>();
            txtBoxData.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            txtBoxNrFatures.Text = GenerateInvoiceNumber();
            txtBoxMenyra.Text = string.Join(" + ", paymentInfoList.Select(x => x.Pershkrimi));
            txtBoxMenyra.ReadOnly = true;
            var references = paymentInfoList
            .Where(x => !string.IsNullOrWhiteSpace(x.ReferenceNr))
            .Select(x => x.ReferenceNr)
            .ToList();

            txtBoxNrReference.Text = string.Join(", ", references);
            txtBoxNrReference.ReadOnly = true;

            var cashPayments = paymentInfoList
            .Where(x => x.Tipi.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase))
            .ToList();

            txtBoxPaguarMe.Text = cashPayments.Count > 0
                ? string.Join(" + ", cashPayments.Select(x => $"{x.PaguarMe:0.00} {x.Valuta}"))
                : "0.00";
            txtBoxPaguarMe.ReadOnly = true;

            UpdatePaymentUI();

            btnSave.Click += btnSave_Click;
            btnPrint.Click += btnPrint_Click;
            btnClear.Click += btnClear_Click;

            CalculateTVSH();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                btnSave.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font normalFont = new Font("Arial", 10);
            Font boldFont = new Font("Arial", 10, FontStyle.Bold);

            int y = 40;

            g.DrawString("FATURË", headerFont, Brushes.Black, 250, y);
            y += 40;

            g.DrawString("Nr. Faturës: " + txtBoxNrFatures.Text, normalFont, Brushes.Black, 40, y);
            y += 25;

            g.DrawString("Data: " + txtBoxData.Text, normalFont, Brushes.Black, 40, y);
            y += 25;

            g.DrawString("Subjekti: " + txtBoxSubjekti.Text, normalFont, Brushes.Black, 40, y);
            y += 25;

            g.DrawString("Nr Fiskal: " + txtBoxNrFiskal.Text, normalFont, Brushes.Black, 40, y);
            y += 25;

            g.DrawString("Adresa: " + txtBoxAdresa.Text, normalFont, Brushes.Black, 40, y);
            y += 40;

            g.DrawString("Mënyra e pagesës: " + string.Join(" + ", paymentInfoList.Select(x => x.Pershkrimi)), normalFont, Brushes.Black, 40, y);
            y += 25;

            foreach (var p in paymentInfoList)
            {
                string pagesaText = $"{p.Pershkrimi}: {p.ShumaPaguar:0.00} EUR";

                if (!string.IsNullOrWhiteSpace(p.Valuta) &&
                    !p.Valuta.Trim().Equals("EUR", StringComparison.OrdinalIgnoreCase))
                {
                    pagesaText += $" ({p.PaguarMe:0.00} {p.Valuta})";
                }
                else
                {
                    pagesaText += $" (Paguar: {p.PaguarMe:0.00} {p.Valuta})";
                }

                g.DrawString(pagesaText, normalFont, Brushes.Black, 40, y);
                y += 20;

                if (!string.IsNullOrWhiteSpace(p.ReferenceNr))
                {
                    g.DrawString("Reference Nr: " + p.ReferenceNr, normalFont, Brushes.Black, 60, y);
                    y += 20;
                }
            }

            g.DrawString("TVSH 18%: " + txtBoxTVSH.Text + " €", normalFont, Brushes.Black, 40, y);
            y += 25;

            var cashPaymentsForPrint = paymentInfoList
            .Where(x => x.Tipi.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase))
            .ToList();

            if (cashPaymentsForPrint.Count > 0)
            {
                string cashPaidText = string.Join(" + ",
                    cashPaymentsForPrint.Select(x => $"{x.PaguarMe:0.00} {x.Valuta}"));

                g.DrawString("Paguar: " + cashPaidText, normalFont, Brushes.Black, 40, y);
                y += 25;
            }

            g.DrawString("Totali: " + txtBoxTotali.Text + " €", normalFont, Brushes.Black, 40, y);
            y += 25;
            g.DrawString("Produkti", boldFont, Brushes.Black, 40, y);
            g.DrawString("Sasia", boldFont, Brushes.Black, 250, y);
            g.DrawString("Cmimi", boldFont, Brushes.Black, 330, y);
            g.DrawString("Vlera", boldFont, Brushes.Black, 420, y);

            y += 25;

            foreach (var item in invoiceitems)
            {
                g.DrawString(item.Produkti, normalFont, Brushes.Black, 40, y);
                g.DrawString(item.Sasia.ToString(), normalFont, Brushes.Black, 250, y);
                g.DrawString(item.Cmimi.ToString("0.00"), normalFont, Brushes.Black, 330, y);
                g.DrawString(item.Vlera.ToString("0.00"), normalFont, Brushes.Black, 420, y);

                y += 20;
            }

            y += 30;

            g.DrawString("Totali: " + txtBoxTotali.Text + " €", boldFont, Brushes.Black, 350, y);
        }

        private string GenerateInvoiceNumber()
        {
            return "F-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void SetupInvoiceGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Produkti",
                HeaderText = "Produkti",
                DataPropertyName = "Produkti"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sasia",
                HeaderText = "Sasia",
                DataPropertyName = "Sasia",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.##" }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cmimi",
                HeaderText = "Cmimi",
                DataPropertyName = "Cmimi",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Vlera",
                HeaderText = "Vlera",
                DataPropertyName = "Vlera",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
            });
        }

        private void LoadInvoiceItems()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = invoiceitems;
            dataGridView1.DataSource = bs;
        }
        private void LoadSubjectAutoComplete()
        {

            subjectList.Clear();
            using (var connection = Db.GetConnection())
            {
                connection.Open();
                string query = @"SELECT ""Pershkrimi"" 
                                FROM ""Subjektet""
                                ORDER BY ""Pershkrimi""";

                using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjectList.Add(reader["Pershkrimi"].ToString());
                    }
                }
                txtBoxSubjekti.AutoCompleteCustomSource = subjectList;
            }
        }

        private void FrmFaturimi_Load(object sender, EventArgs e)
        {
            btnSubject.Visible = Session.Role == "admin";
            txtBoxNrFatures.ReadOnly = true;
            txtBoxData.ReadOnly = true;
            txtBoxTotali.ReadOnly = true;
            txtBoxNrFatures.ReadOnly = true;
            txtBoxSubjekti.Leave += txtBoxSubjekti_Leave;
            txtBoxSubjekti.KeyDown += txtBoxSubjekti_KeyDown;
            txtBoxSubjekti.TextChanged += txtBoxSubjekti_TextChanged;
            comboLloji.Items.Clear();
            comboLloji.Items.Add("Biznes");
            comboLloji.Items.Add("Kompani");
            comboLloji.Items.Add("Privat");
            comboLloji.Items.Add("OJQ");
            comboLloji.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void txtBoxSubjekti_TextChanged(object sender, EventArgs e)
        {
            string text = txtBoxSubjekti.Text.Trim();
            bool exactMatch = subjectList.Cast<string>().Any(x => x.Equals(text, StringComparison.OrdinalIgnoreCase));
            if (exactMatch)
                LoadSubjectDetails(text);
        }

        private void txtBoxSubjekti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadSubjectDetails(txtBoxSubjekti.Text.Trim());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtBoxSubjekti_Leave(object sender, EventArgs e)
        {
            LoadSubjectDetails(txtBoxSubjekti.Text.Trim());
        }
        private void LoadSubjectDetails(string pershkrimi)
        {
            if (string.IsNullOrWhiteSpace(pershkrimi))
            {
                selectedSubjektiId = null;
                txtBoxNrFiskal.Clear();
                txtBoxAdresa.Clear();
                comboLloji.SelectedIndex = -1;
                return;
            }

            using (var connection = Db.GetConnection())
            {
                connection.Open();
                string query = @"SELECT ""Id"",""Pershkrimi"",""NumriFiskal"",""Adresa"",""LlojiSubjektit""
                               FROM ""Subjektet""
                               WHERE LOWER(TRIM(""Pershkrimi""))=LOWER(TRIM(@pershkrimi))
                               LIMIT 1;";
                using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@pershkrimi", pershkrimi);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            selectedSubjektiId = Convert.ToInt32(reader["Id"]);
                            txtBoxSubjekti.Text = reader["Pershkrimi"].ToString();
                            txtBoxNrFiskal.Text = reader["NumriFiskal"]?.ToString() ?? "";
                            txtBoxAdresa.Text = reader["Adresa"]?.ToString() ?? "";
                            string lloji = reader["LlojiSubjektit"]?.ToString() ?? "";
                            if (comboLloji.Items.Contains(lloji))
                                comboLloji.SelectedItem = lloji;
                            else
                                comboLloji.SelectedIndex = -1;
                        }
                        else
                        {
                            selectedSubjektiId = null;
                            txtBoxNrFiskal.Clear();
                            txtBoxAdresa.Clear();
                            comboLloji.SelectedIndex = -1;
                        }
                    }
                }
            }
        }

        private bool ValidateInvoice()
        {
            if (string.IsNullOrWhiteSpace(txtBoxSubjekti.Text))
            {
                MessageBox.Show("Shkruaj subjektin.");
                txtBoxSubjekti.Focus();
                return false;
            }
            if (invoiceitems == null || invoiceitems.Count == 0)
            {
                MessageBox.Show("Fatura nuk ka artikuj.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBoxTotali.Text) || !decimal.TryParse(txtBoxTotali.Text, out decimal total) || total <= 0)
            {
                MessageBox.Show("Totali nuk është valid.");
                return false;
            }
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            selectedSubjektiId = null;
            savedShitjaId = 0;
            txtBoxSubjekti.Clear();
            txtBoxNrFiskal.Clear();
            txtBoxAdresa.Clear();
            comboLloji.SelectedIndex = -1;
            txtBoxKoment.Clear();
            txtBoxTotali.Text = "0.00";
            txtBoxData.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            txtBoxNrFatures.Text = GenerateInvoiceNumber();
            txtBoxSubjekti.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInvoice())
                return;
            try
            {
                using (var connection = Db.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            int newShitjaId;
                            var shiftService = new ShiftService();
                            var activeShift = shiftService.GetOpenShift();

                            if (activeShift == null)
                            {
                                MessageBox.Show("Duhet të hapni një ndërrim para se të kryeni shitje.");
                                return;
                            }
                            int currentShiftId = activeShift.Id;
                            string insertShitjaQuery = @"INSERT INTO ""Shitjet"" (""NrFatures"",""DataShitjes"",""Totali"",""Koment"",""perdoruesi_id"",""SubjektiId"", ""ShiftId"") VALUES
                                                       (@NrFatures, @DataShitjes, @Totali, @Koment, @PerdoruesiId, @SubjektiId, @ShiftId)
                                                       RETURNING ""Id"";";
                            using (var cmd = new Npgsql.NpgsqlCommand(insertShitjaQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@NrFatures", txtBoxNrFatures.Text.Trim());
                                cmd.Parameters.AddWithValue("@DataShitjes", DateTime.Now);
                                cmd.Parameters.AddWithValue("@Totali", decimal.Parse(txtBoxTotali.Text));

                                if (string.IsNullOrWhiteSpace(txtBoxKoment.Text))
                                {
                                    cmd.Parameters.AddWithValue("@Koment", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Koment", txtBoxKoment.Text.Trim());
                                }

                                cmd.Parameters.AddWithValue("@PerdoruesiId", Session.UserId);
                                if (selectedSubjektiId.HasValue)
                                {
                                    cmd.Parameters.AddWithValue("@SubjektiId", selectedSubjektiId.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@SubjektiId", DBNull.Value);
                                }
                                cmd.Parameters.AddWithValue("@ShiftId", currentShiftId);
                                newShitjaId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            foreach (var item in invoiceitems)
                            {
                                string stockCheckQuery = @"SELECT ""SasiaNeStok""
                                                         FROM ""Artikujt""
                                                         WHERE ""Id"" = @artikulliId;";
                                decimal sasiaNeStokAktuale;
                                using (var cmdStock = new Npgsql.NpgsqlCommand(stockCheckQuery, connection, transaction))
                                {
                                    cmdStock.Parameters.AddWithValue("@artikulliId", item.ArtikulliId);
                                    object stockResult = cmdStock.ExecuteScalar();
                                    if (stockResult == null)
                                        throw new Exception("Produkti me Id: " + item.ArtikulliId + "nuk u gjet në databazë.");

                                    sasiaNeStokAktuale = Convert.ToDecimal(stockResult);
                                }
                                if (item.Sasia > sasiaNeStokAktuale)
                                {
                                    NotificationService.Create("STOCK_ERROR", "Error", "Stok i pamjaftueshëm gjatë ruajtjes.", $"Produkti {item.Produkti}", "Artikujt", item.ArtikulliId, Session.UserId);
                                    throw new Exception("Nuk ka stok të mjaftueshëm për produktin: '" + item.Produkti + "'. Në stok: " + sasiaNeStokAktuale + ",kërkohet: " + item.Sasia);
                                }

                                string insertDetaleQuery = @"INSERT INTO ""ShitjetDetale"" (""ShitjaId"", ""ArtikulliId"",""Sasia"",""Cmimi"",""Vlera"") VALUES
                                                   (@ShitjaId, @ArtikulliId, @Sasia, @Cmimi, @Vlera);";
                                using (var cmd = new Npgsql.NpgsqlCommand(insertDetaleQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ShitjaId", newShitjaId);
                                    cmd.Parameters.AddWithValue("@ArtikulliId", item.ArtikulliId);
                                    cmd.Parameters.AddWithValue("@Sasia", item.Sasia);
                                    cmd.Parameters.AddWithValue("@Cmimi", item.Cmimi);
                                    cmd.Parameters.AddWithValue("@Vlera", item.Vlera);
                                    cmd.ExecuteNonQuery();

                                }

                                string updateStockQuery = @"UPDATE ""Artikujt""
                                                          SET ""SasiaNeStok"" = ""SasiaNeStok"" - @sasia
                                                          WHERE ""Id"" = @artikulliId;";
                                using (var cmdUpdateStock = new Npgsql.NpgsqlCommand(updateStockQuery, connection, transaction))
                                {
                                    cmdUpdateStock.Parameters.AddWithValue("@sasia", item.Sasia);
                                    cmdUpdateStock.Parameters.AddWithValue("@artikulliId", item.ArtikulliId);
                                    cmdUpdateStock.ExecuteNonQuery();
                                }


                            }

                            string insertPagesaQuery = @"INSERT INTO ""EkzekutimiPageses""(""ShitjaId"",""MenyraPagesesId"",""ShumaPaguar"",""PaguarMe"",
                                                        ""CashBack"",""Valuta"",""KursiKembimit"",""ShumaNeValuteBaze"",""ReferenceNr"",""Statusi"",""Koment"",""PerdoruesiId"",""Created_At"", ""ShiftId"")
                                                        VALUES (@ShitjaId, @MenyraPagesesId, @ShumaPaguar, @PaguarMe, @CashBack, @Valuta, @KursiKembimit, @ShumaNeValuteBaze, @ReferenceNr,
                                                        @Statusi, @Koment, @PerdoruesiId, @Created_At, @ShiftId);";
                            foreach (var payment in paymentInfoList)
                            {
                                using (var cmdPagesa = new Npgsql.NpgsqlCommand(insertPagesaQuery, connection, transaction))
                                {
                                    cmdPagesa.Parameters.AddWithValue("@ShitjaId", newShitjaId);
                                    cmdPagesa.Parameters.AddWithValue("@MenyraPagesesId", payment.MenyraPagesesId);
                                    cmdPagesa.Parameters.AddWithValue("@ShumaPaguar", payment.ShumaPaguar);
                                    cmdPagesa.Parameters.AddWithValue("@PaguarMe", payment.PaguarMe);
                                    cmdPagesa.Parameters.AddWithValue("@CashBack", payment.CashBack);
                                    cmdPagesa.Parameters.AddWithValue("@Valuta", payment.Valuta ?? "EUR");
                                    cmdPagesa.Parameters.AddWithValue("@KursiKembimit", payment.KursiKembimit);
                                    cmdPagesa.Parameters.AddWithValue("@ShumaNeValuteBaze", payment.ShumaNeValuteBaze);
                                    cmdPagesa.Parameters.AddWithValue("@ReferenceNr",
                                        string.IsNullOrWhiteSpace(payment.ReferenceNr) ? (object)DBNull.Value : payment.ReferenceNr);
                                    cmdPagesa.Parameters.AddWithValue("@Statusi", payment.Statusi ?? "Kompletuar");
                                    cmdPagesa.Parameters.AddWithValue("@Koment",
                                        string.IsNullOrWhiteSpace(payment.Koment) ? (object)DBNull.Value : payment.Koment);
                                    cmdPagesa.Parameters.AddWithValue("@PerdoruesiId", Session.UserId);
                                    cmdPagesa.Parameters.AddWithValue("@Created_At", DateTime.Now);
                                    cmdPagesa.Parameters.AddWithValue("@ShiftId", activeShift.Id);
                                    cmdPagesa.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            savedShitjaId = newShitjaId;
                            InvoiceSaved = true;
                            AutoClosingMessageBox.Show("Fatura u ruajt me sukses.", "Informacion", 900);
                            NotificationService.Create("SALE_COMPLETED", "Info", "Shitje e realizuar", $"Fatura {txtBoxNrFatures.Text} - Totali {txtBoxTotali.Text} EUR", "Shitjet", newShitjaId, Session.UserId);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Gabim gjate ruajtjes: " + ex.Message);
                            NotificationService.Create("SALE_ERROR", "Error", "Gabim gjatë shitjes", ex.Message, "Shitjet", null, Session.UserId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim me databazen: " + ex.Message);
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (savedShitjaId == 0)
            {
                MessageBox.Show("Ruaje faturën para printimit.");
                return;
            }
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDocument;
            preview.Width = 1000;
            preview.Height = 800;
            preview.ShowDialog();
        }

        private void btnSubject_Click(object sender, EventArgs e)
        {
            FrmSubjektet subjects = new FrmSubjektet();
            subjects.ShowDialog();
        }

        private void CalculateTVSH()
        {
            decimal totali = decimal.Parse(txtBoxTotali.Text);
            decimal baza = totali / 1.18m;
            decimal tvsh = totali - baza;
            txtBoxTVSH.Text = tvsh.ToString("0.00");
        }

        private void UpdatePaymentUI()
        {
            if (paymentInfoList == null || paymentInfoList.Count == 0)
                return;

            bool hasCash = paymentInfoList.Any(x =>
                x.Tipi.Trim().Equals("CASH", StringComparison.OrdinalIgnoreCase));

            bool hasReference = paymentInfoList.Any(x =>
                !string.IsNullOrWhiteSpace(x.ReferenceNr));

            txtBoxPaguarMe.Visible = hasCash;
            labelPaguarMe.Visible = hasCash;

            txtBoxNrReference.Visible = hasReference;
            labelNrReference.Visible = hasReference;

            txtBoxPaguarMe.ReadOnly = true;
            txtBoxNrReference.ReadOnly = true;
        }

        
    }
}
