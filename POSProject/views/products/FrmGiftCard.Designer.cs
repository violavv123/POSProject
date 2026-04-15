namespace POSProject.views.products
{
    partial class FrmGiftCard
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
            labelKodi = new Label();
            txtBoxKodi = new TextBox();
            labelBarkodi = new Label();
            txtBoxBarkodi = new TextBox();
            txtBoxShumaFillestare = new TextBox();
            labelShumaFillestare = new Label();
            labelBilanciAktual = new Label();
            txtBoxBilanciAktual = new TextBox();
            dataGridView1 = new DataGridView();
            btnSearch = new Button();
            btnClose = new Button();
            btnClear = new Button();
            txtBoxCreatedAt = new TextBox();
            labelCreatedAt = new Label();
            txtBoxCreatedBy = new TextBox();
            labelCreatedBy = new Label();
            txtBoxShitjaId = new TextBox();
            labelShitjaId = new Label();
            labelTitle = new Label();
            btnUseCard = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // labelKodi
            // 
            labelKodi.AutoSize = true;
            labelKodi.Location = new Point(50, 120);
            labelKodi.Name = "labelKodi";
            labelKodi.Size = new Size(47, 19);
            labelKodi.TabIndex = 0;
            labelKodi.Text = "Kodi:";
            // 
            // txtBoxKodi
            // 
            txtBoxKodi.Location = new Point(205, 117);
            txtBoxKodi.Name = "txtBoxKodi";
            txtBoxKodi.Size = new Size(195, 25);
            txtBoxKodi.TabIndex = 1;
            // 
            // labelBarkodi
            // 
            labelBarkodi.AutoSize = true;
            labelBarkodi.Location = new Point(50, 167);
            labelBarkodi.Name = "labelBarkodi";
            labelBarkodi.Size = new Size(72, 19);
            labelBarkodi.TabIndex = 2;
            labelBarkodi.Text = "Barkodi:";
            // 
            // txtBoxBarkodi
            // 
            txtBoxBarkodi.Location = new Point(205, 164);
            txtBoxBarkodi.Name = "txtBoxBarkodi";
            txtBoxBarkodi.Size = new Size(195, 25);
            txtBoxBarkodi.TabIndex = 3;
            // 
            // txtBoxShumaFillestare
            // 
            txtBoxShumaFillestare.Location = new Point(205, 213);
            txtBoxShumaFillestare.Name = "txtBoxShumaFillestare";
            txtBoxShumaFillestare.Size = new Size(195, 25);
            txtBoxShumaFillestare.TabIndex = 4;
            // 
            // labelShumaFillestare
            // 
            labelShumaFillestare.AutoSize = true;
            labelShumaFillestare.Location = new Point(50, 213);
            labelShumaFillestare.Name = "labelShumaFillestare";
            labelShumaFillestare.Size = new Size(140, 19);
            labelShumaFillestare.TabIndex = 5;
            labelShumaFillestare.Text = "Shuma fillestare:";
            // 
            // labelBilanciAktual
            // 
            labelBilanciAktual.AutoSize = true;
            labelBilanciAktual.Location = new Point(50, 258);
            labelBilanciAktual.Name = "labelBilanciAktual";
            labelBilanciAktual.Size = new Size(121, 19);
            labelBilanciAktual.TabIndex = 6;
            labelBilanciAktual.Text = "Bilanci Aktual:";
            // 
            // txtBoxBilanciAktual
            // 
            txtBoxBilanciAktual.Location = new Point(205, 262);
            txtBoxBilanciAktual.Name = "txtBoxBilanciAktual";
            txtBoxBilanciAktual.Size = new Size(195, 25);
            txtBoxBilanciAktual.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(38, 472);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(861, 188);
            dataGridView1.TabIndex = 16;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Lavender;
            btnSearch.Location = new Point(805, 422);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 17;
            btnSearch.Text = "Kërko";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.LightCoral;
            btnClose.Font = new Font("Arial", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClose.Location = new Point(855, 44);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(53, 44);
            btnClose.TabIndex = 20;
            btnClose.Text = "x";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.LemonChiffon;
            btnClear.Location = new Point(692, 422);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 21;
            btnClear.Text = "Fshij";
            btnClear.UseVisualStyleBackColor = false;
            // 
            // txtBoxCreatedAt
            // 
            txtBoxCreatedAt.Location = new Point(205, 329);
            txtBoxCreatedAt.Name = "txtBoxCreatedAt";
            txtBoxCreatedAt.Size = new Size(195, 25);
            txtBoxCreatedAt.TabIndex = 23;
            // 
            // labelCreatedAt
            // 
            labelCreatedAt.AutoSize = true;
            labelCreatedAt.Location = new Point(50, 325);
            labelCreatedAt.Name = "labelCreatedAt";
            labelCreatedAt.Size = new Size(96, 19);
            labelCreatedAt.TabIndex = 22;
            labelCreatedAt.Text = "Krijuar më:";
            // 
            // txtBoxCreatedBy
            // 
            txtBoxCreatedBy.Location = new Point(205, 373);
            txtBoxCreatedBy.Name = "txtBoxCreatedBy";
            txtBoxCreatedBy.Size = new Size(195, 25);
            txtBoxCreatedBy.TabIndex = 25;
            // 
            // labelCreatedBy
            // 
            labelCreatedBy.AutoSize = true;
            labelCreatedBy.Location = new Point(50, 369);
            labelCreatedBy.Name = "labelCreatedBy";
            labelCreatedBy.Size = new Size(100, 19);
            labelCreatedBy.TabIndex = 24;
            labelCreatedBy.Text = "Krijuar nga:";
            // 
            // txtBoxShitjaId
            // 
            txtBoxShitjaId.Location = new Point(205, 422);
            txtBoxShitjaId.Name = "txtBoxShitjaId";
            txtBoxShitjaId.Size = new Size(195, 25);
            txtBoxShitjaId.TabIndex = 27;
            // 
            // labelShitjaId
            // 
            labelShitjaId.AutoSize = true;
            labelShitjaId.Location = new Point(50, 418);
            labelShitjaId.Name = "labelShitjaId";
            labelShitjaId.Size = new Size(81, 19);
            labelShitjaId.TabIndex = 26;
            labelShitjaId.Text = "Shitja ID:";
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTitle.Location = new Point(374, 62);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(126, 26);
            labelTitle.TabIndex = 28;
            labelTitle.Text = "Gift Cards";
            // 
            // btnUseCard
            // 
            btnUseCard.BackColor = Color.PaleGreen;
            btnUseCard.Location = new Point(772, 683);
            btnUseCard.Name = "btnUseCard";
            btnUseCard.Size = new Size(127, 60);
            btnUseCard.TabIndex = 29;
            btnUseCard.Text = "Përdor kartelën";
            btnUseCard.UseVisualStyleBackColor = false;
            // 
            // FrmGiftCard
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(932, 770);
            Controls.Add(btnUseCard);
            Controls.Add(labelTitle);
            Controls.Add(txtBoxShitjaId);
            Controls.Add(labelShitjaId);
            Controls.Add(txtBoxCreatedBy);
            Controls.Add(labelCreatedBy);
            Controls.Add(txtBoxCreatedAt);
            Controls.Add(labelCreatedAt);
            Controls.Add(btnClear);
            Controls.Add(btnClose);
            Controls.Add(btnSearch);
            Controls.Add(dataGridView1);
            Controls.Add(txtBoxBilanciAktual);
            Controls.Add(labelBilanciAktual);
            Controls.Add(labelShumaFillestare);
            Controls.Add(txtBoxShumaFillestare);
            Controls.Add(txtBoxBarkodi);
            Controls.Add(labelBarkodi);
            Controls.Add(txtBoxKodi);
            Controls.Add(labelKodi);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmGiftCard";
            Text = "FrmGiftCard";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelKodi;
        private TextBox txtBoxKodi;
        private Label labelBarkodi;
        private TextBox txtBoxBarkodi;
        private TextBox txtBoxShumaFillestare;
        private Label labelShumaFillestare;
        private Label labelBilanciAktual;
        private TextBox txtBoxBilanciAktual;
        private DataGridView dataGridView1;
        private Button btnSearch;
        private Button btnClose;
        private Button btnClear;
        private TextBox txtBoxCreatedAt;
        private Label labelCreatedAt;
        private TextBox txtBoxCreatedBy;
        private Label labelCreatedBy;
        private TextBox txtBoxShitjaId;
        private Label labelShitjaId;
        private Label labelTitle;
        private Button btnUseCard;
    }
}