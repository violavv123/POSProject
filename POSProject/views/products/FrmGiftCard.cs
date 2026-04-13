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
using POSProject.models;
using POSProject.repositories.products;
using POSProject.services.products;

namespace POSProject.views.products
{
    public partial class FrmGiftCard : Form
    {
        private readonly GiftCardService _giftCardService;
        public FrmGiftCard()
        {
            InitializeComponent();
            _giftCardService = new GiftCardService(new GiftCardRepository());
            this.Load += FrmGiftCard_Load;
            btnSearch.Click += btnSearch_Click;
            btnClear.Click += btnClear_Click;
        }

        public void FrmGiftCard_Load(object sender, EventArgs e)
        {
            SetupTransactionGrid();
            SetDefaultState();
        }
        private void SetupTransactionGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Created_At",
                HeaderText = "Data",
                DataPropertyName = "Created_At",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm"}
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipi",
                HeaderText = "Tipi",
                DataPropertyName = "Tipi",
                Width = 120
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Shuma",
                HeaderText = "Shuma",
                DataPropertyName = "Shuma",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ShitjaId",
                HeaderText = "ShitjaId",
                DataPropertyName = "ShitjaId",
                Width = 90
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Koment",
                HeaderText = "Koment",
                DataPropertyName = "Koment",
                Width = 220
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Created_By",
                HeaderText = "Krijuar nga",
                DataPropertyName = "Created_By",
                Width = 110
            });

        }
        private void SetDefaultState()
        {
            txtBoxKodi.Clear();
            txtBoxBarkodi.Clear();
            txtBoxShumaFillestare.Clear();
            txtBoxBilanciAktual.Clear();
            txtBoxCreatedAt.Clear();
            txtBoxCreatedBy.Clear();
            txtBoxShitjaId.Clear();

            txtBoxBilanciAktual.ReadOnly = true;
            txtBoxCreatedAt.ReadOnly = true;
            txtBoxCreatedBy.ReadOnly = true;
            txtBoxShitjaId.ReadOnly = true;

            txtBoxBarkodi.ReadOnly = true;
            txtBoxShumaFillestare.ReadOnly = true;
            dataGridView1.DataSource = null;
            txtBoxKodi.Focus();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            SetDefaultState();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtBoxKodi.Text.Trim();

                if (string.IsNullOrWhiteSpace(code))
                {
                    MessageBox.Show("Shkruaj kodin e gift card.");
                    txtBoxKodi.Focus();
                    return;
                }

                GiftCardModel card = _giftCardService.GetByCode(code);

                if (card == null)
                {
                    MessageBox.Show("Gift card nuk u gjet.");
                    return;
                }

                FillGiftCardForm(card);
                LoadGiftCardTransactions(card.Kodi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë kërkimit: " + ex.Message);
            }
        }

        private void FillGiftCardForm(GiftCardModel card)
        {
            txtBoxKodi.Text = card.Kodi;
            txtBoxBarkodi.Text = card.Barkodi ?? "";
            txtBoxShumaFillestare.Text = card.ShumaFillestare.ToString("0.00");
            txtBoxBilanciAktual.Text = card.BilanciAktual.ToString("0.00");

            txtBoxCreatedAt.Text = card.Created_At?.ToString("dd/MM/yyyy HH:mm") ?? "";
            txtBoxCreatedBy.Text = card.Created_By?.ToString() ?? "";
            txtBoxShitjaId.Text = card.ShitjaIdIssued?.ToString() ?? "";
        }

        private void LoadGiftCardTransactions(string code)
        {
            var transactions = _giftCardService.GetTransactionsByCode(code);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = transactions;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
