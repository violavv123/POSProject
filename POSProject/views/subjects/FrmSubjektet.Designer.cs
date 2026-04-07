namespace POSProject
{
    partial class FrmSubjektet
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            labelPershkrimi = new Label();
            labelNrFiskal = new Label();
            labelAdresa = new Label();
            labelLloji = new Label();
            txtBoxPershkrimi = new TextBox();
            txtBoxNrFiskal = new TextBox();
            txtBoxAdresa = new TextBox();
            comboBox1 = new ComboBox();
            btnSave = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(24, 115);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(663, 407);
            dataGridView1.TabIndex = 0;
            // 
            // labelPershkrimi
            // 
            labelPershkrimi.AutoSize = true;
            labelPershkrimi.Location = new Point(814, 189);
            labelPershkrimi.Name = "labelPershkrimi";
            labelPershkrimi.Size = new Size(97, 19);
            labelPershkrimi.TabIndex = 1;
            labelPershkrimi.Text = "Përshkrimi:";
            // 
            // labelNrFiskal
            // 
            labelNrFiskal.AutoSize = true;
            labelNrFiskal.Location = new Point(814, 282);
            labelNrFiskal.Name = "labelNrFiskal";
            labelNrFiskal.Size = new Size(107, 19);
            labelNrFiskal.TabIndex = 2;
            labelNrFiskal.Text = "Numri fiskal:";
            // 
            // labelAdresa
            // 
            labelAdresa.AutoSize = true;
            labelAdresa.Location = new Point(814, 371);
            labelAdresa.Name = "labelAdresa";
            labelAdresa.Size = new Size(66, 19);
            labelAdresa.TabIndex = 3;
            labelAdresa.Text = "Adresa:";
            // 
            // labelLloji
            // 
            labelLloji.AutoSize = true;
            labelLloji.Location = new Point(814, 468);
            labelLloji.Name = "labelLloji";
            labelLloji.Size = new Size(46, 19);
            labelLloji.TabIndex = 4;
            labelLloji.Text = "Lloji:";
            // 
            // txtBoxPershkrimi
            // 
            txtBoxPershkrimi.Location = new Point(1006, 182);
            txtBoxPershkrimi.Name = "txtBoxPershkrimi";
            txtBoxPershkrimi.Size = new Size(284, 25);
            txtBoxPershkrimi.TabIndex = 5;
            // 
            // txtBoxNrFiskal
            // 
            txtBoxNrFiskal.Location = new Point(1006, 276);
            txtBoxNrFiskal.Name = "txtBoxNrFiskal";
            txtBoxNrFiskal.Size = new Size(140, 25);
            txtBoxNrFiskal.TabIndex = 6;
            // 
            // txtBoxAdresa
            // 
            txtBoxAdresa.Location = new Point(1006, 365);
            txtBoxAdresa.Name = "txtBoxAdresa";
            txtBoxAdresa.Size = new Size(204, 25);
            txtBoxAdresa.TabIndex = 7;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(1006, 461);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(169, 27);
            comboBox1.TabIndex = 8;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.LightGreen;
            btnSave.ForeColor = SystemColors.ControlText;
            btnSave.Location = new Point(765, 115);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(106, 28);
            btnSave.TabIndex = 9;
            btnSave.Text = "Ruaj";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.LightGoldenrodYellow;
            btnUpdate.Location = new Point(984, 115);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(106, 28);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Edito";
            btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.Location = new Point(1184, 116);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(106, 28);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Fshij";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // FrmSubjektet
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1358, 639);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnSave);
            Controls.Add(comboBox1);
            Controls.Add(txtBoxAdresa);
            Controls.Add(txtBoxNrFiskal);
            Controls.Add(txtBoxPershkrimi);
            Controls.Add(labelLloji);
            Controls.Add(labelAdresa);
            Controls.Add(labelNrFiskal);
            Controls.Add(labelPershkrimi);
            Controls.Add(dataGridView1);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmSubjektet";
            Text = "FrmSubjektet";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label labelPershkrimi;
        private Label labelNrFiskal;
        private Label labelAdresa;
        private Label labelLloji;
        private TextBox txtBoxPershkrimi;
        private TextBox txtBoxNrFiskal;
        private TextBox txtBoxAdresa;
        private ComboBox comboBox1;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
    }
}