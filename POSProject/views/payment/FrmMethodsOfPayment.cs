using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Npgsql;
using POSProject.repositories.payments;
using POSProject.services.payments;
using POSProject.models;

namespace POSProject
{
    public partial class FrmMethodsOfPayment : Form
    {
        private int? selectedMethodId = null;
        private readonly IPaymentMethodService _paymentMethodService;
        public FrmMethodsOfPayment()
        {
            InitializeComponent();
            _paymentMethodService = new PaymentMethodService(new PaymentMethodRepository());
            txtBoxRendorja.KeyDown += txtBoxRendorja_KeyDown;
            txtBoxPershkrimi.KeyDown += txtBoxPershkrimi_KeyDown;
            txtBoxShkurtesa.KeyDown += txtBoxShkurtesa_KeyDown;
            chckBoxKerkon.CheckedChanged += chckBoxKerkon_CheckedChanged;
            chckBoxNukKerkon.CheckedChanged += chckBoxNukKerkon_CheckedChanged;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnSave.Click += btnSave_Click;
            btnClear.Click += btnClear_Click;
            btnClose.Click += btnClose_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;
            cmbBoxTipi.SelectedIndexChanged += cmbBoxTipi_SelectedIndexChanged;
            txtBoxPershkrimi.TextChanged += txtBoxPershkrimi_TextChanged;
        }

        private void FrmMethodsOfPayment_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadTipi();
            LoadValutaDefault();
            LoadMethods();
            ClearFields();
        }

        private void ConfigureGrid()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FormatGrid()
        {
            if (dataGridView1.Columns.Count == 0)
                return;

            if (dataGridView1.Columns.Contains("Id"))
                dataGridView1.Columns["Id"].Visible = false;

            if (dataGridView1.Columns.Contains("Created_At"))
                dataGridView1.Columns["Created_At"].Visible = false;

            if (dataGridView1.Columns.Contains("UpdatedAt"))
                dataGridView1.Columns["UpdatedAt"].Visible = false;

            dataGridView1.Columns["Rendorja"].HeaderText = "Rendorja";
            dataGridView1.Columns["Pershkrimi"].HeaderText = "Përshkrimi";
            dataGridView1.Columns["Shkurtesa"].HeaderText = "Shkurtesa";
            dataGridView1.Columns["KerkonReference"].HeaderText = "Kërkon Ref.";
            dataGridView1.Columns["Tipi"].HeaderText = "Tipi";
            dataGridView1.Columns["ValutaDefault"].HeaderText = "Valuta";
            dataGridView1.Columns["Aktiv"].HeaderText = "Aktiv";
        }

        private void LoadTipi()
        {
            cmbBoxTipi.Items.Clear();
            cmbBoxTipi.Items.Add("CASH");
            cmbBoxTipi.Items.Add("POS");
            cmbBoxTipi.Items.Add("BANK");
            cmbBoxTipi.Items.Add("VOUCHER");
            cmbBoxTipi.Items.Add("TRANSFER");
            cmbBoxTipi.Items.Add("TJETER");
            cmbBoxTipi.SelectedIndex = -1;
        }

        private void LoadValutaDefault()
        {
            try
            {
                DataTable dt = _paymentMethodService.GetCurrencies();
                cmbBoxValutaDefault.DataSource = dt;
                cmbBoxValutaDefault.DisplayMember = "Valuta";
                cmbBoxValutaDefault.ValueMember = "Valuta";
                cmbBoxValutaDefault.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ngarkimit të valutave: " + ex.Message);
            }
        }

        private void LoadMethods()
        {
            try
            {
                dataGridView1.DataSource = _paymentMethodService.GetMethods();
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ngarkimit të mënyrave të pagesës: " + ex.Message);
            }
        }

        private PaymentMethodModel GetMethodFromForm()
        {
            return new PaymentMethodModel
            {
                Id = selectedMethodId ?? 0,
                Rendorja = int.TryParse(txtBoxRendorja.Text.Trim(), out int rendorja) ? rendorja : 0,
                Pershkrimi = txtBoxPershkrimi.Text.Trim(),
                Shkurtesa = txtBoxShkurtesa.Text.Trim(),
                KerkonReference = chckBoxKerkon.Checked,
                Tipi = cmbBoxTipi.Text.Trim(),
                ValutaDefault = cmbBoxValutaDefault.Text.Trim()
            };
        }
      
