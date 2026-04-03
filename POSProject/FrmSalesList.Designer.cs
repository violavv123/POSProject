namespace POSProject
{
    partial class FrmSalesList
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            datePickerFrom = new DateTimePicker();
            datePickerTo = new DateTimePicker();
            labelTo = new Label();
            labelFrom = new Label();
            labelName = new Label();
            txtBoxName = new TextBox();
            btnSearch = new Button();
            labelTotali = new Label();
            txtBoxTotali = new TextBox();
            btnToday = new Button();
            btnYesterday = new Button();
            btn7Days = new Button();
            btn30Days = new Button();
            dataGridView2 = new DataGridView();
            labelProducts = new Label();
            labelNoSales = new Label();
            labelNoResult = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Control;
            dataGridViewCellStyle7.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            dataGridView1.Location = new Point(14, 59);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = SystemColors.Control;
            dataGridViewCellStyle9.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle9.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.Size = new Size(825, 297);
            dataGridView1.TabIndex = 0;
            // 
            // datePickerFrom
            // 
            datePickerFrom.Location = new Point(993, 116);
            datePickerFrom.Name = "datePickerFrom";
            datePickerFrom.Size = new Size(281, 25);
            datePickerFrom.TabIndex = 1;
            datePickerFrom.ValueChanged += datePickerFrom_ValueChanged;
            // 
            // datePickerTo
            // 
            datePickerTo.Location = new Point(993, 190);
            datePickerTo.Name = "datePickerTo";
            datePickerTo.Size = new Size(281, 25);
            datePickerTo.TabIndex = 2;
            datePickerTo.ValueChanged += datePickerTo_ValueChanged;
            // 
            // labelTo
            // 
            labelTo.AutoSize = true;
            labelTo.Location = new Point(898, 190);
            labelTo.Name = "labelTo";
            labelTo.Size = new Size(52, 19);
            labelTo.TabIndex = 4;
            labelTo.Text = "Deri: ";
            // 
            // labelFrom
            // 
            labelFrom.AutoSize = true;
            labelFrom.Location = new Point(896, 116);
            labelFrom.Name = "labelFrom";
            labelFrom.Size = new Size(49, 19);
            labelFrom.TabIndex = 3;
            labelFrom.Text = "Prej: ";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(898, 267);
            labelName.Name = "labelName";
            labelName.Size = new Size(55, 19);
            labelName.TabIndex = 5;
            labelName.Text = "Emri: ";
            // 
            // txtBoxName
            // 
            txtBoxName.Location = new Point(993, 264);
            txtBoxName.Name = "txtBoxName";
            txtBoxName.Size = new Size(140, 25);
            txtBoxName.TabIndex = 6;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.LightSteelBlue;
            btnSearch.Location = new Point(1169, 593);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(106, 28);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Kërko";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // labelTotali
            // 
            labelTotali.AutoSize = true;
            labelTotali.Location = new Point(631, 369);
            labelTotali.Name = "labelTotali";
            labelTotali.Size = new Size(61, 19);
            labelTotali.TabIndex = 8;
            labelTotali.Text = "Totali: ";
            // 
            // txtBoxTotali
            // 
            txtBoxTotali.Location = new Point(698, 365);
            txtBoxTotali.Name = "txtBoxTotali";
            txtBoxTotali.ReadOnly = true;
            txtBoxTotali.Size = new Size(140, 25);
            txtBoxTotali.TabIndex = 9;
            // 
            // btnToday
            // 
            btnToday.BackColor = Color.Thistle;
            btnToday.Location = new Point(14, 26);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(106, 28);
            btnToday.TabIndex = 10;
            btnToday.Text = "Sot";
            btnToday.UseVisualStyleBackColor = false;
            btnToday.Click += btnToday_Click;
            // 
            // btnYesterday
            // 
            btnYesterday.BackColor = Color.Thistle;
            btnYesterday.Location = new Point(235, 26);
            btnYesterday.Name = "btnYesterday";
            btnYesterday.Size = new Size(106, 28);
            btnYesterday.TabIndex = 11;
            btnYesterday.Text = "Dje";
            btnYesterday.UseVisualStyleBackColor = false;
            btnYesterday.Click += btnYesterday_Click;
            // 
            // btn7Days
            // 
            btn7Days.BackColor = Color.Thistle;
            btn7Days.Location = new Point(440, 26);
            btn7Days.Name = "btn7Days";
            btn7Days.Size = new Size(140, 28);
            btn7Days.TabIndex = 12;
            btn7Days.Text = "7 ditët e fundit";
            btn7Days.UseVisualStyleBackColor = false;
            btn7Days.Click += btn7Days_Click;
            // 
            // btn30Days
            // 
            btn30Days.BackColor = Color.Thistle;
            btn30Days.Location = new Point(688, 26);
            btn30Days.Name = "btn30Days";
            btn30Days.Size = new Size(150, 28);
            btn30Days.TabIndex = 13;
            btn30Days.Text = "30 ditët e fundit";
            btn30Days.UseVisualStyleBackColor = false;
            btn30Days.Click += btn30Days_Click;
            // 
            // dataGridView2
            // 
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = SystemColors.Control;
            dataGridViewCellStyle10.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = SystemColors.Window;
            dataGridViewCellStyle11.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle11.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            dataGridView2.DefaultCellStyle = dataGridViewCellStyle11;
            dataGridView2.Location = new Point(14, 449);
            dataGridView2.MultiSelect = false;
            dataGridView2.Name = "dataGridView2";
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = SystemColors.Control;
            dataGridViewCellStyle12.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle12.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle12.SelectionForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
            dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView2.Size = new Size(1120, 171);
            dataGridView2.TabIndex = 14;
            // 
            // labelProducts
            // 
            labelProducts.AutoSize = true;
            labelProducts.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelProducts.Location = new Point(18, 422);
            labelProducts.Name = "labelProducts";
            labelProducts.Size = new Size(236, 23);
            labelProducts.TabIndex = 15;
            labelProducts.Text = "Top produktet sipas shitjeve";
            // 
            // labelNoSales
            // 
            labelNoSales.BackColor = SystemColors.ControlDark;
            labelNoSales.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNoSales.Location = new Point(256, 195);
            labelNoSales.Name = "labelNoSales";
            labelNoSales.Size = new Size(286, 25);
            labelNoSales.TabIndex = 16;
            labelNoSales.Text = "Nuk ka shitje të bëra për këtë periudhë.";
            labelNoSales.Visible = false;
            // 
            // labelNoResult
            // 
            labelNoResult.AutoSize = true;
            labelNoResult.Font = new Font("Bookman Old Style", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelNoResult.Location = new Point(993, 292);
            labelNoResult.Name = "labelNoResult";
            labelNoResult.Size = new Size(112, 18);
            labelNoResult.TabIndex = 17;
            labelNoResult.Text = "Nuk ka rezultat";
            labelNoResult.Visible = false;
            // 
            // FrmSalesList
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1346, 660);
            Controls.Add(labelNoResult);
            Controls.Add(labelNoSales);
            Controls.Add(labelProducts);
            Controls.Add(dataGridView2);
            Controls.Add(btn30Days);
            Controls.Add(btn7Days);
            Controls.Add(btnYesterday);
            Controls.Add(btnToday);
            Controls.Add(txtBoxTotali);
            Controls.Add(labelTotali);
            Controls.Add(btnSearch);
            Controls.Add(txtBoxName);
            Controls.Add(labelName);
            Controls.Add(labelTo);
            Controls.Add(labelFrom);
            Controls.Add(datePickerTo);
            Controls.Add(datePickerFrom);
            Controls.Add(dataGridView1);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmSalesList";
            Text = "FrmSalesList";
            Load += FrmSalesList_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DateTimePicker datePickerFrom;
        private DateTimePicker datePickerTo;
        private Label labelFrom;
        private Label labelTo;
        private Label labelName;
        private TextBox txtBoxName;
        private Button btnSearch;
        private Label labelTotali;
        private TextBox txtBoxTotali;
        private Button btnToday;
        private Button btnYesterday;
        private Button btn7Days;
        private Button btn30Days;
        private DataGridView dataGridView2;
        private Label labelProducts;
        private Label labelNoSales;
        private Label labelNoResult;
    }
}