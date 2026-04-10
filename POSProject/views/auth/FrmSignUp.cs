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
using POSProject.repositories.auth;
using POSProject.services;

namespace POSProject
{
    public partial class FrmSignUp : Form
    {
        private readonly UserService _userService;
        public FrmSignUp()
        {
            InitializeComponent();
            txtBoxPassword.PasswordChar = '*';
            txtBoxConfirm.PasswordChar = '*';
            this.AcceptButton = btnSignUp;
            txtBoxUsername.KeyDown += txtUsername_KeyDown;
            txtBoxPassword.KeyDown += txtPassword_KeyDown;

            IUserRepository userRepo = new UserRepository();
            _userService = new UserService(userRepo);
        }

        private void FrmSignUp_Load(object sender, EventArgs e)
        {
            if(Session.Role != "admin")
            {
                MessageBox.Show("Nuk keni akses në këtë faqe.");
                this.Close();
            }
        }

        private bool ValidateSignUp()
        {
            if (string.IsNullOrWhiteSpace(txtBoxUsername.Text) ||
                string.IsNullOrWhiteSpace(txtBoxPassword.Text) ||
                string.IsNullOrWhiteSpace(txtBoxConfirm.Text))
            {
                MessageBox.Show("Plotësoni të gjitha fushat.");
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
            if (!ValidateSignUp())
                return;

            var result = _userService.CreateUser(
                txtBoxUsername.Text,
                txtBoxPassword.Text,
                txtBoxConfirm.Text
                );
            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }
            MessageBox.Show(result.Message);
            ClearForm();
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
