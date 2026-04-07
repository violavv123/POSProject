using System.Data;
using Npgsql;
using NpgsqlTypes;
namespace POSProject
{
    public partial class FrmSalesList : Form
    {
        private AutoCompleteStringCollection cashierList = new AutoCompleteStringCollection();
        public FrmSalesList()
        {
            InitializeComponent();
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
            using (var connection = Db.GetConnection())
            {
                connection.Open();
                string query = @"SELECT ""username"" 
                                FROM ""perdoruesit""
                                WHERE ""role"" = 'cashier'
                                ORDER BY ""username""";

                using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cashierList.Add(reader["username"].ToString());
                    }
                }
                txtBoxName.AutoCompleteCustomSource = cashierList;
            }
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
            if (string.IsNullOrWhiteSpace(txtBoxName.Text))
                LoadSales();
            else
                LoadSalesByName();
        }

        private void LoadSales()
        {
            try
            {
                using (var connection = Db.GetConnection())
                {
                    connection.Open();

                    string query = @"SELECT 
                            s.""NrFatures"" AS ""NrFatures"",
                            s.""DataShitjes"" AS ""DataShitjes"",
                            p.""username"" AS ""Cashier"",
                            s.""ShiftId"" AS ""ShiftId"",
                            s.""Totali"" AS ""Totali"",
                            s.""Koment"" AS ""Koment""
                            FROM ""Shitjet"" s
                            INNER JOIN ""perdoruesit"" p
                              ON s.""perdoruesi_id"" = p.""id""
                            LEFT JOIN ""CashierShifts"" cs
                              ON s.""ShiftId"" = cs.""Id""
                            WHERE s.""DataShitjes""::date >= @fromDate
                              AND s.""DataShitjes""::date <= @toDate
                            ORDER BY s.""DataShitjes"" DESC, s.""Id"" DESC";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@fromDate", datePickerFrom.Value.Date);
                        cmd.Parameters.AddWithValue("@toDate", datePickerTo.Value.Date);

                        using (var da = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }

                ToggleNoSalesLabel();
                FormatSalesGrid();
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ngarkimit të shitjeve: " + ex.Message);
            }
        }

        private void LoadSalesByName()
        {
            string cashier = txtBoxName.Text.Trim();

            if (string.IsNullOrWhiteSpace(cashier))
            {
                LoadSales();
                return;
            }

            try
            {
                using (var connection = Db.GetConnection())
                {
                    connection.Open();

                    string query = @"SELECT
                            s.""NrFatures"" AS ""NrFatures"",
                            s.""DataShitjes"" AS ""DataShitjes"",
                            p.""username"" AS ""Cashier"",
                            s.""ShiftId"" AS ""ShiftId"",
                            s.""Totali"" AS ""Totali"",
                            s.""Koment"" AS ""Koment""
                            FROM ""Shitjet"" s
                            INNER JOIN ""perdoruesit"" p
                              ON s.""perdoruesi_id"" = p.""id""
                            LEFT JOIN ""CashierShifts"" cs
                              ON s.""ShiftId"" = cs.""Id""
                            WHERE s.""DataShitjes""::date >= @fromDate
                              AND s.""DataShitjes""::date <= @toDate
                              AND p.""username"" ILIKE @cashier
                            ORDER BY s.""DataShitjes"" DESC, s.""Id"" DESC";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@fromDate", datePickerFrom.Value.Date);
                        cmd.Parameters.AddWithValue("@toDate", datePickerTo.Value.Date);
                        cmd.Parameters.AddWithValue("@cashier", cashier + "%");

                        using (var da = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }

                ToggleNoSalesLabel();
                FormatSalesGrid();
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë kërkimit të shitjeve: " + ex.Message);
            }
        }

        private void ShowTopSoldProducts()
        {
            try
            {
                using(var connection = Db.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT
                                    a.""Emri"" AS ""Produkti"",
                                    a.""Barkodi"" AS ""Barkodi"",
                                    k.""Emri"" AS ""Kategoria"",
                                    SUM(sd.""Sasia"") AS ""SasiaShitur"",
                                    a.""CmimiShitjes"" AS ""Cmimi"",
                                    SUM(sd.""Vlera"") AS ""TotaliFituar"",
                                    a.""SasiaNeStok"" AS ""StokuMbetur""
                                    FROM ""ShitjetDetale"" sd
                                    INNER JOIN ""Shitjet"" s
                                    ON sd.""ShitjaId"" = s.""Id""
                                    INNER JOIN ""Artikujt"" a
                                    ON sd.""ArtikulliId"" = a.""Id""
                                    LEFT JOIN ""kategorite"" k
                                    ON a.""KategoriaId"" = k.""id""
                                    WHERE s.""DataShitjes""::date >= @fromDate
                                    AND s.""DataShitjes""::date <= @toDate
                                    GROUP BY
                                    a.""Id"",
                                    a.""Emri"",
                                    a.""Barkodi"",
                                    k.""Emri"",
                                    a.""CmimiShitjes"",
                                    a.""SasiaNeStok""
                                    ORDER BY SUM(sd.""Sasia"") DESC, SUM(sd.""Vlera"") DESC;";
                    using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@fromDate", NpgsqlDbType.Date).Value = datePickerFrom.Value.Date;
                        cmd.Parameters.Add("@toDate", NpgsqlDbType.Date).Value = datePickerTo.Value.Date;

                        using (var da = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView2.DataSource = dt;
                        }
                    }
                }
                FormatGrid();
            }catch(Exception ex)
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
