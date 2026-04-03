namespace POSProject
{
    partial class FrmMethodsOfPayment
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
            panel1 = new Panel();
            btnClose = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            dataGridView1 = new DataGridView();
            btnSave = new Button();
            labelRendorja = new Label();
            txtBoxRendorja = new TextBox();
            labelPershkrimi = new Label();
            txtBoxPershkrimi = new TextBox();
            labelShkurtesa = new Label();
            cmbBoxTipi = new ComboBox();
            labelTipi = new Label();
            cmbBoxValutaDefault = new ComboBox();
            labelValutaDefault = new Label();
            txtBoxShkurtesa = new TextBox();
            labelKerkonReference = new Label();
            chckBoxKerkon = new CheckBox();
            chckBoxNukKerkon = new CheckBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnClear);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnUpdate);
            panel1.Controls.Add(btnAdd);
            panel1.Location = new Point(758, 127);
            panel1.Name = "panel1";
            panel1.Size = new Size(684, 66);
            panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Orange;
            btnClose.Location = new Point(546, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(136, 61);
            btnClose.TabIndex = 3;
            btnClose.Text = "Mbyll";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.Thistle;
            btnClear.Location = new Point(410, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(136, 61);
            btnClear.TabIndex = 3;
            btnClear.Text = "Pastro";
            btnClear.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.Location = new Point(271, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(136, 61);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Fshij";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.LightYellow;
            btnUpdate.Location = new Point(133, 3);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(136, 61);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Përditëso";
            btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Location = new Point(3, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 61);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Shto";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 127);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(737, 425);
            dataGridView1.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.LightSteelBlue;
            btnSave.Location = new Point(1314, 524);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(106, 28);
            btnSave.TabIndex = 2;
            btnSave.Text = "Ruaj";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // labelRendorja
            // 
            labelRendorja.AutoSize = true;
            labelRendorja.Location = new Point(858, 253);
            labelRendorja.Name = "labelRendorja";
            labelRendorja.Size = new Size(82, 19);
            labelRendorja.TabIndex = 3;
            labelRendorja.Text = "Rendorja:";
            // 
            // txtBoxRendorja
            // 
            txtBoxRendorja.Location = new Point(1037, 250);
            txtBoxRendorja.Name = "txtBoxRendorja";
            txtBoxRendorja.Size = new Size(198, 25);
            txtBoxRendorja.TabIndex = 4;
            txtBoxRendorja.TextAlign = HorizontalAlignment.Right;
            // 
            // labelPershkrimi
            // 
            labelPershkrimi.AutoSize = true;
            labelPershkrimi.Location = new Point(858, 299);
            labelPershkrimi.Name = "labelPershkrimi";
            labelPershkrimi.Size = new Size(97, 19);
            labelPershkrimi.TabIndex = 5;
            labelPershkrimi.Text = "Përshkrimi:";
            // 
            // txtBoxPershkrimi
            // 
            txtBoxPershkrimi.Location = new Point(1037, 296);
            txtBoxPershkrimi.Name = "txtBoxPershkrimi";
            txtBoxPershkrimi.Size = new Size(198, 25);
            txtBoxPershkrimi.TabIndex = 6;
            txtBoxPershkrimi.TextAlign = HorizontalAlignment.Right;
            txtBoxPershkrimi.TextChanged += txtBoxPershkrimi_TextChanged;
            // 
            // labelShkurtesa
            // 
            labelShkurtesa.AutoSize = true;
            labelShkurtesa.Location = new Point(858, 350);
            labelShkurtesa.Name = "labelShkurtesa";
            labelShkurtesa.Size = new Size(92, 19);
            labelShkurtesa.TabIndex = 7;
            labelShkurtesa.Text = "Shkurtesa:";
            // 
            // cmbBoxTipi
            // 
            cmbBoxTipi.FormattingEnabled = true;
            cmbBoxTipi.Location = new Point(1037, 446);
            cmbBoxTipi.Name = "cmbBoxTipi";
            cmbBoxTipi.Size = new Size(198, 27);
            cmbBoxTipi.TabIndex = 8;
            // 
            // labelTipi
            // 
            labelTipi.AutoSize = true;
            labelTipi.Location = new Point(858, 449);
            labelTipi.Name = "labelTipi";
            labelTipi.Size = new Size(41, 19);
            labelTipi.TabIndex = 9;
            labelTipi.Text = "Tipi:";
            // 
            // cmbBoxValutaDefault
            // 
            cmbBoxValutaDefault.FormattingEnabled = true;
            cmbBoxValutaDefault.Location = new Point(1037, 498);
            cmbBoxValutaDefault.Name = "cmbBoxValutaDefault";
            cmbBoxValutaDefault.Size = new Size(198, 27);
            cmbBoxValutaDefault.TabIndex = 10;
            // 
            // labelValutaDefault
            // 
            labelValutaDefault.AutoSize = true;
            labelValutaDefault.Location = new Point(858, 501);
            labelValutaDefault.Name = "labelValutaDefault";
            labelValutaDefault.Size = new Size(125, 19);
            labelValutaDefault.TabIndex = 11;
            labelValutaDefault.Text = "Valuta Default:";
            // 
            // txtBoxShkurtesa
            // 
            txtBoxShkurtesa.Location = new Point(1037, 343);
            txtBoxShkurtesa.Name = "txtBoxShkurtesa";
            txtBoxShkurtesa.Size = new Size(198, 25);
            txtBoxShkurtesa.TabIndex = 12;
            txtBoxShkurtesa.TextAlign = HorizontalAlignment.Right;
            // 
            // labelKerkonReference
            // 
            labelKerkonReference.AutoSize = true;
            labelKerkonReference.Location = new Point(859, 394);
            labelKerkonReference.Name = "labelKerkonReference";
            labelKerkonReference.Size = new Size(150, 19);
            labelKerkonReference.TabIndex = 13;
            labelKerkonReference.Text = "Kërkon Referencë:";
            // 
            // chckBoxKerkon
            // 
            chckBoxKerkon.AutoSize = true;
            chckBoxKerkon.Location = new Point(1037, 394);
            chckBoxKerkon.Name = "chckBoxKerkon";
            chckBoxKerkon.Size = new Size(48, 23);
            chckBoxKerkon.TabIndex = 14;
            chckBoxKerkon.Text = "Po";
            chckBoxKerkon.UseVisualStyleBackColor = true;
            // 
            // chckBoxNukKerkon
            // 
            chckBoxNukKerkon.AutoSize = true;
            chckBoxNukKerkon.Location = new Point(1127, 394);
            chckBoxNukKerkon.Name = "chckBoxNukKerkon";
            chckBoxNukKerkon.Size = new Size(48, 23);
            chckBoxNukKerkon.TabIndex = 15;
            chckBoxNukKerkon.Text = "Jo";
            chckBoxNukKerkon.UseVisualStyleBackColor = true;
            // 
            // FrmMethodsOfPayment
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1458, 640);
            Controls.Add(chckBoxNukKerkon);
            Controls.Add(chckBoxKerkon);
            Controls.Add(labelKerkonReference);
            Controls.Add(txtBoxShkurtesa);
            Controls.Add(labelValutaDefault);
            Controls.Add(cmbBoxValutaDefault);
            Controls.Add(labelTipi);
            Controls.Add(cmbBoxTipi);
            Controls.Add(labelShkurtesa);
            Controls.Add(txtBoxPershkrimi);
            Controls.Add(labelPershkrimi);
            Controls.Add(txtBoxRendorja);
            Controls.Add(labelRendorja);
            Controls.Add(btnSave);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmMethodsOfPayment";
            Text = "MethodsOfPayment";
            Load += FrmMethodsOfPayment_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridView1;
        private Button btnClose;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private Button btnSave;
        private Label labelRendorja;
        private TextBox txtBoxRendorja;
        private Label labelPershkrimi;
        private TextBox txtBoxPershkrimi;
        private Label labelShkurtesa;
        private ComboBox cmbBoxTipi;
        private Label labelTipi;
        private ComboBox cmbBoxValutaDefault;
        private Label labelValutaDefault;
        private TextBox txtBoxShkurtesa;
        private Label labelKerkonReference;
        private CheckBox chckBoxKerkon;
        private CheckBox chckBoxNukKerkon;
    }
}