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
        private readonly bool _selectionMode;
        public string SelectedGiftCardCode { get; private set; }
        public bool CardSelected { get; private set; } = false;
        public FrmGiftCard(bool selectionMode = false)
        {
            InitializeComponent();
            _giftCardService = new GiftCardService(new GiftCardRepository());
            _selectionMode = selectionMode;
            this.Load += FrmGiftCard_Load;
            btnSearch.Click += btnSearch_Click;
            btnClear.Click += btnClear_Click;
            btnUseCard.Click += btnUseCard_Click;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        public void FrmGiftCard_Load(object sender, EventArgs e)
        {
            SetupGiftCardGrid();
            SetDefaultState();

            btnUseCard.Visible = _selectionMode;

            LoadGiftCards();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["Kodi"];
                FillSelectedRowData();
            }
        }
        
        private void FillSelectedRowData()
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.DataBoundItem == null)
                return;

            var card = dataGridView1.CurrentRow.DataBoundItem as GiftCardModel;
            if (card == null)
                return;

            FillGiftCardForm(card);
        }
        private void  dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            FillSelectedRowData();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_selectionMode || e.RowIndex < 0)
                return;

            btnUseCard.PerformClick();
        }
        private void SetupGiftCardGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Kodi",
                HeaderText = "Kodi",
                DataPropertyName = "Kodi",
                Width = 160
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Barkodi",
                HeaderText = "Barkodi",
                DataPropertyName = "Barkodi",
                Width = 160
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ShumaFillestare",
                HeaderText = "Shuma Fillestare",
                DataPropertyName = "ShumaFillestare",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BilanciAktual",
                HeaderText = "Bilanci Aktual",
                DataPropertyName = "BilanciAktual",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 100
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Created_At",
                HeaderText = "Krijuar më",
                DataPropertyName = "Created_At",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ShitjaIdIssued",
                HeaderText = "Shitja ID",
                DataPropertyName = "ShitjaIdIssued",
                Width = 90
            });
        }

        private void LoadGiftCards()
        {
            try
            {
                var cards = _giftCardService.GetAll(false);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = cards;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ngarkimit të gift cards: " + ex.Message);
            }
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

            txtBoxBarkodi.ReadOnly = true;
            txtBoxShumaFillestare.ReadOnly = true;
            txtBoxBilanciAktual.ReadOnly = true;
            txtBoxCreatedAt.ReadOnly = true;
            txtBoxCreatedBy.ReadOnly = true;
            txtBoxShitjaId.ReadOnly = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxKodi.Clear();
            LoadGiftCards();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["Kodi"];
                FillSelectedRowData();
            }
            else
            {
                SetDefaultState();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string code = txtBoxKodi.Text.Trim();

            if (string.IsNullOrWhiteSpace(code))
            {
                LoadGiftCards();
                return;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string rowCode = dataGridView1.Rows[i].Cells["Kodi"].Value?.ToString() ?? "";

                if (rowCode.Equals(code, StringComparison.OrdinalIgnoreCase))
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["Kodi"];
                    FillSelectedRowData();
                    return;
                }
            }

            MessageBox.Show("Gift card nuk u gjet në listë.");
        }

        private void FillGiftCardForm(GiftCardModel card)
        {
            txtBoxKodi.Text = card.Kodi ?? "";
            txtBoxBarkodi.Text = card.Barkodi ?? "";
            txtBoxShumaFillestare.Text = card.ShumaFillestare.ToString("0.00");
            txtBoxBilanciAktual.Text = card.BilanciAktual.ToString("0.00");
            txtBoxCreatedAt.Text = card.Created_At?.ToString("dd/MM/yyyy HH:mm") ?? "";
            txtBoxCreatedBy.Text = card.Created_By?.ToString() ?? "";
            txtBoxShitjaId.Text = card.ShitjaIdIssued?.ToString() ?? "";
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUseCard_Click(object sender, EventArgs e)
        {
            if (!_selectionMode)
                return;

            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Zgjedh një gift card nga lista.");
                return;
            }

            var card = dataGridView1.CurrentRow.DataBoundItem as GiftCardModel;
            if (card == null)
            {
                MessageBox.Show("Gift card nuk u gjet.");
                return;
            }

            if (!card.Statusi.Trim().Equals("Aktiv", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Gift card nuk është aktive.");
                return;
            }

            if (card.Expires_At.HasValue && card.Expires_At.Value < DateTime.Now)
            {
                MessageBox.Show("Gift card ka skaduar.");
                return;
            }

            if (card.BilanciAktual <= 0)
            {
                MessageBox.Show("Gift card nuk ka bilanc.");
                return;
            }

            SelectedGiftCardCode = card.Kodi;
            CardSelected = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
    
}
