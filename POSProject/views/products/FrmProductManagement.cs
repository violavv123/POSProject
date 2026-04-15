using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using POSProject.models;
using POSProject.repositories.notifications;
using POSProject.repositories.products;
using POSProject.services.notifications;
using POSProject.services.products;

namespace POSProject
{
    public partial class FrmProductManagement : Form
    {
        private int? selectedArtikulliId = null;
        private bool isDeleteMode = false;
        private AutoCompleteStringCollection productList = new AutoCompleteStringCollection();
        private readonly INotificationService _notifsService;
        private readonly ProductService _productService;
        private readonly ICategoryService _categoryService;
        public FrmProductManagement()
        {
            InitializeComponent();

            INotificationRepository notifsRepo = new NotificationRepository();
            _notifsService = new NotificationService(notifsRepo);

            IProductRepository productRepo = new ProductRepository();
            _productService = new ProductService(productRepo);

            ICategoryRepository categoryRepo = new CategoryRepository();
            _categoryService = new CategoryService(categoryRepo);

            this.Load += FrmProductManagement_Load;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnRemove.Click += btnRemove_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;
            txtBoxName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBoxName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            chckBxAktiv.CheckedChanged += chckBxAktiv_CheckedChanged;
            chckBxJoAktiv.CheckedChanged += chckBxJoAktiv_CheckedChanged;
            txtBoxName.KeyDown += txtName_KeyDown;
            txtBoxBarcode.KeyDown += txtBarcode_KeyDown;
            txtBoxCmimi.KeyDown += txtCmimi_KeyDown;
            txtBoxSasia.KeyDown += txtSasia_KeyDown;
        }

        private void FrmProductManagement_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadCategories();
            LoadProducts();
            LoadProductAutoComplete();
            ClearFields();
        }

        private void SetupGrid()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadCategories()
        {
            var categories = _categoryService.GetAll();

            comboKategoria.DataSource = null;
            comboKategoria.Items.Clear();
            comboKategoria.DisplayMember = "Emri";
            comboKategoria.ValueMember = "Id";
            comboKategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            comboKategoria.DataSource = categories;
            comboKategoria.SelectedIndex = -1;
        }

        private void LoadProducts()
        {
            var products = _productService.GetAllForManagement();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products;

            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;

            if (dataGridView1.Columns["KategoriaId"] != null)
                dataGridView1.Columns["KategoriaId"].Visible = false;

            if (dataGridView1.Columns["Barkodi"] != null)
                dataGridView1.Columns["Barkodi"].HeaderText = "Barkodi";

            if (dataGridView1.Columns["Emri"] != null)
                dataGridView1.Columns["Emri"].HeaderText = "Emri";

            if (dataGridView1.Columns["Kategoria"] != null)
                dataGridView1.Columns["Kategoria"].HeaderText = "Kategoria";

            if (dataGridView1.Columns["CmimiShitjes"] != null)
                dataGridView1.Columns["CmimiShitjes"].HeaderText = "Çmimi";

            if (dataGridView1.Columns["SasiaNeStok"] != null)
                dataGridView1.Columns["SasiaNeStok"].HeaderText = "Sasia";

            if (dataGridView1.Columns["Aktiv"] != null)
                dataGridView1.Columns["Aktiv"].HeaderText = "Aktiv";
        }

        private void LoadProductAutoComplete()
        {
            productList.Clear();

            List<string> names = _productService.GetProductNames();
            foreach (var name in names)
            {
                productList.Add(name);
            }

            txtBoxName.AutoCompleteCustomSource = productList;
        }

        private bool ValidateProduct()
        {
            if (string.IsNullOrWhiteSpace(txtBoxName.Text))
            {
                AutoClosingMessageBox.Show("Shkruaj emrin e produktit.", "Informacion", 1200);
                txtBoxName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBoxBarcode.Text))
            {
                AutoClosingMessageBox.Show("Shkruaj barkodin.", "Informacion", 1200);
                txtBoxBarcode.Focus();
                return false;
            }

            if (comboKategoria.SelectedIndex == -1)
            {
                AutoClosingMessageBox.Show("Zgjedh kategorinë.", "Informacion", 1200);
                comboKategoria.Focus();
                return false;
            }