        private void ClearFields()
        {
            selectedMethodId = null;
            txtBoxRendorja.Clear();
            txtBoxPershkrimi.Clear();
            txtBoxShkurtesa.Clear();
            cmbBoxTipi.SelectedIndex = -1;
            cmbBoxValutaDefault.SelectedIndex = -1;
            chckBoxKerkon.Checked = false;
            chckBoxNukKerkon.Checked = true;
            txtBoxRendorja.Focus();
        }
        private void txtBoxPershkrimi_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBoxPershkrimi.Text))
                return;
            if (string.IsNullOrWhiteSpace(txtBoxShkurtesa.Text))
            {
                txtBoxShkurtesa.Text = GenerateShkurtesa(txtBoxPershkrimi.Text);
                txtBoxShkurtesa.SelectionStart = txtBoxShkurtesa.Text.Length;
            }
        }

        private string GenerateShkurtesa(string pershkrimi)
        {
            var words = pershkrimi.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 1)
            {
                string word = words[0].ToUpper();
                return word.Length <= 5 ? word : word.Substring(0, 5);
            }
            return string.Concat(words.Select(w => char.ToUpper(w[0]))).ToUpper();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedMethodId == null)
            {
                MessageBox.Show("Zgjidh një rresht për përditësim.");
                return;
            }
            try
            {
                var method = GetMethodFromForm();
                var result = _paymentMethodService.Update(method);

                if (!result.Success)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                AutoClosingMessageBox.Show(result.Message, "Informacion", 900);
                LoadMethods();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë përditësimit të mënyrës së pagesës: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedMethodId == null)
            {
                MessageBox.Show("Zgjedh një rresht për fshirje.");
                return;
            }
            DialogResult resultConfirm = MessageBox.Show(
                "A jeni i sigurt që dëshironi të ç'aktivizoni këtë mënyrë pagese?",
                "Konfirmim",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultConfirm != DialogResult.Yes)
                return;

            try
            {
                var result = _paymentMethodService.Deactivate(selectedMethodId.Value, txtBoxPershkrimi.Text.Trim());

                if (!result.Success)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                AutoClosingMessageBox.Show(result.Message, "Informacion", 900);
                LoadMethods();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ç'aktivizimit të mënyrës së pagesës: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var method = GetMethodFromForm();
                var result = _paymentMethodService.Save(method);

                if (!result.Success)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                AutoClosingMessageBox.Show(result.Message, "Informacion", 900);
                LoadMethods();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ruajtjes së mënyrës së pagesës: " + ex.Message);
            }
        }
        private void txtBoxRendorja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtBoxPershkrimi.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxPershkrimi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtBoxShkurtesa.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxShkurtesa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                chckBoxKerkon.Checked = true;
                e.SuppressKeyPress = true;
            }
        }

        private void chckBoxKerkon_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBoxKerkon.Checked)
                chckBoxNukKerkon.Checked = false;
            else if (!chckBoxNukKerkon.Checked)
                chckBoxNukKerkon.Checked = true;
        }

        private void chckBoxNukKerkon_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBoxNukKerkon.Checked)
                chckBoxKerkon.Checked = false;
            else if (!chckBoxKerkon.Checked)
                chckBoxKerkon.Checked = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Rows.Count == 0)
                return;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (row.Cells["Id"].Value == null)
                return;
            selectedMethodId = Convert.ToInt32(row.Cells["Id"].Value);
            txtBoxRendorja.Text = row.Cells["Rendorja"].Value?.ToString() ?? "";
            txtBoxPershkrimi.Text = row.Cells["Pershkrimi"].Value?.ToString() ?? "";
            txtBoxShkurtesa.Text = row.Cells["Shkurtesa"].Value?.ToString() ?? "";
            string tipi = row.Cells["Tipi"].Value?.ToString() ?? "";
            string valutaDefault = row.Cells["ValutaDefault"].Value?.ToString() ?? "";
            bool kerkonReference = row.Cells["KerkonReference"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["KerkonReference"].Value);
            cmbBoxTipi.SelectedItem = tipi;
            if (cmbBoxValutaDefault.Items.Count > 0)
                cmbBoxValutaDefault.SelectedValue = valutaDefault;
            chckBoxKerkon.Checked = kerkonReference;
            chckBoxNukKerkon.Checked = !kerkonReference;
        }

        private void cmbBoxTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxTipi.SelectedItem == null)
                return;
            string tipi = cmbBoxTipi.SelectedItem.ToString();
            if (tipi == "CASH")
            {
                chckBoxKerkon.Checked = false;
                chckBoxNukKerkon.Checked = true;
            }
            else if (tipi == "POS" || tipi == "BANK" || tipi == "CARD" || tipi == "VOUCHER")
            {
                chckBoxKerkon.Checked = true;
                chckBoxNukKerkon.Checked = false;
            }
        }

    }
}
