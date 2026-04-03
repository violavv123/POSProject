namespace POSProject
{
    partial class FrmCashMovements
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
            labelShuma = new Label();
            txtBoxShuma = new TextBox();
            labelArsyeja = new Label();
            txtBoxArsyeja = new TextBox();
            txtBoxKoment = new TextBox();
            labelKoment = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            labelTitle = new Label();
            SuspendLayout();
            // 
            // labelShuma
            // 
            labelShuma.AutoSize = true;
            labelShuma.Location = new Point(65, 130);
            labelShuma.Name = "labelShuma";
            labelShuma.Size = new Size(67, 19);
            labelShuma.TabIndex = 0;
            labelShuma.Text = "Shuma:";
            // 
            // txtBoxShuma
            // 
            txtBoxShuma.Location = new Point(184, 124);
            txtBoxShuma.Name = "txtBoxShuma";
            txtBoxShuma.PlaceholderText = "0.00";
            txtBoxShuma.Size = new Size(125, 25);
            txtBoxShuma.TabIndex = 1;
            txtBoxShuma.TextAlign = HorizontalAlignment.Right;
            // 
            // labelArsyeja
            // 
            labelArsyeja.AutoSize = true;
            labelArsyeja.Location = new Point(65, 205);
            labelArsyeja.Name = "labelArsyeja";
            labelArsyeja.Size = new Size(70, 19);
            labelArsyeja.TabIndex = 2;
            labelArsyeja.Text = "Arsyeja:";
            // 
            // txtBoxArsyeja
            // 
            txtBoxArsyeja.Location = new Point(184, 199);
            txtBoxArsyeja.Name = "txtBoxArsyeja";
            txtBoxArsyeja.Size = new Size(125, 25);
            txtBoxArsyeja.TabIndex = 3;
            // 
            // txtBoxKoment
            // 
            txtBoxKoment.Location = new Point(184, 279);
            txtBoxKoment.Name = "txtBoxKoment";
            txtBoxKoment.Size = new Size(125, 25);
            txtBoxKoment.TabIndex = 4;
            // 
            // labelKoment
            // 
            labelKoment.AutoSize = true;
            labelKoment.Location = new Point(65, 285);
            labelKoment.Name = "labelKoment";
            labelKoment.Size = new Size(72, 19);
            labelKoment.TabIndex = 5;
            labelKoment.Text = "Koment:";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.LightCoral;
            btnCancel.Location = new Point(26, 364);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(149, 51);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.LightGreen;
            btnSave.Location = new Point(212, 364);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(136, 51);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTitle.Location = new Point(167, 57);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(68, 23);
            labelTitle.TabIndex = 8;
            labelTitle.Text = "Titulli";
            // 
            // FrmCashMovements
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(378, 469);
            Controls.Add(labelTitle);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(labelKoment);
            Controls.Add(txtBoxKoment);
            Controls.Add(txtBoxArsyeja);
            Controls.Add(labelArsyeja);
            Controls.Add(txtBoxShuma);
            Controls.Add(labelShuma);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmCashMovements";
            Text = "FrmCashMovements";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelShuma;
        private TextBox txtBoxShuma;
        private Label labelArsyeja;
        private TextBox txtBoxArsyeja;
        private TextBox txtBoxKoment;
        private Label labelKoment;
        private Button btnCancel;
        private Button btnSave;
        private Label labelTitle;
    }
}