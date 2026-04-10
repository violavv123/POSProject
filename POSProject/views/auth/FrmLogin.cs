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
using POSProject.repositories.auth;
using POSProject.services;


namespace POSProject
{
    public partial class FrmLogin : Form
    {
        private readonly UserService _userService;
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

            IUserRepository _userRepo = new UserRepository();
            _userService = new UserService(_userRepo);
        }

        private bool ValidateLogin()
        {
            var result = _userService.Login(
                txtBoxUsername.Text,
                txtBoxPassword.Text
                );
            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return false;
            }

            _userService.StartSession(result.User);
            return true;
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
