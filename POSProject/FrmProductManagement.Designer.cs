namespace POSProject
{
    partial class FrmProductManagement
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
            btnAdd = new Button();
            btnEdit = new Button();
            btnRemove = new Button();
            labelName = new Label();
            txtBoxName = new TextBox();
            labelBarcode = new Label();
            txtBoxBarcode = new TextBox();
            labelKategoria = new Label();
            comboKategoria = new ComboBox();
            labelCmimi = new Label();
            txtBoxCmimi = new TextBox();
            labelSasia = new Label();
            txtBoxSasia = new TextBox();
            labelAktiv = new Label();
            chckBxAktiv = new CheckBox();
            chckBxJoAktiv = new CheckBox();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Location = new Point(664, 89);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(106, 28);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Shto produkt";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.LightGoldenrodYellow;
            btnEdit.Location = new Point(909, 89);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(106, 28);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Edito produkt";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.LightCoral;
            btnRemove.Location = new Point(1146, 89);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(106, 28);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Fshi produkt";
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(724, 183);
            labelName.Name = "labelName";
            labelName.Size = new Size(50, 19);
            labelName.TabIndex = 3;
            labelName.Text = "Emri:";
            // 
            // txtBoxName
            // 
            txtBoxName.Location = new Point(922, 177);
            txtBoxName.Name = "txtBoxName";
            txtBoxName.Size = new Size(195, 25);
            txtBoxName.TabIndex = 4;
            // 
            // labelBarcode
            // 
            labelBarcode.AutoSize = true;
            labelBarcode.Location = new Point(724, 251);
            labelBarcode.Name = "labelBarcode";
            labelBarcode.Size = new Size(72, 19);
            labelBarcode.TabIndex = 5;
            labelBarcode.Text = "Barkodi:";
            // 
            // txtBoxBarcode
            // 
            txtBoxBarcode.Location = new Point(922, 245);
            txtBoxBarcode.Name = "txtBoxBarcode";
            txtBoxBarcode.Size = new Size(195, 25);
            txtBoxBarcode.TabIndex = 6;
            // 
            // labelKategoria
            // 
            labelKategoria.AutoSize = true;
            labelKategoria.Location = new Point(724, 322);
            labelKategoria.Name = "labelKategoria";
            labelKategoria.Size = new Size(86, 19);
            labelKategoria.TabIndex = 7;
            labelKategoria.Text = "Kategoria:";
            // 
            // comboKategoria
            // 
            comboKategoria.FormattingEnabled = true;
            comboKategoria.Location = new Point(922, 314);
            comboKategoria.Name = "comboKategoria";
            comboKategoria.Size = new Size(195, 27);
            comboKategoria.TabIndex = 8;
            // 
            // labelCmimi
            // 
            labelCmimi.AutoSize = true;
            labelCmimi.Location = new Point(724, 386);
            labelCmimi.Name = "labelCmimi";
            labelCmimi.Size = new Size(63, 19);
            labelCmimi.TabIndex = 9;
            labelCmimi.Text = "Çmimi:";
            // 
            // txtBoxCmimi
            // 
            txtBoxCmimi.Location = new Point(922, 379);
            txtBoxCmimi.Name = "txtBoxCmimi";
            txtBoxCmimi.Size = new Size(159, 25);
            txtBoxCmimi.TabIndex = 10;
            // 
            // labelSasia
            // 
            labelSasia.AutoSize = true;
            labelSasia.Location = new Point(724, 450);
            labelSasia.Name = "labelSasia";
            labelSasia.Size = new Size(55, 19);
            labelSasia.TabIndex = 11;
            labelSasia.Text = "Sasia:";
            // 
            // txtBoxSasia
            // 
            txtBoxSasia.Location = new Point(922, 447);
            txtBoxSasia.Name = "txtBoxSasia";
            txtBoxSasia.Size = new Size(159, 25);
            txtBoxSasia.TabIndex = 12;
            // 
            // labelAktiv
            // 
            labelAktiv.AutoSize = true;
            labelAktiv.Location = new Point(724, 524);
            labelAktiv.Name = "labelAktiv";
            labelAktiv.Size = new Size(51, 19);
            labelAktiv.TabIndex = 13;
            labelAktiv.Text = "Aktiv:";
            // 
            // chckBxAktiv
            // 
            chckBxAktiv.AutoSize = true;
            chckBxAktiv.Location = new Point(922, 523);
            chckBxAktiv.Name = "chckBxAktiv";
            chckBxAktiv.Size = new Size(48, 23);
            chckBxAktiv.TabIndex = 14;
            chckBxAktiv.Text = "Po";
            chckBxAktiv.UseVisualStyleBackColor = true;
            chckBxAktiv.CheckedChanged += chckBxAktiv_CheckedChanged;
            // 
            // chckBxJoAktiv
            // 
            chckBxJoAktiv.AutoSize = true;
            chckBxJoAktiv.Location = new Point(1036, 523);
            chckBxJoAktiv.Name = "chckBxJoAktiv";
            chckBxJoAktiv.Size = new Size(48, 23);
            chckBxJoAktiv.TabIndex = 15;
            chckBxJoAktiv.Text = "Jo";
            chckBxJoAktiv.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(27, 79);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(534, 476);
            dataGridView1.TabIndex = 16;
            // 
            // FrmProductManagement
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1378, 646);
            Controls.Add(dataGridView1);
            Controls.Add(chckBxJoAktiv);
            Controls.Add(chckBxAktiv);
            Controls.Add(labelAktiv);
            Controls.Add(txtBoxSasia);
            Controls.Add(labelSasia);
            Controls.Add(txtBoxCmimi);
            Controls.Add(labelCmimi);
            Controls.Add(comboKategoria);
            Controls.Add(labelKategoria);
            Controls.Add(txtBoxBarcode);
            Controls.Add(labelBarcode);
            Controls.Add(txtBoxName);
            Controls.Add(labelName);
            Controls.Add(btnRemove);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmProductManagement";
            Text = "s";
            Load += FrmProductManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAdd;
        private Button btnEdit;
        private Button btnRemove;
        private Label labelName;
        private TextBox txtBoxName;
        private Label labelBarcode;
        private TextBox txtBoxBarcode;
        private Label labelKategoria;
        private ComboBox comboKategoria;
        private Label labelCmimi;
        private TextBox txtBoxCmimi;
        private Label labelSasia;
        private TextBox txtBoxSasia;
        private Label labelAktiv;
        private CheckBox chckBxAktiv;
        private CheckBox chckBxJoAktiv;
        private DataGridView dataGridView1;
    }
}