using System.Data;
using Npgsql;
using NpgsqlTypes;
using POSProject.models;
using POSProject.repositories.auth;
using POSProject.repositories.sales;
using POSProject.services;
using POSProject.services.sales;
namespace POSProject
{
    public partial class FrmSalesList : Form
    {
        private AutoCompleteStringCollection cashierList = new AutoCompleteStringCollection();
        private readonly UserService _userService;
        private readonly SalesReportService _salesReportService;
        public FrmSalesList()
        {
            InitializeComponent();

            IUserRepository userRepo = new UserRepository();
            ISalesReportRepository salesReportRepo = new SalesReportRepository();

            _userService = new UserService(userRepo);
            _salesReportService = new SalesReportService(salesReportRepo);

            txtBoxName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBoxName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBoxName.TextChanged += txtBoxName_TextChanged;
            LoadCashierAutoComplete();
            this.AcceptButton = btnSearch;
        }

        private void FrmSalesList_Load(object sender, EventArgs e)
        {

            LoadCashierAutoComplete();

            datePickerFrom.MaxDate = DateTime.Today;
            datePickerTo.MaxDate = DateTime.Today;
            datePickerTo.Value = DateTime.Today;
            datePickerFrom.Value = DateTime.Today.AddDays(-7);
            datePickerFrom.Format = DateTimePickerFormat.Short;
            datePickerTo.Format = DateTimePickerFormat.Short;
            LoadSales();
            ShowTopSoldProducts();
        }

        private void txtBoxName_TextChanged(object sender, EventArgs e) 
        {
            string input = txtBoxName.Text.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                labelNoResult.Visible = false;
                return;
            }
            bool exactMatch = cashierList.Cast<string>().Any(x => x.Equals(input, StringComparison.OrdinalIgnoreCase));
            bool partialMatch = cashierList.Cast<string>().Any(x => x.StartsWith(input, StringComparison.OrdinalIgnoreCase));
            labelNoResult.Visible = !(exactMatch || partialMatch);
        }

        private void datePickerTo_ValueChanged(object sender, EventArgs e)
        {
            if (datePickerTo.Value < datePickerFrom.Value)
            {
                datePickerFrom.Value = datePickerTo.Value;
            }
        }

        private void datePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            if (datePickerFrom.Value > datePickerTo.Value)
            {
                datePickerTo.Value = datePickerFrom.Value;
            }
        }

        private void LoadCashierAutoComplete()
        {
            cashierList.Clear();

            List<string> cashiers = _userService.GetCashierUsernames();
            foreach (var cashier in cashiers)
            {
                cashierList.Add(cashier);
            }

            txtBoxName.AutoCompleteCustomSource = cashierList;
        }

        private SalesFilterModel BuildFilter()
        {
            return new SalesFilterModel
            {
                FromDate = datePickerFrom.Value.Date,
                ToDate = datePickerTo.Value.Date,
                Cashier = txtBoxName.Text.Trim()
            };
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            datePickerFrom.Value = DateTime.Today;
            datePickerTo.Value = DateTime.Today;
            SearchSales();
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);
            datePickerFrom.Value = yesterday;
            datePickerTo.Value = yesterday;
            SearchSales();
        }

        private void btn7Days_Click(object sender, EventArgs e)
        {
            datePickerFrom.Value = DateTime.Today.AddDays(-7);
            datePickerTo.Value = DateTime.Today;
            SearchSales();
        }

        private void btn30Days_Click(object sender, EventArgs e)
        {
            datePickerFrom.Value = DateTime.Today.AddDays(-30);
            datePickerTo.Value = DateTime.Today;
            SearchSales();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowTopSoldProducts();
            SearchSales();
        }

        private void SearchSales()
        {
            LoadSales();
        }

        private void LoadSales()
        {
            try
            {
                var filter = BuildFilter();

                if (string.IsNullOrWhiteSpace(filter.Cashier))
                    filter.Cashier = null;

                DataTable dt = _salesReportService.GetSales(filter);
                dataGridView1.DataSource = dt;

                ToggleNoSalesLabel();
                FormatSalesGrid();
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ngarkimit të shitjeve: " + ex.Message);
            }
        }

        private void ShowTopSoldProducts()
        {
            try
            {
                var filter = BuildFilter();
                DataTable dt = _salesReportService.GetTopSoldProducts(filter);
                dataGridView2.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ngarkimit të top produkteve: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            if (dataGridView2.Columns.Count == 0)
                return;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (dataGridView2.Columns["Cmimi"] != null)
                dataGridView2.Columns["Cmimi"].DefaultCellStyle.Format = "0.00";

            if (dataGridView2.Columns["TotaliFituar"] != null)
                dataGridView2.Columns["TotaliFituar"].DefaultCellStyle.Format = "0.00";
        }
        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Totali"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Totali"].Value);
                }
            }
            txtBoxTotali.Text = total.ToString("0.00");
        }

        private void ToggleNoSalesLabel()
        {
            bool hasRows = dataGridView1.Rows.Count > 0;
            labelNoSales.Visible = !hasRows;
            dataGridView1.Visible = true;
            labelNoSales.BringToFront();
        }

        private void FormatSalesGrid()
        {
            if (dataGridView1.Columns.Count == 0)
                return;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (dataGridView1.Columns["Totali"] != null)
                dataGridView1.Columns["Totali"].DefaultCellStyle.Format = "0.00";

            if (dataGridView1.Columns["DataShitjes"] != null)
                dataGridView1.Columns["DataShitjes"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            if (dataGridView1.Columns["ShiftId"] != null)
            {
                dataGridView1.Columns["ShiftId"].HeaderText = "Ndërrimi";
                dataGridView1.Columns["ShiftId"].Width = 80;
            }
        }

    }
}
