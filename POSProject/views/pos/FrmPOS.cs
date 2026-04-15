using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using System.Globalization;
using System.Diagnostics;
using POSProject.repositories.returns;
using POSProject.services.returns;
using POSProject.services.products;
using POSProject.repositories.products;
using POSProject.models;
using POSProject.repositories.notifications;
using POSProject.services.notifications;

namespace POSProject
{
    public partial class FrmPOS : Form
    {
        private DataTable cartTable;
        private System.Windows.Forms.Timer notificationTimer;

        private decimal invoiceDiscountAmount = 0m;
        private decimal totaliPaZbritje = 0m;
        private decimal zbritjaTotale = 0m;
        private decimal totaliFinal = 0m;

        private readonly ProductService _productService;
        private readonly INotificationService _notifsService;
        
        public FrmPOS()
        {
            InitializeComponent();

            IProductRepository productRepo = new ProductRepository();
            _productService = new ProductService(productRepo);
            INotificationRepository notifsRepo = new NotificationRepository();
            _notifsService = new NotificationService(notifsRepo);

            this.KeyPreview = true;
            txtBoxBarkodi.KeyDown += txtBoxBarkodi_KeyDown;
            btnSasia.Click += btnSasia_Click;
            btnNderroCmimin.Click += btnNderroCmimin_Click;
            txtBoxPaguar.TextChanged += txtBoxPaguar_TextChanged;
            txtBoxPaguar.KeyDown += txtBoxPaguar_KeyDown;
            txtBoxPaguar.Leave += txtBoxPaguar_Leave;
            btnLogOut.Click += btnLogOut_Click;
            btnNotifications.Click += btnNotifications_Click;
            SetupTimer();
            btnLogOut.FlatStyle = FlatStyle.Flat;
            btnLogOut.FlatAppearance.BorderSize = 0;
            btnLogOut.MouseEnter += btn_MouseEnter;
            btnLogOut.MouseLeave += btn_MouseLeave;

            btnAddProducts.MouseEnter += btn_MouseEnter;
            btnAddProducts.MouseLeave += btn_MouseLeave;
            btnAddProducts.FlatStyle = FlatStyle.Flat;
            btnAddProducts.FlatAppearance.BorderSize = 0;

            btnShift.Click += btnShift_Click;
            btnShift.MouseEnter += btn_MouseEnter;
            btnShift.MouseLeave += btn_MouseLeave;
            btnShift.FlatStyle = FlatStyle.Flat;
            btnShift.FlatAppearance.BorderSize = 0;

            btnSasia.MouseEnter += btn_MouseEnter;
            btnSasia.MouseLeave += btn_MouseLeave;
            btnSasia.FlatStyle = FlatStyle.Flat;
            btnSasia.FlatAppearance.BorderSize = 0;

            btnNderroCmimin.MouseEnter += btn_MouseEnter;
            btnNderroCmimin.MouseLeave += btn_MouseLeave;
            btnNderroCmimin.FlatStyle = FlatStyle.Flat;
            btnNderroCmimin.FlatAppearance.BorderSize = 0;

            btnAdd.MouseEnter += btn_MouseEnter;
            btnAdd.MouseLeave += btn_MouseLeave;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;

            btnRemove.MouseEnter += btn_MouseEnter;
            btnRemove.MouseLeave += btn_MouseLeave;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.FlatAppearance.BorderSize = 0;

            btnFinish.MouseEnter += btn_MouseEnter;
            btnFinish.MouseLeave += btn_MouseLeave;
            btnFinish.FlatStyle = FlatStyle.Flat;
            btnFinish.FlatAppearance.BorderSize = 0;

            btnSalesList.MouseEnter += btn_MouseEnter;
            btnSalesList.MouseLeave += btn_MouseLeave;
            btnSalesList.FlatStyle = FlatStyle.Flat;
            btnSalesList.FlatAppearance.BorderSize = 0;

            btnMethodPayment.MouseEnter += btn_MouseEnter;
            btnMethodPayment.MouseLeave += btn_MouseLeave;
            btnMethodPayment.FlatStyle = FlatStyle.Flat;
            btnMethodPayment.FlatAppearance.BorderSize = 0;

            btnAddUsers.MouseEnter += btn_MouseEnter;
            btnAddUsers.MouseLeave += btn_MouseLeave;
            btnAddUsers.FlatStyle = FlatStyle.Flat;
            btnAddUsers.FlatAppearance.BorderSize = 0;

            btnDiscountInvoice.MouseEnter += btn_MouseEnter;
            btnDiscountInvoice.MouseLeave += btn_MouseLeave;
            btnDiscountInvoice.FlatStyle = FlatStyle.Flat;
            btnDiscountInvoice.FlatAppearance.BorderSize = 0;

            btnDiscountItem.MouseEnter += btn_MouseEnter;
            btnDiscountItem.MouseLeave += btn_MouseLeave;
            btnDiscountItem.FlatStyle = FlatStyle.Flat;
            btnDiscountItem.FlatAppearance.BorderSize = 0;

            btnReturn.MouseEnter += btn_MouseEnter;
            btnReturn.MouseLeave += btn_MouseLeave;
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.FlatAppearance.BorderSize = 0;

            btnGiftCard.MouseEnter += btn_MouseEnter;
            btnGiftCard.MouseLeave += btn_MouseLeave;
            btnGiftCard.FlatStyle = FlatStyle.Flat;
            btnGiftCard.FlatAppearance.BorderSize = 0;

            btnDiscountInvoice.Click += btnDiscountInvoice_Click;
            btnDiscountItem.Click += btnDiscountItem_Click;
            btnReturn.Click += btnReturn_Click;
            btnGiftCard.Click += btnGiftCard_Click;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                btnFinish.PerformClick();
                return true;
            }

