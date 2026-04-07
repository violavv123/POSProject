using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSProject
{
    public partial class FrmSignUp : Form
    {
        public FrmSignUp()
        {
            InitializeComponent();
            txtBoxPassword.PasswordChar = '*';
            txtBoxConfirm.PasswordChar = '*';
            this.AcceptButton = btnSignUp;
            txtBoxUsername.KeyDown += txtUsername_KeyDown;
            txtBoxPassword.KeyDown += txtPassword_KeyDown;
        }

        private void FrmSignUp_Load(object sender, EventArgs e)
        {
            if(Session.Role != "admin")
            {
                MessageBox.Show("Nuk keni akses në këtë faqe.");
                this.Close();
            }
        }

        private bool UsernameExists(string username)
        {
            using(var connection = Db.GetConnection())
            {
                connection.Open();
                string query = @"SELECT COUNT(*) FROM ""perdoruesit"" WHERE ""username""=@username";

                using(var cmd = new Npgsql.NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void CreateUser(string username, string passwordHash)
        {
            using (var connection = Db.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO ""perdoruesit""
                               (""username"",""password_hash"",""role"",""is_active"",""created_at"")
                               VALUES (@username, @passwordHash, @role, TRUE, NOW())";

                using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@role", "cashier");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool ValidateSignUp()
        {
            string username = txtBoxUsername.Text.Trim();
            string password = txtBoxPassword.Text;
            string confirmPassword = txtBoxConfirm.Text;
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Plotësoni të gjitha fushat.");
                return false;
            }
            
            if(password != confirmPassword)
            {
                MessageBox.Show("Passwordet nuk përputhen.");
                return false;
            }
            
            if(password.Length < 6)
            {
                MessageBox.Show("Passwordi duhet të jetë të paktën 6 karaktere.");
                return false;
            }

            if (UsernameExists(username))
            {
                MessageBox.Show("Ky username është i zënë. Zgjidhni një tjetër.");
                return false;
            }

            return true;

        }
        private void btnMasking1_Click(object sender, EventArgs e)
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

        private void btnMasking2_Click(object sender, EventArgs e)
        {
            if (txtBoxConfirm.PasswordChar == '\0')
            {
                txtBoxConfirm.PasswordChar = '*';
            }
            else
            {
                txtBoxConfirm.PasswordChar = '\0';
            }
        }

        private void btnSignUp_Click(object sender,EventArgs e)
        {
            if (ValidateSignUp())
            {
                string username = txtBoxUsername.Text.Trim();
                string password = txtBoxPassword.Text;
                string passwordHash = PasswordHelper.HashPassword(password);
                CreateUser(username, passwordHash);
                MessageBox.Show("Përdoruesi u krijua me sukses.");
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtBoxUsername.Clear();
            txtBoxPassword.Clear();
            txtBoxConfirm.Clear();
            txtBoxPassword.PasswordChar = '*';
            txtBoxConfirm.PasswordChar = '*';
            txtBoxUsername.Focus();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                txtBoxPassword.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                txtBoxConfirm.Focus();
                e.SuppressKeyPress = true;
            }
        }
    }
}
