namespace POSProject
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            labelUsername = new Label();
            txtBoxUsername = new TextBox();
            labelPassword = new Label();
            labelLogin = new Label();
            btnLogin = new Button();
            btnMasking = new Button();
            txtBoxPassword = new TextBox();
            SuspendLayout();
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Location = new Point(482, 226);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(92, 19);
            labelUsername.TabIndex = 0;
            labelUsername.Text = "Username:";
            // 
            // txtBoxUsername
            // 
            txtBoxUsername.Location = new Point(618, 223);
            txtBoxUsername.Name = "txtBoxUsername";
            txtBoxUsername.Size = new Size(140, 25);
            txtBoxUsername.TabIndex = 1;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(482, 294);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(84, 19);
            labelPassword.TabIndex = 2;
            labelPassword.Text = "Password:";
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelLogin.ForeColor = Color.Black;
            labelLogin.Location = new Point(604, 140);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(105, 32);
            labelLogin.TabIndex = 4;
            labelLogin.Text = "Log In";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.LightSteelBlue;
            btnLogin.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = SystemColors.ActiveCaptionText;
            btnLogin.Location = new Point(763, 418);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(119, 42);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Log In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnMasking
            // 
            btnMasking.BackgroundImage = (Image)resources.GetObject("btnMasking.BackgroundImage");
            btnMasking.BackgroundImageLayout = ImageLayout.Zoom;
            btnMasking.Location = new Point(776, 292);
            btnMasking.Name = "btnMasking";
            btnMasking.Size = new Size(34, 26);
            btnMasking.TabIndex = 7;
            btnMasking.UseVisualStyleBackColor = true;
            btnMasking.Click += btnMasking_Click;
            // 
            // txtBoxPassword
            // 
            txtBoxPassword.Location = new Point(618, 292);
            txtBoxPassword.Name = "txtBoxPassword";
            txtBoxPassword.PasswordChar = '*';
            txtBoxPassword.Size = new Size(140, 25);
            txtBoxPassword.TabIndex = 8;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1297, 590);
            Controls.Add(txtBoxPassword);
            Controls.Add(btnMasking);
            Controls.Add(btnLogin);
            Controls.Add(labelLogin);
            Controls.Add(labelPassword);
            Controls.Add(txtBoxUsername);
            Controls.Add(labelUsername);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmLogin";
            Text = "FrmLogin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelUsername;
        private TextBox txtBoxUsername;
        private Label labelPassword;
        private Label labelLogin;
        private Button btnLogin;
        private Button btnMasking;
        private TextBox txtBoxPassword;
    }
}