            if (keyData == Keys.F6)
            {
                btnRemove.PerformClick();
                return true;
            }

            if (keyData == Keys.F2)
            {
                btnSasia.PerformClick();
                return true;
            }

            if (keyData == Keys.F4)
            {
                btnNderroCmimin.PerformClick();
                return true;
            }

            if (keyData == Keys.F3)
            {
                btnShift.PerformClick();
                return true;
            }

            if (keyData == Keys.Tab)
            {
                txtBoxPaguar.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmPOS_Load(object sender, EventArgs e)
        {
            SetupCartGrid();
            txtBoxKusuri.ReadOnly = true;
            txtBoxTotali.ReadOnly = true;
            labelUsername.Text = "User: " + Session.Username;
            btnAddUsers.Visible = Session.Role == "admin";
            btnSalesList.Visible = Session.Role == "admin";
            btnAddProducts.Visible = Session.Role == "admin";
            btnMethodPayment.Visible = Session.Role == "admin";
            btnNotifications.Visible = Session.Role == "admin";
            btnGiftCard.Visible = Session.Role == "admin";
            UpdateNotificationBadge();
        }

        private void btnAddUsers_Click(object sender, EventArgs e)
        {
            if (Session.Role != "admin")
            {
                MessageBox.Show("Nuk keni qasje në shtimin e punëtorëve.");
                return;
            }
            FrmSignUp signUp = new FrmSignUp();
            signUp.ShowDialog();
        }

        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            if (Session.Role != "admin")
            {
                MessageBox.Show("Nuk keni qasje në menaxhimin e produkteve.");
                return;
            }
            FrmProductManagement products = new FrmProductManagement();
            products.ShowDialog();
        }

