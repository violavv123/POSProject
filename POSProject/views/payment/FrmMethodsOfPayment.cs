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

namespace POSProject
{
    public partial class FrmMethodsOfPayment : Form
    {
        private int? selectedMethodId = null;
        public FrmMethodsOfPayment()
        {
            InitializeComponent();
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
                using (var conn = Db.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT ""Valuta""
                        FROM ""KursetKembimit""
                        WHERE ""Aktiv"" = TRUE
                        ORDER BY ""Valuta"";";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cmbBoxValutaDefault.DataSource = dt;
                        cmbBoxValutaDefault.DisplayMember = "Valuta";
                        cmbBoxValutaDefault.ValueMember = "Valuta";
                        cmbBoxValutaDefault.SelectedIndex = -1;
                    }
                }
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
                using (var conn = Db.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            ""Id"",
                            ""Rendorja"",
                            ""Pershkrimi"",
                            ""Shkurtesa"",
                            ""KerkonReference"",
                            ""Tipi"",
                            ""ValutaDefault"",
                            ""Aktiv"",
                            ""Created_At"",
                            ""Updated_At""
                        FROM ""MenyratPageses""
                        ORDER BY ""Rendorja"", ""Pershkrimi"";";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }

                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ngarkimit të mënyrave të pagesës: " + ex.Message);
            }
        }

