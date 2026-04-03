using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace POSProject
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            txtBoxPassword.PasswordChar = '*';  
            this.AcceptButton = btnLogin;
            txtBoxUsername.KeyDown += txtUsername_KeyDown;

            btnLogin.MouseEnter += btn_MouseEnter;
            btnLogin.MouseLeave += btn_MouseLeave;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
        }

        private DataRow GetUserByUsername(string username)
        {
            using(var connection = Db.GetConnection())
            {
                connection.Open();
                string query = @"SELECT ""id"", ""username"", ""password_hash"", ""role"", ""is_active""
                                FROM ""perdoruesit""
                                WHERE ""username"" = @username";

                using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    using (var da = new Npgsql.NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            return dt.Rows[0];
                        }
                    }
                }
            }
            return null;
        }

        private bool ValidateLogin()
        {
            string username = txtBoxUsername.Text.Trim();
            string password = txtBoxPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Shkruani usernamin dhe passwordin.");
                return false;
            }

            DataRow userRow = GetUserByUsername(username);
            if (userRow == null)
            {
                MessageBox.Show("Ky përdorues nuk ekziston.");
                return false;
            }

            bool isActive = Convert.ToBoolean(userRow["is_active"]);
            if (!isActive)
            {
                MessageBox.Show("Ky përdorues është jo aktiv. Kontaktoni administratorin.");
                return false;
            }

            string storedHash = userRow["password_hash"].ToString().Trim();

            bool isValid = PasswordHelper.VerifyPassword(password, storedHash);

            if (!isValid)
            {
                MessageBox.Show("Passwordi është jo i saktë. Provoni përsëri.");
                return false;
            }

            int userId = Convert.ToInt32(userRow["id"]);
            string role = userRow["role"].ToString();

            Session.Start(userId, username, role);
            UpdateLastLogin(userId);

            return true;
        }

        private void UpdateLastLogin(int userId)
        {
            using(var connection = Db.GetConnection())
            {
                connection.Open();
                string query = @"UPDATE ""perdoruesit"" SET ""last_login_at""=NOW() WHERE ""id""=@id";
                using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnMasking_Click(object sender, EventArgs e)
        {
            if(txtBoxPassword.PasswordChar == '\0')
            {
                txtBoxPassword.PasswordChar = '*';
            }
            else
            {
                txtBoxPassword.PasswordChar = '\0';
            }
        }
       
        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                txtBoxPassword.Focus();
                e.SuppressKeyPress = true;
            }
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
    }
}