        private void btnMethodPayment_Click(object sender, EventArgs e)
        {
            if (Session.Role != "admin")
            {
                MessageBox.Show("Nuk keni qasje në menaxhimin e mënyrave të pagesës.");
                return;
            }
            FrmMethodsOfPayment methods = new FrmMethodsOfPayment();
            methods.ShowDialog();
        }
        private void btnSalesList_Click(object sender, EventArgs e)
        {
            if (Session.Role != "admin")
            {
                MessageBox.Show("Nuk keni qasje në listën e shitjeve.");
                return;
            }
            FrmSalesList salesList = new FrmSalesList();
            salesList.ShowDialog();
        }
        private void SetupCartGrid()
        {
            cartTable = new DataTable();
            cartTable.Columns.Add("ArtikulliId", typeof(int));
            cartTable.Columns.Add("Produkti", typeof(string));
            cartTable.Columns.Add("Sasia", typeof(decimal));
            cartTable.Columns.Add("Cmimi", typeof(decimal));
            cartTable.Columns.Add("Zbritja", typeof(decimal));
            cartTable.Columns.Add("CmimiFinal", typeof(decimal));
            cartTable.Columns.Add("Vlera", typeof(decimal));

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DataSource = cartTable;

            dataGridView1.Columns["ArtikulliId"].Visible = false;
            dataGridView1.Columns["Produkti"].Width = 200;
            dataGridView1.Columns["Sasia"].Width = 80;
            dataGridView1.Columns["Cmimi"].Width = 80;
            dataGridView1.Columns["Zbritja"].Width = 80;
            dataGridView1.Columns["CmimiFinal"].Width = 85;
            dataGridView1.Columns["Vlera"].Width = 80;

            dataGridView1.Columns["Cmimi"].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns["Zbritja"].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns["CmimiFinal"].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns["Vlera"].DefaultCellStyle.Format = "0.00";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            { 

                if (string.IsNullOrWhiteSpace(txtBoxBarkodi.Text))
                {
                    txtBoxBarkodi.Focus();
                    return;
                }

                string barkodi = txtBoxBarkodi.Text.Trim();
                ProductModel product = _productService.GetByBarcode(barkodi);

                if (product == null)
                {
                    AutoClosingMessageBox.Show("Produkti nuk u gjet.", "Informacion", 900);
                    _notifsService.Create("PRODUCT_NOT_FOUND", "Warning", "Produkti nuk u gjet.", $"Barcode:{txtBoxBarkodi.Text}", "Artikujt", null, Session.UserId);
                    txtBoxBarkodi.Focus();
                    return;
                }

                int artikulliId = product.Id;
                string emri = product.Emri;
                decimal cmimi = product.CmimiShitjes;
                decimal sasiaKerkuar = 1m;
                decimal sasiaNeStok = product.SasiaNeStok;
                decimal sasiaNeFature = GetQuantityInCart(artikulliId);

                if (!_productService.HasEnoughStock(artikulliId, sasiaKerkuar, sasiaNeFature))
                {
                    AutoClosingMessageBox.Show($"Nuk ka stok te mjaftueshem per produktin '{emri}'. Ne stok: {sasiaNeStok}, ne fature: {sasiaNeFature}, u kerkuar edhe: {sasiaKerkuar}.", "Informacion", 900);
                    _notifsService.Create("LOW_STOCK", "Warning", "Stok i pamjaftueshëm.", $"Produkti{emri} - stok:{sasiaNeStok}", "Artikujt", artikulliId, Session.UserId);
                    txtBoxBarkodi.Focus();
                    return;
                }

                DataRow existingCartRow = null;

                foreach (DataRow row in cartTable.Rows)
                {
                    if (Convert.ToInt32(row["ArtikulliId"]) == artikulliId)
                    {
                        existingCartRow = row;
                        break;
                    }
                }

                if (existingCartRow != null)
                {
                    decimal sasiaAktuale = Convert.ToDecimal(existingCartRow["Sasia"]);
                    decimal sasiaRe = sasiaAktuale + sasiaKerkuar;

                    decimal zbritjaAktuale = Convert.ToDecimal(existingCartRow["Zbritja"]);
                    decimal cmimiFinal = cmimi - zbritjaAktuale;
                    if (cmimiFinal < 0)
                        cmimiFinal = 0;

                    existingCartRow["Sasia"] = sasiaRe;
                    existingCartRow["Cmimi"] = cmimi;
                    existingCartRow["CmimiFinal"] = cmimiFinal;
                    existingCartRow["Vlera"] = sasiaRe * cmimiFinal;
                }
                else
                {
                    decimal zbritja = 0m;
                    decimal cmimiFinal = cmimi;
                    decimal vlera = cmimiFinal * sasiaKerkuar;
                    cartTable.Rows.Add(artikulliId, emri, sasiaKerkuar, cmimi, zbritja, cmimiFinal, vlera);
                }

                CalculateTotal();
                txtBoxBarkodi.Clear();
                BeginInvoke(new Action(() =>
                {
                    txtBoxBarkodi.Focus();
                    txtBoxBarkodi.SelectAll();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void CalculateTotal()
        {
            totaliPaZbritje = 0m;
            decimal totalAfterItemsDiscount = 0m;

            foreach (DataRow row in cartTable.Rows)
            {
                decimal sasia = Convert.ToDecimal(row["Sasia"]);
                decimal cmimi = Convert.ToDecimal(row["Cmimi"]);
                decimal vlera = Convert.ToDecimal(row["Vlera"]);

                totaliPaZbritje += sasia * cmimi;
                totalAfterItemsDiscount += vlera;
            }

            zbritjaTotale = (totaliPaZbritje - totalAfterItemsDiscount) + invoiceDiscountAmount;

            totaliFinal = totalAfterItemsDiscount - invoiceDiscountAmount;

            if (totaliFinal < 0)
                totaliFinal = 0;

            txtBoxTotali.Text = totaliFinal.ToString("0.00");

            CalculateChange();
        }

        private void CalculateChange()
        {
            decimal total = ParseDecimal(txtBoxTotali.Text);
            decimal paguar = ParseDecimal(txtBoxPaguar.Text);

            decimal kusuri = paguar - total;

            if (kusuri < 0)
            {
                kusuri = 0;
            }
            txtBoxKusuri.Text = kusuri.ToString("0.00");
        }


        private decimal ParseDecimal(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            text = text.Trim();

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal value))
                return value;

            if (decimal.TryParse(text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                return value;

            if (decimal.TryParse(text.Replace(".", ","), NumberStyles.Any, new CultureInfo("de-DE"), out value))
                return value;

            return 0;
        }
        private void txtBoxPaguar_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private decimal GetQuantityInCart(int artikulliId)
        {
            decimal total = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                if (Convert.ToInt32(row["ArtikulliId"]) == artikulliId)
                    total += Convert.ToDecimal(row["Sasia"]);
            }
            return total;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                int rowIndex = dataGridView1.CurrentRow.Index;
                cartTable.Rows[rowIndex].Delete();
                CalculateTotal();
            }
            txtBoxBarkodi.Focus();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                if (cartTable == null || cartTable.Rows.Count == 0)
                {
                    AutoClosingMessageBox.Show("Nuk ka produkte në shitje.", "Informacion", 900);
                    _notifsService.Create("EMPTY_CART", "Warning", "Tentim shitje pa produkte", "U tentua të bëhet pagesë pa artikuj.", "Shitjet", null, Session.UserId);
                    return;
                }

                decimal total = totaliFinal;
                decimal paguarPOS = ParseDecimal(txtBoxPaguar.Text);
                List<InvoiceItemModel> items = GetInvoiceItemsFromCart();

                using (FrmPagesa pagesa = new FrmPagesa(
                    totaliFinal,
                    paguarPOS,
                    txtBoxKoment.Text.Trim(),
                    items,
                    totaliPaZbritje,
                    zbritjaTotale))
                {
                    if (pagesa.ShowDialog() == DialogResult.OK)
                    {
                        ClearCart();
                    }
                }

                txtBoxBarkodi.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearCart()
        {
            cartTable.Clear();
            invoiceDiscountAmount = 0m;
            totaliPaZbritje = 0m;
            zbritjaTotale = 0m;
            totaliFinal = 0m;
            txtBoxTotali.Text = "0.00";
            txtBoxPaguar.Clear();
            txtBoxKusuri.Text = "0.00";
            txtBoxKoment.Clear();
            txtBoxBarkodi.Clear();
            txtBoxBarkodi.Focus();
        }
        private void txtBoxBarkodi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnNderroCmimin_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Zgjedh nje rresht.");
                return;
            }

            int artikulliId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ArtikulliId"]);
            DataGridViewRow row = dataGridView1.CurrentRow;
            string emri = row.Cells["Produkti"].Value.ToString();
            decimal sasia = Convert.ToDecimal(row.Cells["Sasia"].Value);
            decimal cmimiAktual = Convert.ToDecimal(row.Cells["Cmimi"].Value);

            string input = ShowInputDialog("Ndrysho cmimin", $"Jep cmimin e ri per '{emri}':", cmimiAktual.ToString("0.00"));
            if (input == null)
                return;

            if (!decimal.TryParse(input, out decimal cmimiRi) || cmimiRi <= 0)
            {
                MessageBox.Show("Cmimi i ri duhet te jete me i madh se zero.");
                return;
            }

            row.Cells["Cmimi"].Value = cmimiRi;

            decimal zbritjaAktuale = Convert.ToDecimal(row.Cells["Zbritja"].Value);
            decimal cmimiFinal = cmimiRi - zbritjaAktuale;
            if (cmimiFinal < 0)
                cmimiFinal = 0;
            row.Cells["CmimiFinal"].Value = cmimiFinal;
            row.Cells["Vlera"].Value = sasia * cmimiFinal;
            _notifsService.Create("PRICE_CHANGE", "Info", "Çmimi u ndryshua.", $"Produkti: {emri} nga {cmimiAktual} në {cmimiRi}", "Artikujt", artikulliId, Session.UserId);
            CalculateTotal();
            CalculateChange();
            BeginInvoke(new Action(() =>
            {
                txtBoxBarkodi.Clear();
                txtBoxBarkodi.Focus();
            }));

        }

        private void txtBoxPaguar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CalculateChange();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxPaguar_Leave(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void btnSasia_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Zgjedh nje rresht!");
                return;
            }

            DataGridViewRow row = dataGridView1.CurrentRow;
            int artikulliId = Convert.ToInt32(row.Cells["ArtikulliId"].Value);
            string emri = row.Cells["Produkti"].Value.ToString();
            decimal cmimi = Convert.ToDecimal(row.Cells["Cmimi"].Value);
            decimal sasiaAktuale = Convert.ToDecimal(row.Cells["Sasia"].Value);

            string input = ShowInputDialog("Ndrysho sasinë", $"Jep sasinë e re për '{emri}':", sasiaAktuale.ToString("0.##"));

            if (input == null)
            {
                return;
            }

            if (!decimal.TryParse(input, out decimal sasiaRe) || sasiaRe <= 0)
            {
                MessageBox.Show("Sasia e re duhet te jete me e madhe se zero.");
                return;
            }

            ProductModel product = _productService.GetById(artikulliId);
            if(product == null)
            {
                MessageBox.Show("Produkti nuk u gjet.");
                return;
            }

            decimal sasiaNeStok = product.SasiaNeStok;
            decimal sasiaTjeterNeFature = GetQuantityInCart(artikulliId) - sasiaAktuale;

            if (!_productService.HasEnoughStock(artikulliId, sasiaRe, sasiaTjeterNeFature))
            {
                MessageBox.Show($"Nuk ka stok te mjaftueshem per '{emri}'. Ne stok: {sasiaNeStok}");
                return;
            }

            decimal zbritjaAktuale = Convert.ToDecimal(row.Cells["Zbritja"].Value);
            decimal cmimiFinal = cmimi - zbritjaAktuale;
            if (cmimiFinal < 0)
                cmimiFinal = 0;
            row.Cells["Sasia"].Value = sasiaRe;
            row.Cells["CmimiFinal"].Value = cmimiFinal;
            row.Cells["Vlera"].Value = sasiaRe * cmimiFinal;
            CalculateTotal();
            CalculateChange();

            BeginInvoke(new Action(() =>
            {
                txtBoxBarkodi.Clear();
                txtBoxBarkodi.Focus();
            }));
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("A jeni të sigurtë që dëshironi të dilni?", "LogOut", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Session.Clear();
                this.Hide();
                FrmLogin login = new FrmLogin();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                    labelUsername.Text = "User:" + Session.Username;
                    btnAddUsers.Visible = Session.Role == "admin";
                    btnSalesList.Visible = Session.Role == "admin";
                    btnAddProducts.Visible = Session.Role == "admin";
                    btnMethodPayment.Visible = Session.Role == "admin";
                }
                else
                {
                    this.Close();
                }
            }
        }


