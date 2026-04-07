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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
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
            labelRefundMethod = new Label();
            comboBoxRefundMethod = new ComboBox();
            labelRefundAmount = new Label();
            txtBoxRefundAmount = new TextBox();
            labelReferenceNr = new Label();
            txtBoxReferenceNr = new TextBox();
            labelTotalReturnCaption = new Label();
            labelTotalReturn = new Label();
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
            txtBoxInvoiceNumber.Size = new Size(233, 25);
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
            labelSaleNumber.Location = new Point(55, 361);
            labelSaleNumber.Name = "labelSaleNumber";
            labelSaleNumber.Size = new Size(122, 19);
            labelSaleNumber.TabIndex = 3;
            labelSaleNumber.Text = "Numri i shitjes";
            // 
            // labelSaleDate
            // 
            labelSaleDate.AutoSize = true;
            labelSaleDate.Location = new Point(55, 417);
            labelSaleDate.Name = "labelSaleDate";
            labelSaleDate.Size = new Size(115, 19);
            labelSaleDate.TabIndex = 4;
            labelSaleDate.Text = "Data e shitjes";
            // 
            // labelCashier
            // 
            labelCashier.AutoSize = true;
            labelCashier.Location = new Point(55, 476);
            labelCashier.Name = "labelCashier";
            labelCashier.Size = new Size(71, 19);
            labelCashier.TabIndex = 5;
            labelCashier.Text = "Arkëtari";
            // 
            // labelOriginalTotal
            // 
            labelOriginalTotal.AutoSize = true;
            labelOriginalTotal.Location = new Point(55, 535);
            labelOriginalTotal.Name = "labelOriginalTotal";
            labelOriginalTotal.Size = new Size(123, 19);
            labelOriginalTotal.TabIndex = 6;
            labelOriginalTotal.Text = "Totali origjinal:";
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
            dataGridView1.Location = new Point(350, 41);
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
            dataGridView1.Size = new Size(841, 285);
            dataGridView1.TabIndex = 7;
            // 
            // labelReason
            // 
            labelReason.AutoSize = true;
            labelReason.Location = new Point(413, 376);
            labelReason.Name = "labelReason";
            labelReason.Size = new Size(70, 19);
            labelReason.TabIndex = 8;
            labelReason.Text = "Arsyeja:";
            // 
            // txtBoxReason
            // 
            txtBoxReason.Location = new Point(600, 373);
            txtBoxReason.Name = "txtBoxReason";
            txtBoxReason.Size = new Size(168, 25);
            txtBoxReason.TabIndex = 9;
            // 
            // labelRefundMethod
            // 
            labelRefundMethod.AutoSize = true;
            labelRefundMethod.Location = new Point(413, 457);
            labelRefundMethod.Name = "labelRefundMethod";
            labelRefundMethod.Size = new Size(178, 19);
            labelRefundMethod.TabIndex = 10;
            labelRefundMethod.Text = "Mënyra e rimbursimit:";
            // 
            // comboBoxRefundMethod
            // 
            comboBoxRefundMethod.FormattingEnabled = true;
            comboBoxRefundMethod.Location = new Point(600, 454);
            comboBoxRefundMethod.Name = "comboBoxRefundMethod";
            comboBoxRefundMethod.Size = new Size(168, 27);
            comboBoxRefundMethod.TabIndex = 11;
            // 
            // labelRefundAmount
            // 
            labelRefundAmount.AutoSize = true;
            labelRefundAmount.Location = new Point(413, 527);
            labelRefundAmount.Name = "labelRefundAmount";
            labelRefundAmount.Size = new Size(163, 19);
            labelRefundAmount.TabIndex = 12;
            labelRefundAmount.Text = "Vlera e rimbursimit:";
            // 
            // txtBoxRefundAmount
            // 
            txtBoxRefundAmount.Location = new Point(600, 524);
            txtBoxRefundAmount.Name = "txtBoxRefundAmount";
            txtBoxRefundAmount.Size = new Size(168, 25);
            txtBoxRefundAmount.TabIndex = 13;
            // 
            // labelReferenceNr
            // 
            labelReferenceNr.AutoSize = true;
            labelReferenceNr.Location = new Point(819, 375);
            labelReferenceNr.Name = "labelReferenceNr";
            labelReferenceNr.Size = new Size(157, 19);
            labelReferenceNr.TabIndex = 14;
            labelReferenceNr.Text = "Numri i referencës:";
            // 
            // txtBoxReferenceNr
            // 
            txtBoxReferenceNr.Location = new Point(982, 372);
            txtBoxReferenceNr.Name = "txtBoxReferenceNr";
            txtBoxReferenceNr.Size = new Size(161, 25);
            txtBoxReferenceNr.TabIndex = 15;
            // 
            // labelTotalReturnCaption
            // 
            labelTotalReturnCaption.AutoSize = true;
            labelTotalReturnCaption.Location = new Point(819, 427);
            labelTotalReturnCaption.Name = "labelTotalReturnCaption";
            labelTotalReturnCaption.Size = new Size(63, 19);
            labelTotalReturnCaption.TabIndex = 16;
            labelTotalReturnCaption.Text = "label10";
            // 
            // labelTotalReturn
            // 
            labelTotalReturn.AutoSize = true;
            labelTotalReturn.Location = new Point(819, 481);
            labelTotalReturn.Name = "labelTotalReturn";
            labelTotalReturn.Size = new Size(104, 19);
            labelTotalReturn.TabIndex = 17;
            labelTotalReturn.Text = "Kthimi total:";
            // 
            // labelInfo
            // 
            labelInfo.AutoSize = true;
            labelInfo.Location = new Point(819, 528);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(42, 19);
            labelInfo.TabIndex = 18;
            labelInfo.Text = "Info:";
            // 
            // btnSaveReturn
            // 
            btnSaveReturn.BackColor = Color.LightGreen;
            btnSaveReturn.Location = new Point(983, 575);
            btnSaveReturn.Name = "btnSaveReturn";
            btnSaveReturn.Size = new Size(161, 47);
            btnSaveReturn.TabIndex = 19;
            btnSaveReturn.Text = "Ruaj";
            btnSaveReturn.UseVisualStyleBackColor = false;
            btnSaveReturn.Click += btnSaveReturn_Click;
            // 
            // FrmReturns
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1219, 642);
            Controls.Add(btnSaveReturn);
            Controls.Add(labelInfo);
            Controls.Add(labelTotalReturn);
            Controls.Add(labelTotalReturnCaption);
            Controls.Add(txtBoxReferenceNr);
            Controls.Add(labelReferenceNr);
            Controls.Add(txtBoxRefundAmount);
            Controls.Add(labelRefundAmount);
            Controls.Add(comboBoxRefundMethod);
            Controls.Add(labelRefundMethod);
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
        private Label labelRefundMethod;
        private ComboBox comboBoxRefundMethod;
        private Label labelRefundAmount;
        private TextBox txtBoxRefundAmount;
        private Label labelReferenceNr;
        private TextBox txtBoxReferenceNr;
        private Label labelTotalReturnCaption;
        private Label labelTotalReturn;
        private Label labelInfo;
        private Button btnSaveReturn;
    }
}