        private bool CheckDuplicatePershkrimi()
        {
            try
            {
                using (var conn = Db.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*)
                                     FROM ""MenyratPageses""
                                     WHERE LOWER(TRIM(""Pershkrimi"")) = LOWER(TRIM(@Pershkrimi))
                                     AND (@Id IS NULL OR ""Id"" <> @Id;";
                    using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Pershkrimi", txtBoxPershkrimi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Id", (object?)selectedMethodId ?? DBNull.Value);
                        long count = (long)cmd.ExecuteScalar();
                        if(count > 0)
                        {
                            AutoClosingMessageBox.Show("Ekziston një mënyrë pagese me të njëjtin përshkrim.", "Informacion", 1000);
                            txtBoxPershkrimi.Focus();
                            return false;
                        }
                    }
                }
                return true;
            }catch(Exception ex)
            {
                MessageBox.Show("Gabim gjatë verifikimit të përshkrimit: " + ex.Message);
                return false;
            }
        }

        private bool CheckDuplicateRendorja()
        {
            try
            {
                using (var conn = Db.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*)
                                    FROM ""MenyratPageses""
                                    WHERE LOWER(TRIM(""Rendorja"")) = LOWER(TRIM(@Rendorja))
                                    AND (@Id IS NULL OR ""Id"" <> @Id;";
                    using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Rendorja", int.Parse(txtBoxRendorja.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Id", (object?)selectedMethodId ?? DBNull.Value);
                        long count = (long)cmd.ExecuteScalar();
                        if(count > 0)
                        {
                            AutoClosingMessageBox.Show("Ekziston tashmë një mënyrë pagese me të njëjtin numër rendor.", "Informacion", 1000);
                            txtBoxRendorja.Focus();
                            return false;
                        }
                    }
                }
                return true;
            }catch(Exception ex)
            {
                MessageBox.Show("Gabim gjatë verifikimit të rendores: " + ex.Message);
                return false;
            }
        }
        private bool CheckDuplicateShkurtesa()
        {
            try
            {
                using (var conn = Db.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*)
                                    FROM ""MenyratPageses""
                                    WHERE LOWER(TRIM(""Shkurtesa"")) = LOWER(TRIM(@Shkurtesa))
                                    AND (@ID IS NULL OR ""Id"" <> @Id;";
                    using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Shkurtesa", txtBoxShkurtesa.Text.Trim());
                        cmd.Parameters.AddWithValue("@Id", (object?)selectedMethodId ?? DBNull.Value);
                        long count = (long)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            AutoClosingMessageBox.Show("Ekziston tashmë një mënyrë pagese me të njëjtën shkurtesë.", "Informacion", 1000);
                            txtBoxShkurtesa.Focus();
                            return false;
                        }
                    }
                }
            return true;
            }catch(Exception ex){
                MessageBox.Show("Gabim gjatë verifikimit të shkurtesës: " + ex.Message);
                return false;
            }
        }
        private bool ValidateMethod()
        {
            if (string.IsNullOrWhiteSpace(txtBoxRendorja.Text))
            {
                MessageBox.Show("Rendorja është e detyrueshme.");
                txtBoxRendorja.Focus();
                return false;
            }

            if (!int.TryParse(txtBoxRendorja.Text.Trim(), out int rendorja) || rendorja <= 0)
            {
                MessageBox.Show("Rendorja duhet të jetë numër pozitiv.");
                txtBoxRendorja.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBoxPershkrimi.Text))
            {
                MessageBox.Show("Përshkrimi është i detyrueshëm.");
                txtBoxPershkrimi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBoxShkurtesa.Text))
            {
                MessageBox.Show("Shkurtesa është e detyrueshme.");
                txtBoxShkurtesa.Focus();
                return false;
            }

            if (cmbBoxTipi.SelectedIndex == -1)
            {
                MessageBox.Show("Zgjidh tipin.");
                cmbBoxTipi.Focus();
                return false;
            }

            if (cmbBoxValutaDefault.SelectedIndex == -1)
            {
                MessageBox.Show("Zgjidh valutën default.");
                cmbBoxValutaDefault.Focus();
                return false;
            }

            if (!CheckDuplicatePershkrimi())
                return false;

            if (!CheckDuplicateShkurtesa())
                return false;

            if (!CheckDuplicateRendorja())
                return false;

            return true;
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
            if(words.Length == 1)
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
            if(selectedMethodId == null)
            {
                MessageBox.Show("Zgjidh një rresht për përditësim.");
                return;
            }
            if (!ValidateMethod())
                return;
            try
            {
                using (var conn = Db.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE ""MenyratPageses""
                                     SET ""Pershkrimi"" = @Pershkrimi,
                                         ""Shkurtesa"" = @Shkurtesa,
                                         ""KerkonReference"" = @KerkonReference,
                                         ""Tipi"" = @Tipi,
                                         ""ValutaDefault"" = @ValutaDefault,
                                         ""Rendorja"" = @Rendorja,
                                         ""Updated_At"" = CURRENT_TIMESTAMP
                                         WHERE ""Id"" = @Id;";
                    using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Pershkrimi", txtBoxPershkrimi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Shkurtesa", txtBoxShkurtesa.Text.Trim());
                        cmd.Parameters.AddWithValue("@KerkonReference", chckBoxKerkon.Checked);
                        cmd.Parameters.AddWithValue("@Tipi", cmbBoxTipi.Text.Trim());
                        cmd.Parameters.AddWithValue("@ValutaDefault", cmbBoxValutaDefault.Text.Trim());
                        cmd.Parameters.AddWithValue("@Rendorja", int.Parse(txtBoxRendorja.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Id", selectedMethodId.Value);
                        cmd.ExecuteNonQuery();
                    }
                    AutoClosingMessageBox.Show("Mënyra e pagesës u përditësua me sukses.", "Informacion", 900);
                    NotificationService.Create("PAYMENT_METHOD_UPDATED", "Info", "Mënyrë pagese e përditësuar", txtBoxPershkrimi.Text, "MenyratPageses", selectedMethodId.Value, Session.UserId);
                    LoadMethods();
                    ClearFields();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Gabim gjatë përditësimit të mënyrës së pagesës: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(selectedMethodId == null)
            {
                MessageBox.Show("Zgjedh një rresht për fshirje.");
                return;
            }
            DialogResult result = MessageBox.Show(
                "A jeni i sigurt që dëshironi të ç'aktivizoni këtë mënyrë pagese?",
                "Konfirmim",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            try
            {
                using (var conn = Db.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE ""MenyratPageses""
                                     SET ""Aktiv"" = FALSE,
                                         ""Updated_At"" = CURRENT_TIMESTAMP
                                     WHERE ""Id"" = @Id;";
                    using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", selectedMethodId.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                AutoClosingMessageBox.Show("Mënyra e pagesës u ç'aktivizua me sukses.", "Informacion", 900);
                NotificationService.Create("PAYMENT_METHOD_DEACTIVATED", "Info", "Mënyrë pagese e ç'aktivizuar", txtBoxPershkrimi.Text, "MenyratPageses", selectedMethodId.Value, Session.UserId);
                LoadMethods();
                ClearFields();
            }catch(Exception ex)
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
            if (!ValidateMethod())
                return;
            try
            {
                using (var conn = Db.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO ""MenyratPageses""
                                     (""Pershkrimi"", ""Shkurtesa"", ""Tipi"", ""ValutaDefault"", ""KerkonReference"", ""Aktiv"", ""Created_At"", ""Updated_At"", ""Rendorja"") 
                                     VALUES (@Pershkrimi, @Shkurtesa, @Tipi , @ValutaDefault, @KerkonReference, TRUE, CURRENT_TIMESTAMP, NULL, @Rendorja);";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Pershkrimi", txtBoxPershkrimi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Shkurtesa", txtBoxShkurtesa.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tipi", cmbBoxTipi.Text.Trim());
                        cmd.Parameters.AddWithValue("@ValutaDefault", cmbBoxValutaDefault.Text.Trim());
                        cmd.Parameters.AddWithValue("@KerkonReference", chckBoxKerkon.Checked);
                        cmd.Parameters.AddWithValue("@Rendorja", int.Parse(txtBoxRendorja.Text.Trim()));
                        cmd.ExecuteNonQuery();
                    }
                    AutoClosingMessageBox.Show("Mënyra e pagesës u ruajt me sukses.", "Informacion", 900);
                    NotificationService.Create("PAYMENT_METHOD_ADDED", "Info", "Mënyrë pagese e re", txtBoxPershkrimi.Text, "MenyratPageses", null, Session.UserId);
                    LoadMethods();
                    ClearFields();        
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Gabim gjatë ruajtjes së mënyrës së pagesës: " + ex.Message);
            }
        }
        private void txtBoxRendorja_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                txtBoxPershkrimi.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxPershkrimi_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                txtBoxShkurtesa.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtBoxShkurtesa_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
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
            if(tipi == "CASH")
            {
                chckBoxKerkon.Checked = false;
                chckBoxNukKerkon.Checked = true;
            }else if (tipi == "POS" || tipi == "BANK" || tipi == "CARD"|| tipi == "VOUCHER")
            {
                chckBoxKerkon.Checked = true;
                chckBoxNukKerkon.Checked = false;
            }
        }
    }
}