        private string ShowInputDialog(string title, string prompt, string defaultValue = "")
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            form.Text = title;
            label.Text = prompt;
            textBox.Text = defaultValue;
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";

            label.SetBounds(10, 10, 260, 20);
            textBox.SetBounds(10, 35, 260, 25);
            buttonOk.SetBounds(70, 70, 75, 30);
            buttonCancel.SetBounds(155, 70, 75, 30);
            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            form.ClientSize = new Size(285, 115);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            return form.ShowDialog() == DialogResult.OK ? textBox.Text : null;

        }

        private List<InvoiceItemModel> GetInvoiceItemsFromCart()
        {
            List<InvoiceItemModel> items = new List<InvoiceItemModel>();
            foreach (DataRow row in cartTable.Rows)
            {
                items.Add(new InvoiceItemModel
                {
                    ArtikulliId = Convert.ToInt32(row["ArtikulliId"]),
                    Produkti = row["Produkti"].ToString(),
                    Sasia = Convert.ToDecimal(row["Sasia"]),
                    Cmimi = Convert.ToDecimal(row["Cmimi"]),
                    Zbritja = Convert.ToDecimal(row["Zbritja"]),
                    CmimiFinal = Convert.ToDecimal(row["CmimiFinal"]),
                    Vlera = Convert.ToDecimal(row["Vlera"])

                });
            }
            return items;
        }

