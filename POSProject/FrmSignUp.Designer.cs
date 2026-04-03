namespace POSProject
{
    partial class FrmSignUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSignUp));
            lblSignUp = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            lblConfirm = new Label();
            txtBoxUsername = new TextBox();
            txtBoxPassword = new TextBox();
            txtBoxConfirm = new TextBox();
            btnSignUp = new Button();
            btnMasking1 = new Button();
            btnMasking2 = new Button();
            SuspendLayout();
            // 
            // lblSignUp
            // 
            lblSignUp.AutoSize = true;
            lblSignUp.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSignUp.Location = new Point(655, 141);
            lblSignUp.Name = "lblSignUp";
            lblSignUp.Size = new Size(125, 32);
            lblSignUp.TabIndex = 0;
            lblSignUp.Text = "Sign Up";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(495, 227);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(92, 19);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(495, 292);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(84, 19);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password:";
            // 
            // lblConfirm
            // 
            lblConfirm.AutoSize = true;
            lblConfirm.Location = new Point(495, 368);
            lblConfirm.Name = "lblConfirm";
            lblConfirm.Size = new Size(149, 19);
            lblConfirm.TabIndex = 3;
            lblConfirm.Text = "Confirm Password:";
            // 
            // txtBoxUsername
            // 
            txtBoxUsername.Location = new Point(709, 220);
            txtBoxUsername.Name = "txtBoxUsername";
            txtBoxUsername.Size = new Size(140, 25);
            txtBoxUsername.TabIndex = 4;
            // 
            // txtBoxPassword
            // 
            txtBoxPassword.Location = new Point(709, 289);
            txtBoxPassword.Name = "txtBoxPassword";
            txtBoxPassword.Size = new Size(140, 25);
            txtBoxPassword.TabIndex = 5;
            // 
            // txtBoxConfirm
            // 
            txtBoxConfirm.Location = new Point(709, 365);
            txtBoxConfirm.Name = "txtBoxConfirm";
            txtBoxConfirm.Size = new Size(140, 25);
            txtBoxConfirm.TabIndex = 6;
            // 
            // btnSignUp
            // 
            btnSignUp.BackColor = Color.LightSteelBlue;
            btnSignUp.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSignUp.Location = new Point(801, 429);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(105, 34);
            btnSignUp.TabIndex = 7;
            btnSignUp.Text = "Sign Up";
            btnSignUp.UseVisualStyleBackColor = false;
            btnSignUp.Click += btnSignUp_Click;
            // 
            // btnMasking1
            // 
            btnMasking1.BackgroundImage = (Image)resources.GetObject("btnMasking1.BackgroundImage");
            btnMasking1.BackgroundImageLayout = ImageLayout.Zoom;
            btnMasking1.Location = new Point(867, 289);
            btnMasking1.Name = "btnMasking1";
            btnMasking1.Size = new Size(29, 25);
            btnMasking1.TabIndex = 8;
            btnMasking1.UseVisualStyleBackColor = true;
            btnMasking1.Click += btnMasking1_Click;
            // 
            // btnMasking2
            // 
            btnMasking2.BackgroundImage = (Image)resources.GetObject("btnMasking2.BackgroundImage");
            btnMasking2.BackgroundImageLayout = ImageLayout.Zoom;
            btnMasking2.Location = new Point(867, 365);
            btnMasking2.Name = "btnMasking2";
            btnMasking2.Size = new Size(29, 25);
            btnMasking2.TabIndex = 9;
            btnMasking2.UseVisualStyleBackColor = true;
            btnMasking2.Click += btnMasking2_Click;
            // 
            // FrmSignUp
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1394, 618);
            Controls.Add(btnMasking2);
            Controls.Add(btnMasking1);
            Controls.Add(btnSignUp);
            Controls.Add(txtBoxConfirm);
            Controls.Add(txtBoxPassword);
            Controls.Add(txtBoxUsername);
            Controls.Add(lblConfirm);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Controls.Add(lblSignUp);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmSignUp";
            Text = "FrmSignUp";
            Load += FrmSignUp_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSignUp;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblConfirm;
        private TextBox txtBoxUsername;
        private TextBox txtBoxPassword;
        private TextBox txtBoxConfirm;
        private Button btnSignUp;
        private Button btnMasking1;
        private Button btnMasking2;
    }
}