            if (!decimal.TryParse(txtBoxCmimi.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cmimi) &&
                !decimal.TryParse(txtBoxCmimi.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out cmimi))
            {
                AutoClosingMessageBox.Show("Çmimi nuk është valid.", "Informacion", 1200);
                txtBoxCmimi.Focus();
                return false;
            }

            if (cmimi < 0)
            {
                AutoClosingMessageBox.Show("Çmimi nuk mund të jetë negativ.", "Informacion", 1200);
                txtBoxCmimi.Focus();
                return false;
            }

            if (!decimal.TryParse(txtBoxSasia.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal sasia) &&
                !decimal.TryParse(txtBoxSasia.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out sasia))
            {
                AutoClosingMessageBox.Show("Sasia nuk është valide.", "Informacion", 1200);
                txtBoxSasia.Focus();
                return false;
            }

            if (sasia < 0)
            {
                AutoClosingMessageBox.Show("Sasia nuk mund të jetë negative.", "Informacion", 1200);
                txtBoxSasia.Focus();
                return false;
            }

            if (!chckBxAktiv.Checked && !chckBxJoAktiv.Checked)
            {
                AutoClosingMessageBox.Show("Zgjedh statusin Aktiv/Jo.", "Informacion", 1200);
                return false;
            }

            return true;
        }

        private void ClearFields()
        {
            selectedArtikulliId = null;
            isDeleteMode = false;

            txtBoxName.Clear();
            txtBoxBarcode.Clear();
            txtBoxCmimi.Clear();
            txtBoxSasia.Clear();
            comboKategoria.SelectedIndex = -1;
            chckBxAktiv.Checked = false;
            chckBxJoAktiv.Checked = false;

            dataGridView1.ClearSelection();

            btnEdit.Enabled = false;
            btnRemove.Enabled = false;

            SetNormalMode();
            txtBoxName.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            selectedArtikulliId = Convert.ToInt32(row.Cells["Id"].Value);
            isDeleteMode = false;

            txtBoxBarcode.Text = row.Cells["Barkodi"].Value?.ToString() ?? "";
            txtBoxName.Text = row.Cells["Emri"].Value?.ToString() ?? "";
            txtBoxCmimi.Text = Convert.ToDecimal(row.Cells["CmimiShitjes"].Value).ToString("0.00");
            txtBoxSasia.Text = Convert.ToDecimal(row.Cells["SasiaNeStok"].Value).ToString("0.##");

            if (row.Cells["KategoriaId"].Value != DBNull.Value)
                comboKategoria.SelectedValue = Convert.ToInt32(row.Cells["KategoriaId"].Value);
            else
                comboKategoria.SelectedIndex = -1;

            bool aktiv = row.Cells["Aktiv"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Aktiv"].Value);
            chckBxAktiv.Checked = aktiv;
            chckBxJoAktiv.Checked = !aktiv;

            SetNormalMode();
            btnEdit.Enabled = true;
            btnRemove.Enabled = true;

            txtBoxName.Focus();
            txtBoxName.SelectionStart = txtBoxName.Text.Length;
        }

        private void SetDeleteMode(bool deleteMode)
        {
            txtBoxName.Visible = true;
            labelName.Visible = true;

            txtBoxBarcode.Visible = !deleteMode;
            labelBarcode.Visible = !deleteMode;

            comboKategoria.Visible = !deleteMode;
            labelKategoria.Visible = !deleteMode;

            txtBoxCmimi.Visible = !deleteMode;
            labelCmimi.Visible = !deleteMode;

            txtBoxSasia.Visible = !deleteMode;
            labelSasia.Visible = !deleteMode;

            chckBxAktiv.Visible = !deleteMode;
            chckBxJoAktiv.Visible = !deleteMode;
            labelAktiv.Visible = !deleteMode;

            if (deleteMode)
            {
                txtBoxBarcode.Clear();
                txtBoxCmimi.Clear();
                txtBoxSasia.Clear();
                comboKategoria.SelectedIndex = -1;
                chckBxAktiv.Checked = false;
                chckBxJoAktiv.Checked = false;
            }

            btnAdd.Enabled = !deleteMode;
            btnEdit.Enabled = !deleteMode && selectedArtikulliId != null;
            btnRemove.Enabled = selectedArtikulliId != null;
        }

        private void SetNormalMode()
        {
            txtBoxName.Visible = true;
            labelName.Visible = true;

            txtBoxBarcode.Visible = true;
            labelBarcode.Visible = true;

            comboKategoria.Visible = true;
            labelKategoria.Visible = true;

            txtBoxCmimi.Visible = true;
            labelCmimi.Visible = true;

            txtBoxSasia.Visible = true;
            labelSasia.Visible = true;

            chckBxAktiv.Visible = true;
            chckBxJoAktiv.Visible = true;
            labelAktiv.Visible = true;

            btnAdd.Enabled = true;
        }

        private bool ProductExists(string barcode, string emri)
        {
            return _productService.ExistsByBarcodeOrName(barcode, emri);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateProduct())
                return;

            string barkodi = txtBoxBarcode.Text.Trim();
            string emri = txtBoxName.Text.Trim();

            try
            {
                if (ProductExists(barkodi, emri))
                {
                    MessageBox.Show("Ekziston tashmë një produkt me emër ose barkod të njëjtë.");
                    return;
                }

                var product = new ProductModel
                {
                    Barkodi = barkodi,
                    Emri = emri,
                    KategoriaId = Convert.ToInt32(comboKategoria.SelectedValue),
                    CmimiShitjes = decimal.Parse(txtBoxCmimi.Text),
                    SasiaNeStok = decimal.Parse(txtBoxSasia.Text),
                    Aktiv = chckBxAktiv.Checked
                };

                _productService.Add(product);

                AutoClosingMessageBox.Show("Produkti u ruajt me sukses.", "Informacion", 900);
                _notifsService.Create("PRODUCT_CREATED", "Info", "Produkt i ri", txtBoxName.Text, "Artikujt", null, Session.UserId);

                LoadProducts();
                LoadProductAutoComplete();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ruajtjes: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedArtikulliId == null)
            {
                MessageBox.Show("Zgjedh një produkt nga lista.");
                return;
            }

            if (!ValidateProduct())
                return;

            try
            {
                var product = new ProductModel
                {
                    Id = selectedArtikulliId.Value,
                    Barkodi = txtBoxBarcode.Text.Trim(),
                    Emri = txtBoxName.Text.Trim(),
                    KategoriaId = Convert.ToInt32(comboKategoria.SelectedValue),
                    CmimiShitjes = decimal.Parse(txtBoxCmimi.Text),
                    SasiaNeStok = decimal.Parse(txtBoxSasia.Text),
                    Aktiv = chckBxAktiv.Checked
                };

                _productService.Update(product);

                AutoClosingMessageBox.Show("Produkti u përditësua me sukses.", "Informacion", 900);
                _notifsService.Create("PRODUCT_UPDATED", "Info", "Produkt i përditësuar", txtBoxName.Text, "Artikujt", selectedArtikulliId, Session.UserId);

                LoadProducts();
                LoadProductAutoComplete();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë editimit: " + ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (selectedArtikulliId == null)
            {
                MessageBox.Show("Zgjedh një produkt nga lista.");
                return;
            }

            if (!isDeleteMode)
            {
                isDeleteMode = true;
                SetDeleteMode(true);
                txtBoxName.Focus();

                AutoClosingMessageBox.Show(
                    "Delete mode aktiv. Kliko përsëri Fshi produkt për konfirmim.",
                    "Informacion",
                    2000
                );
                return;
            }

            DialogResult result = MessageBox.Show(
                "A jeni të sigurt që dëshironi të fshini këtë produkt?",
                "Konfirmim",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                int idToDelete = selectedArtikulliId.Value;
                string deletedName = txtBoxName.Text;

                _productService.Delete(idToDelete);

                AutoClosingMessageBox.Show("Produkti u fshi me sukses.", "Informacion", 900);
                _notifsService.Create("PRODUCT_DELETED", "Warning", "Produkt i fshirë", deletedName, "Artikujt", idToDelete, Session.UserId);

                LoadProducts();
                LoadProductAutoComplete();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë fshirjes: " + ex.Message);
            }
        }

        private void chckBxAktiv_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBxAktiv.Checked)
                chckBxJoAktiv.Checked = false;
        }

        private void chckBxJoAktiv_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBxJoAktiv.Checked)
                chckBxAktiv.Checked = false;
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtBoxBarcode.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                comboKategoria.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtCmimi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtBoxSasia.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtSasia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                chckBxAktiv.Checked = true;
                e.SuppressKeyPress = true;
            }
        }

        
    }
}