        private void UpdateNotificationBadge()
        {
            int count = _notifsService.GetUnreadCount(30);

            btnNotifications.BackColor = count > 0 ? Color.LightCoral : Color.LightGray;
            btnNotifications.Font = new Font("Segoe UI Emoji", 10, FontStyle.Bold);

            if (count > 0)
                btnNotifications.Text = $"\U0001F514 ({count})";
            else
                btnNotifications.Text = "\U0001F514";
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            if (Session.Role != "admin")
            {
                MessageBox.Show("Nuk keni qasje në menaxhimin e njoftimeve.");
                return;
            }
            FrmNotification notification = new FrmNotification();
            notification.ShowDialog();
            UpdateNotificationBadge();
        }

        private void SetupTimer()
        {
            notificationTimer = new System.Windows.Forms.Timer();
            notificationTimer.Interval = 3000;
            notificationTimer.Tick += NotificationTimer_Tick;
            notificationTimer.Start();
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            UpdateNotificationBadge();
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.LightSteelBlue;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            btn.FlatAppearance.BorderSize = 0;
        }
        private void btnShift_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmShiftOpenClose())
            {
                frm.ShowDialog();
            }
        }

        private void btnDiscountItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                AutoClosingMessageBox.Show("Zgjedh një rresht.", "Info", 900);
                return;
            }
            DataGridViewRow row = dataGridView1.CurrentRow;
            int artikulliId = Convert.ToInt32(row.Cells["ArtikulliId"].Value);
            string emri = row.Cells["Produkti"].Value.ToString();
            decimal sasia = Convert.ToDecimal(row.Cells["Sasia"].Value);
            decimal cmimi = Convert.ToDecimal(row.Cells["Cmimi"].Value);
            decimal zbritjaAktuale = Convert.ToDecimal(row.Cells["Zbritja"].Value);

            string input = ShowInputDialog(
                "Zbritje në rresht",
                $"Jep zbritjen për njësi për '{emri}':",
                zbritjaAktuale.ToString("0.00")
                );

            if (input == null)
                return;

            decimal zbritjaRe = ParseDecimal(input);
            if (zbritjaRe < 0)
            {
                AutoClosingMessageBox.Show("Zbritja nuk mund të jetë më e vogël se zero", "Info", 900);
                return;
            }

            if (zbritjaRe > cmimi)
            {
                AutoClosingMessageBox.Show("Zbritja nuk mund të jetë më e madhe se çmimi.", "Info", 900);
                return;
            }

            decimal cmimiFinal = cmimi - zbritjaRe;
            if (cmimiFinal < 0)
                cmimiFinal = 0;

            decimal vlera = sasia * cmimiFinal;
            row.Cells["Zbritja"].Value = zbritjaRe;
            row.Cells["CmimiFinal"].Value = cmimiFinal;
            row.Cells["Vlera"].Value = vlera;

            _notifsService.Create("DISCOUNT_ITEM", "Info", "U aplikua zbritje në artikull.", $"Produkti: {emri}, Zbritja: {zbritjaRe:0.00} €/copë", "Artikujt", artikulliId, Session.UserId);
            CalculateTotal();
            BeginInvoke(new Action(() =>
            {
                txtBoxBarkodi.Clear();
                txtBoxBarkodi.Focus();
            }));
        }

        private void btnDiscountInvoice_Click(object sender, EventArgs e)
        {
            if (cartTable == null || cartTable.Rows.Count == 0)
            {
                AutoClosingMessageBox.Show("Nuk ka artikuj në faturë.", "Info", 900);
                return;
            }
            string input = ShowInputDialog(
                "Zbritje në faturë",
                "Jep zbritjen totale në faturë",
                invoiceDiscountAmount.ToString("0.00")
                );

            if (input == null)
                return;
            decimal discount = ParseDecimal(input);
            if (discount < 0)
            {
                MessageBox.Show("Zbritja nuk mund të jetë negative.");
                return;
            }

            decimal totalAfterItemDiscounts = 0m;
            foreach (DataRow row in cartTable.Rows)
            {
                totalAfterItemDiscounts += Convert.ToDecimal(row["Vlera"]);
            }

            if (discount > totalAfterItemDiscounts)
            {
                MessageBox.Show("Zbritja e faturës nuk mund të jetë më e madhe se totali aktual.");
                return;
            }

            invoiceDiscountAmount = discount;
            CalculateTotal();

            _notifsService.Create("DISCOUNT_INVOICE", "Info", "U aplikua zbritje në faturë", $"Zbritja totale e faturës: {invoiceDiscountAmount:0.00} €", "Shitjet", null, Session.UserId);

            BeginInvoke(new Action(() =>
            {
                txtBoxBarkodi.Clear();
                txtBoxBarkodi.Focus();
            }));
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            var returnRepo = new ReturnRepository();
            var returnService = new ReturnService(returnRepo);
            FrmReturns frm = new FrmReturns(returnService);
            frm.ShowDialog();
        }

        private void btnGiftCard_Click(object sender, EventArgs e)
        {
            if (Session.Role != "admin")
            {
                MessageBox.Show("Nuk keni qasje në menaxhimin e gift cards.");
                return;
            }

            using (var frm = new POSProject.views.products.FrmGiftCard())
            {
                frm.ShowDialog();
            }
        }
    }
}