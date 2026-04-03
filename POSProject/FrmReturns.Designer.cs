using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;

namespace POSProject
{
    partial class FrmReturns
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
            labelSearchTitle = new Label();
            txtBoxInvoiceNumber = new TextBox();
            btnSearchSale = new Button();
            labelSaleNumber = new Label();
            labelSaleDate = new Label();
            labelCashier = new Label();
            labelOriginalTotal = new Label();
            dataGridView1 = new DataGridView();
            labelReason = new Label();
            txtBoxReason = new TextBox();
            label7 = new Label();
            comboBox1 = new ComboBox();
            label8 = new Label();
            textBox3 = new TextBox();
            label9 = new Label();
            textBox4 = new TextBox();
            label10 = new Label();
            label11 = new Label();
            labelInfo = new Label();
            btnSaveReturn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // labelSearchTitle
            // 
            labelSearchTitle.AutoSize = true;
            labelSearchTitle.Location = new Point(58, 84);
            labelSearchTitle.Name = "labelSearchTitle";
            labelSearchTitle.Size = new Size(237, 19);
            labelSearchTitle.TabIndex = 0;
            labelSearchTitle.Text = "Kërko sipas numrit të faturës:";
            // 
            // txtBoxInvoiceNumber
            // 
            txtBoxInvoiceNumber.Location = new Point(62, 146);
            txtBoxInvoiceNumber.Name = "txtBoxInvoiceNumber";
            txtBoxInvoiceNumber.Size = new Size(125, 25);
            txtBoxInvoiceNumber.TabIndex = 1;
            // 
            // btnSearchSale
            // 
            btnSearchSale.BackColor = Color.LightSteelBlue;
            btnSearchSale.Location = new Point(62, 210);
            btnSearchSale.Name = "btnSearchSale";
            btnSearchSale.Size = new Size(94, 29);
            btnSearchSale.TabIndex = 2;
            btnSearchSale.Text = "Kërko shitjen";
            btnSearchSale.UseVisualStyleBackColor = false;
            // 
            // labelSaleNumber
            // 
            labelSaleNumber.AutoSize = true;
            labelSaleNumber.Location = new Point(92, 361);
            labelSaleNumber.Name = "labelSaleNumber";
            labelSaleNumber.Size = new Size(54, 19);
            labelSaleNumber.TabIndex = 3;
            labelSaleNumber.Text = "Numri i shitjes";
            // 
            // labelSaleDate
            // 
            labelSaleDate.AutoSize = true;
            labelSaleDate.Location = new Point(92, 417);
            labelSaleDate.Name = "labelSaleDate";
            labelSaleDate.Size = new Size(54, 19);
            labelSaleDate.TabIndex = 4;
            labelSaleDate.Text = "Data e shitjes";
            // 
            // labelCashier
            // 
            labelCashier.AutoSize = true;
            labelCashier.Location = new Point(92, 476);
            labelCashier.Name = "labelCashier";
            labelCashier.Size = new Size(54, 19);
            labelCashier.TabIndex = 5;
            labelCashier.Text = "Arkëtari";
            // 
            // labelOriginalTotal
            // 
            labelOriginalTotal.AutoSize = true;
            labelOriginalTotal.Location = new Point(92, 535);
            labelOriginalTotal.Name = "labelOriginalTotal";
            labelOriginalTotal.Size = new Size(54, 19);
            labelOriginalTotal.TabIndex = 6;
            labelOriginalTotal.Text = "Totali origjinal:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(412, 41);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(731, 285);
            dataGridView1.TabIndex = 7;
            // 
            // labelReason
            // 
            labelReason.AutoSize = true;
            labelReason.Location = new Point(437, 376);
            labelReason.Name = "labelReason";
            labelReason.Size = new Size(54, 19);
            labelReason.TabIndex = 8;
            labelReason.Text = "Arsyeja:";
            // 
            // txtBoxReason
            // 
            txtBoxReason.Location = new Point(544, 375);
            txtBoxReason.Name = "txtBoxReason";
            txtBoxReason.Size = new Size(125, 25);
            txtBoxReason.TabIndex = 9;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(437, 450);
            label7.Name = "label7";
            label7.Size = new Size(54, 19);
            label7.TabIndex = 10;
            label7.Text = "label7";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(544, 446);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 27);
            comboBox1.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(437, 520);
            label8.Name = "label8";
            label8.Size = new Size(54, 19);
            label8.TabIndex = 12;
            label8.Text = "label8";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(544, 517);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 25);
            textBox3.TabIndex = 13;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(819, 375);
            label9.Name = "label9";
            label9.Size = new Size(54, 19);
            label9.TabIndex = 14;
            label9.Text = "label9";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(928, 375);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(125, 25);
            textBox4.TabIndex = 15;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(819, 436);
            label10.Name = "label10";
            label10.Size = new Size(63, 19);
            label10.TabIndex = 16;
            label10.Text = "label10";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(819, 479);
            label11.Name = "label11";
            label11.Size = new Size(63, 19);
            label11.TabIndex = 17;
            label11.Text = "label11";
            // 
            // labelInfo
            // 
            labelInfo.AutoSize = true;
            labelInfo.Location = new Point(819, 522);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(42, 19);
            labelInfo.TabIndex = 18;
            labelInfo.Text = "Info:";
            // 
            // btnSaveReturn
            // 
            btnSaveReturn.BackColor = Color.LightGreen;
            btnSaveReturn.Location = new Point(982, 564);
            btnSaveReturn.Name = "btnSaveReturn";
            btnSaveReturn.Size = new Size(161, 47);
            btnSaveReturn.TabIndex = 19;
            btnSaveReturn.Text = "Ruaj";
            btnSaveReturn.UseVisualStyleBackColor = false;
            // 
            // FrmReturns
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1219, 642);
            Controls.Add(btnSaveReturn);
            Controls.Add(labelInfo);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(textBox4);
            Controls.Add(label9);
            Controls.Add(textBox3);
            Controls.Add(label8);
            Controls.Add(comboBox1);
            Controls.Add(label7);
            Controls.Add(txtBoxReason);
            Controls.Add(labelReason);
            Controls.Add(dataGridView1);
            Controls.Add(labelOriginalTotal);
            Controls.Add(labelCashier);
            Controls.Add(labelSaleDate);
            Controls.Add(labelSaleNumber);
            Controls.Add(btnSearchSale);
            Controls.Add(txtBoxInvoiceNumber);
            Controls.Add(labelSearchTitle);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmReturns";
            Text = "FrmReturns";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelSearchTitle;
        private TextBox txtBoxInvoiceNumber;
        private Button btnSearchSale;
        private Label labelSaleNumber;
        private Label labelSaleDate;
        private Label labelCashier;
        private Label labelOriginalTotal;
        private DataGridView dataGridView1;
        private Label labelReason;
        private TextBox txtBoxReason;
        private Label label7;
        private ComboBox comboBox1;
        private Label label8;
        private TextBox textBox3;
        private Label label9;
        private TextBox textBox4;
        private Label label10;
        private Label label11;
        private Label labelInfo;
        private Button btnSaveReturn;
    }
}