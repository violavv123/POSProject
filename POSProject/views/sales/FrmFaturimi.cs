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
using POSProject.services.sales;
using POSProject.repositories.subjects;
using POSProject.repositories.sales;
using POSProject.repositories.products;
using POSProject.repositories.payments;
using POSProject.services.products;
using POSProject.models;
using POSProject.services.subjects;

namespace POSProject
{
    public partial class FrmFaturimi : Form
    {

        private AutoCompleteStringCollection subjectList = new AutoCompleteStringCollection();
        private List<InvoiceItemModel> invoiceitems = new List<InvoiceItemModel>();
        private int? selectedSubjektiId = null;
        private int savedShitjaId = 0;
        private PrintDocument printDocument = new PrintDocument();
        public bool InvoiceSaved { get; private set; } = false;

        private List<PaymentExecutionModel> paymentInfoList = new List<PaymentExecutionModel>();

        private decimal _totaliFinal;
        private decimal _totaliPaZbritje;
        private decimal _zbritjaTotale;

        private readonly SubjectService _subjectService;
        private readonly SaleService _saleService;
        private readonly GiftCardService _giftCardService;
        private readonly List<int> GIFT_CARD_ARTIKULLI_IDS = new List<int> { 7, 8, 9, 10 };
        private List<string> _issuedGiftCardCodes = new List<string>();
        public FrmFaturimi(List<InvoiceItemModel> items, string komenti, decimal totaliFinal, decimal totaliPaZbritje, decimal zbritjaTotale, List<PaymentExecutionModel> payments)
        {
            InitializeComponent();
            ISubjectRepository subjectRepo = new SubjectRepository();
            ISaleRepository saleRepo = new SaleRepository();
            IProductRepository productRepo = new ProductRepository();
            IPaymentExecutionRepository paymentRepo = new PaymentExecutionRepository();

            _subjectService = new SubjectService(subjectRepo);
            _saleService = new SaleService(saleRepo, productRepo, paymentRepo);
            _giftCardService = new GiftCardService(new GiftCardRepository());
            printDocument.PrintPage += PrintDocument_PrintPage;
            invoiceitems = items ?? new List<InvoiceItemModel>();
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
                    if (p.Tipi != null && p.Tipi.Trim().Equals("GIFTCARD", StringComparison.OrdinalIgnoreCase))
                        g.DrawString("Gift Card Code: " + p.ReferenceNr, normalFont, Brushes.Black, 60, y);
                    else
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

            if (_issuedGiftCardCodes.Any())
            {
                y += 20;
                g.DrawString("Gift Cards të lëshuara:", boldFont, Brushes.Black, 40, y);
                y += 20;

                foreach (var code in _issuedGiftCardCodes)
                {
                    g.DrawString(code, normalFont, Brushes.Black, 60, y);
                    y += 20;
                }
            }
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
            List<string> names = _subjectService.GetSubjectNames();
            foreach(var name in names)
            {
                subjectList.Add(name);
            }
            txtBoxSubjekti.AutoCompleteCustomSource = subjectList;
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

            var subjekti = _subjectService.GetByName(pershkrimi);
            if(subjekti != null)
            {
                selectedSubjektiId = subjekti.Id;
                txtBoxSubjekti.Text = subjekti.Pershkrimi;
                txtBoxNrFiskal.Text = subjekti.NumriFiskal ?? "";
                txtBoxAdresa.Text = subjekti.Adresa ?? "";
                if (comboLloji.Items.Contains(subjekti.LlojiSubjektit))
                    comboLloji.SelectedItem = subjekti.LlojiSubjektit;
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
                var shiftService = new ShiftService();
                var activeShift = shiftService.GetOpenShift();

                if (activeShift == null)
                {
                    MessageBox.Show("Duhet të hapni një ndërrim para se të kryeni shitje.");
                    return;
                }

                var shitja = new SaleModel
                {
                    NrFatures = txtBoxNrFatures.Text.Trim(),
                    DataShitjes = DateTime.Now,
                    Totali = decimal.Parse(txtBoxTotali.Text),
                    Koment = string.IsNullOrWhiteSpace(txtBoxKoment.Text) ? null : txtBoxKoment.Text.Trim(),
                    PerdoruesiId = Session.UserId,
                    SubjektiId = selectedSubjektiId,
                    ShiftId = activeShift.Id,
                    TotaliPaZbritje = _totaliPaZbritje,
                    ZbritjaTotale = _zbritjaTotale,
                    TotaliFinal = _totaliFinal
                };

                var detale = invoiceitems.Select(x => new SaleDetailModel
                {
                    ArtikulliId = x.ArtikulliId,
                    Sasia = x.Sasia,
                    Cmimi = x.Cmimi,
                    Vlera = x.Vlera,
                    Zbritja = x.Zbritja,
                    CmimiFinal = x.CmimiFinal
                }).ToList();

                var payments = paymentInfoList.Select(x => new PaymentExecutionModel
                {
                    MenyraPagesesId = x.MenyraPagesesId,
                    Tipi = x.Tipi,
                    ShumaPaguar = x.ShumaPaguar,
                    PaguarMe = x.PaguarMe,
                    CashBack = x.CashBack,
                    Valuta = x.Valuta ?? "EUR",
                    KursiKembimit = x.KursiKembimit,
                    ShumaNeValuteBaze = x.ShumaNeValuteBaze,
                    ReferenceNr = x.ReferenceNr,
                    Statusi = x.Statusi ?? "Kompletuar",
                    Koment = x.Koment,
                    PerdoruesiId = Session.UserId,
                    CreatedAt = DateTime.Now,
                    ShiftId = activeShift.Id
                }).ToList();

                int newShitjaId = _saleService.SaveSale(shitja, detale, payments);

                CreateGiftCardsForSale(newShitjaId);

                savedShitjaId = newShitjaId;
                InvoiceSaved = true;

                string successMessage = "Fatura u ruajt me sukses.";

                if (_issuedGiftCardCodes.Count > 0)
                {
                    successMessage += "\n\nGift card të krijuara:";
                    foreach (string code in _issuedGiftCardCodes)
                    {
                        successMessage += $"\n- {code}";
                    }
                }

                MessageBox.Show(successMessage, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ruajtjes: " + ex.Message);
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

        private bool IsGiftCardItem(InvoiceItemModel item)
        {
            return GIFT_CARD_ARTIKULLI_IDS.Contains(item.ArtikulliId);
        }

        private List<InvoiceItemModel> GetGiftCardItems()
        {
            return invoiceitems
                .Where(IsGiftCardItem)
                .ToList();
        }

        private void CreateGiftCardsForSale(int shitjaId)
        {
            _issuedGiftCardCodes.Clear();

            var giftCardItems = GetGiftCardItems();

            foreach (var item in giftCardItems)
            {
                int quantity = Convert.ToInt32(item.Sasia);

                for (int i = 0; i < quantity; i++)
                {
                    decimal vleraKarteles = item.CmimiFinal > 0
                        ? item.CmimiFinal
                        : item.Cmimi;

                    GiftCardModel card = _giftCardService.IssueGiftCardFromSale(
                        vleraKarteles,
                        shitjaId,
                        Session.UserId);

                    _issuedGiftCardCodes.Add(card.Kodi);
                }
            }
        }
    }
}
