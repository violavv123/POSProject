namespace POSProject
{
    partial class FrmPOS
    {
        
        private System.ComponentModel.IContainer components = null;

       
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPOS));
            txtBoxBarkodi = new TextBox();
            labelBarkodi = new Label();
            btnAdd = new Button();
            dataGridView1 = new DataGridView();
            ClmnProdukti = new DataGridViewTextBoxColumn();
            ClmnSasia = new DataGridViewTextBoxColumn();
            ClmnCmimi = new DataGridViewTextBoxColumn();
            ClmnVlera = new DataGridViewTextBoxColumn();
            txtBoxTotali = new TextBox();
            btnRemove = new Button();
            btnFinish = new Button();
            txtBoxKoment = new TextBox();
            labelKoment = new Label();
            pictureBox1 = new PictureBox();
            txtBoxPaguar = new TextBox();
            txtBoxKusuri = new TextBox();
            labelPaguar = new Label();
            labelKusuri = new Label();
            btnSasia = new Button();
            btnNderroCmimin = new Button();
            labelUsername = new Label();
            btnAddUsers = new Button();
            btnSalesList = new Button();
            btnAddProducts = new Button();
            btnMethodPayment = new Button();
            btnNotifications = new Button();
            btnLogOut = new Button();
            btnShift = new Button();
            btnDiscountItem = new Button();
            btnDiscountInvoice = new Button();
            btnReturn = new Button();
            btnGiftCard = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtBoxBarkodi
            // 
            txtBoxBarkodi.Location = new Point(189, 72);
            txtBoxBarkodi.Margin = new Padding(3, 4, 3, 4);
            txtBoxBarkodi.Name = "txtBoxBarkodi";
            txtBoxBarkodi.Size = new Size(241, 25);
            txtBoxBarkodi.TabIndex = 0;
            txtBoxBarkodi.TextAlign = HorizontalAlignment.Right;
            // 
            // labelBarkodi
            // 
            labelBarkodi.AutoSize = true;
            labelBarkodi.Location = new Point(196, 74);
            labelBarkodi.Name = "labelBarkodi";
            labelBarkodi.Size = new Size(77, 19);
            labelBarkodi.TabIndex = 1;
            labelBarkodi.Text = "Barkodi :";
            // 
            // btnAdd
            // 
            btnAdd.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Location = new Point(1131, 260);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(179, 44);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Shto produktin";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            dataGridView1.Location = new Point(12, 140);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
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
            dataGridView1.Size = new Size(770, 405);
            dataGridView1.TabIndex = 7;
            // 
            // ClmnProdukti
            // 
            ClmnProdukti.MinimumWidth = 6;
            ClmnProdukti.Name = "ClmnProdukti";
            ClmnProdukti.Width = 125;
            // 
            // ClmnSasia
            // 
            ClmnSasia.MinimumWidth = 6;
            ClmnSasia.Name = "ClmnSasia";
            ClmnSasia.Width = 125;
            // 
            // ClmnCmimi
            // 
            ClmnCmimi.MinimumWidth = 6;
            ClmnCmimi.Name = "ClmnCmimi";
            ClmnCmimi.Width = 125;
            // 
            // ClmnVlera
            // 
            ClmnVlera.MinimumWidth = 6;
            ClmnVlera.Name = "ClmnVlera";
            ClmnVlera.Width = 125;
            // 
            // txtBoxTotali
            // 
            txtBoxTotali.Font = new Font("Bookman Old Style", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBoxTotali.Location = new Point(1032, -2);
            txtBoxTotali.Margin = new Padding(3, 4, 3, 4);
            txtBoxTotali.Multiline = true;
            txtBoxTotali.Name = "txtBoxTotali";
            txtBoxTotali.PlaceholderText = "0.00";
            txtBoxTotali.ReadOnly = true;
            txtBoxTotali.Size = new Size(277, 68);
            txtBoxTotali.TabIndex = 9;
            txtBoxTotali.TextAlign = HorizontalAlignment.Right;
            // 
            // btnRemove
            // 
            btnRemove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRemove.BackColor = Color.LightCoral;
            btnRemove.Location = new Point(1131, 310);
            btnRemove.Margin = new Padding(3, 4, 3, 4);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(177, 44);
            btnRemove.TabIndex = 10;
            btnRemove.Text = "Fshije rreshtin (F6)";
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnFinish
            // 
            btnFinish.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnFinish.BackColor = Color.LightSteelBlue;
            btnFinish.Location = new Point(1131, 165);
            btnFinish.Margin = new Padding(3, 4, 3, 4);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(179, 88);
            btnFinish.TabIndex = 11;
            btnFinish.Text = "Përfundo (F12)";
            btnFinish.UseVisualStyleBackColor = false;
            btnFinish.Click += btnFinish_Click;
            // 
            // txtBoxKoment
            // 
            txtBoxKoment.Location = new Point(929, 492);
            txtBoxKoment.Margin = new Padding(3, 4, 3, 4);
            txtBoxKoment.Multiline = true;
            txtBoxKoment.Name = "txtBoxKoment";
            txtBoxKoment.Size = new Size(354, 53);
            txtBoxKoment.TabIndex = 12;
            // 
            // labelKoment
            // 
            labelKoment.AutoSize = true;
            labelKoment.Location = new Point(831, 495);
            labelKoment.Name = "labelKoment";
            labelKoment.Size = new Size(77, 19);
            labelKoment.TabIndex = 13;
            labelKoment.Text = "Koment :";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-1, -2);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(101, 97);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // txtBoxPaguar
            // 
            txtBoxPaguar.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBoxPaguar.Location = new Point(826, 72);
            txtBoxPaguar.Margin = new Padding(3, 4, 3, 4);
            txtBoxPaguar.Multiline = true;
            txtBoxPaguar.Name = "txtBoxPaguar";
            txtBoxPaguar.PlaceholderText = "0.00";
            txtBoxPaguar.Size = new Size(200, 34);
            txtBoxPaguar.TabIndex = 17;
            txtBoxPaguar.TextAlign = HorizontalAlignment.Right;
            // 
            // txtBoxKusuri
            // 
            txtBoxKusuri.BackColor = SystemColors.Window;
            txtBoxKusuri.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBoxKusuri.Location = new Point(1032, 70);
            txtBoxKusuri.Margin = new Padding(3, 4, 3, 4);
            txtBoxKusuri.Multiline = true;
            txtBoxKusuri.Name = "txtBoxKusuri";
            txtBoxKusuri.PlaceholderText = "0.00";
            txtBoxKusuri.ReadOnly = true;
            txtBoxKusuri.Size = new Size(277, 35);
            txtBoxKusuri.TabIndex = 18;
            txtBoxKusuri.TextAlign = HorizontalAlignment.Right;
            // 
            // labelPaguar
            // 
            labelPaguar.AutoSize = true;
            labelPaguar.Location = new Point(831, 78);
            labelPaguar.Name = "labelPaguar";
            labelPaguar.Size = new Size(66, 19);
            labelPaguar.TabIndex = 19;
            labelPaguar.Text = "Paguar:";
            // 
            // labelKusuri
            // 
            labelKusuri.AutoSize = true;
            labelKusuri.Location = new Point(1041, 78);
            labelKusuri.Name = "labelKusuri";
            labelKusuri.Size = new Size(65, 19);
            labelKusuri.TabIndex = 20;
            labelKusuri.Text = "Kusuri:";
            // 
            // btnSasia
            // 
            btnSasia.BackColor = Color.LightYellow;
            btnSasia.Location = new Point(1131, 357);
            btnSasia.Margin = new Padding(3, 4, 3, 4);
            btnSasia.Name = "btnSasia";
            btnSasia.Size = new Size(179, 37);
            btnSasia.TabIndex = 21;
            btnSasia.Text = "Sasia (F2)";
            btnSasia.UseVisualStyleBackColor = false;
            // 
            // btnNderroCmimin
            // 
            btnNderroCmimin.BackColor = Color.SandyBrown;
            btnNderroCmimin.Location = new Point(1131, 401);
            btnNderroCmimin.Margin = new Padding(3, 4, 3, 4);
            btnNderroCmimin.Name = "btnNderroCmimin";
            btnNderroCmimin.Size = new Size(179, 45);
            btnNderroCmimin.TabIndex = 22;
            btnNderroCmimin.Text = "Ndërro çmimin (F4)";
            btnNderroCmimin.UseVisualStyleBackColor = false;
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Location = new Point(12, 632);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(55, 19);
            labelUsername.TabIndex = 24;
            labelUsername.Text = "User: ";
            // 
            // btnAddUsers
            // 
            btnAddUsers.BackgroundImage = (Image)resources.GetObject("btnAddUsers.BackgroundImage");
            btnAddUsers.BackgroundImageLayout = ImageLayout.Zoom;
            btnAddUsers.Location = new Point(104, -2);
            btnAddUsers.Margin = new Padding(3, 4, 3, 4);
            btnAddUsers.Name = "btnAddUsers";
            btnAddUsers.Size = new Size(33, 29);
            btnAddUsers.TabIndex = 25;
            btnAddUsers.UseVisualStyleBackColor = true;
            btnAddUsers.Click += btnAddUsers_Click;
            // 
            // btnSalesList
            // 
            btnSalesList.BackColor = Color.Yellow;
            btnSalesList.Location = new Point(135, -2);
            btnSalesList.Margin = new Padding(3, 4, 3, 4);
            btnSalesList.Name = "btnSalesList";
            btnSalesList.Size = new Size(129, 29);
            btnSalesList.TabIndex = 26;
            btnSalesList.Text = "See Sales List";
            btnSalesList.UseVisualStyleBackColor = false;
            btnSalesList.Click += btnSalesList_Click;
            // 
            // btnAddProducts
            // 
            btnAddProducts.BackColor = SystemColors.ActiveCaption;
            btnAddProducts.Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddProducts.ForeColor = Color.Black;
            btnAddProducts.Location = new Point(12, 553);
            btnAddProducts.Margin = new Padding(3, 4, 3, 4);
            btnAddProducts.Name = "btnAddProducts";
            btnAddProducts.Size = new Size(109, 44);
            btnAddProducts.TabIndex = 27;
            btnAddProducts.Text = "Produktet";
            btnAddProducts.UseVisualStyleBackColor = false;
            btnAddProducts.Click += btnAddProducts_Click;
            // 
            // btnMethodPayment
            // 
            btnMethodPayment.BackColor = Color.GreenYellow;
            btnMethodPayment.Location = new Point(261, -2);
            btnMethodPayment.Name = "btnMethodPayment";
            btnMethodPayment.Size = new Size(94, 29);
            btnMethodPayment.TabIndex = 28;
            btnMethodPayment.Text = "Payment";
            btnMethodPayment.UseVisualStyleBackColor = false;
            btnMethodPayment.Click += btnMethodPayment_Click;
            // 
            // btnNotifications
            // 
            btnNotifications.Location = new Point(-1, 96);
            btnNotifications.Name = "btnNotifications";
            btnNotifications.Size = new Size(101, 35);
            btnNotifications.TabIndex = 29;
            btnNotifications.Text = "Notifications";
            btnNotifications.UseVisualStyleBackColor = true;
            // 
            // btnLogOut
            // 
            btnLogOut.BackColor = Color.Yellow;
            btnLogOut.FlatAppearance.BorderSize = 0;
            btnLogOut.Location = new Point(1191, 623);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(105, 37);
            btnLogOut.TabIndex = 30;
            btnLogOut.Text = "Log Out";
            btnLogOut.UseVisualStyleBackColor = false;
            // 
            // btnShift
            // 
            btnShift.BackColor = Color.Coral;
            btnShift.Location = new Point(142, 608);
            btnShift.Name = "btnShift";
            btnShift.Size = new Size(128, 56);
            btnShift.TabIndex = 31;
            btnShift.Text = "Ndërrimi (F3)";
            btnShift.UseVisualStyleBackColor = false;
            // 
            // btnDiscountItem
            // 
            btnDiscountItem.BackColor = Color.LightPink;
            btnDiscountItem.Location = new Point(268, 608);
            btnDiscountItem.Name = "btnDiscountItem";
            btnDiscountItem.Size = new Size(129, 56);
            btnDiscountItem.TabIndex = 32;
            btnDiscountItem.Text = "Zbritje në rresht";
            btnDiscountItem.UseVisualStyleBackColor = false;
            // 
            // btnDiscountInvoice
            // 
            btnDiscountInvoice.BackColor = Color.YellowGreen;
            btnDiscountInvoice.Location = new Point(396, 608);
            btnDiscountInvoice.Name = "btnDiscountInvoice";
            btnDiscountInvoice.Size = new Size(128, 56);
            btnDiscountInvoice.TabIndex = 33;
            btnDiscountInvoice.Text = "Zbritje në faturë";
            btnDiscountInvoice.UseVisualStyleBackColor = false;
            // 
            // btnReturn
            // 
            btnReturn.BackColor = Color.Gold;
            btnReturn.Location = new Point(626, 608);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(157, 56);
            btnReturn.TabIndex = 34;
            btnReturn.Text = "Kthim/Rimbursim";
            btnReturn.UseVisualStyleBackColor = false;
            // 
            // btnGiftCard
            // 
            btnGiftCard.BackColor = Color.HotPink;
            btnGiftCard.Location = new Point(522, 608);
            btnGiftCard.Name = "btnGiftCard";
            btnGiftCard.Size = new Size(105, 56);
            btnGiftCard.TabIndex = 35;
            btnGiftCard.Text = "Gift Card";
            btnGiftCard.UseVisualStyleBackColor = false;
            // 
            // FrmPOS
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1308, 669);
            Controls.Add(btnGiftCard);
            Controls.Add(btnReturn);
            Controls.Add(btnDiscountInvoice);
            Controls.Add(btnDiscountItem);
            Controls.Add(btnShift);
            Controls.Add(btnLogOut);
            Controls.Add(btnNotifications);
            Controls.Add(btnMethodPayment);
            Controls.Add(btnAddProducts);
            Controls.Add(btnSalesList);
            Controls.Add(btnAddUsers);
            Controls.Add(labelUsername);
            Controls.Add(btnNderroCmimin);
            Controls.Add(btnSasia);
            Controls.Add(labelKusuri);
            Controls.Add(labelPaguar);
            Controls.Add(txtBoxKusuri);
            Controls.Add(txtBoxPaguar);
            Controls.Add(pictureBox1);
            Controls.Add(labelKoment);
            Controls.Add(txtBoxKoment);
            Controls.Add(btnFinish);
            Controls.Add(btnRemove);
            Controls.Add(txtBoxTotali);
            Controls.Add(dataGridView1);
            Controls.Add(btnAdd);
            Controls.Add(labelBarkodi);
            Controls.Add(txtBoxBarkodi);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ImeMode = ImeMode.Off;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmPOS";
            Load += FrmPOS_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBoxBarkodi;
        private Label labelBarkodi;
        private Button btnAdd;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn ClmnProdukti;
        private DataGridViewTextBoxColumn ClmnSasia;
        private DataGridViewTextBoxColumn ClmnCmimi;
        private DataGridViewTextBoxColumn ClmnVlera;
        private TextBox txtBoxTotali;
        private Button btnRemove;
        private Button btnFinish;
        private TextBox txtBoxKoment;
        private Label labelKoment;
        private PictureBox pictureBox1;
        private TextBox txtBoxPaguar;
        private TextBox txtBoxKusuri;
        private Label labelPaguar;
        private Label labelKusuri;
        private Button btnSasia;
        private Button btnNderroCmimin;
        private Label labelUsername;
        private Button btnAddUsers;
        private Button btnSalesList;
        private Button btnAddProducts;
        private Button btnMethodPayment;
        private Button btnNotifications;
        private Button btnLogOut;
        private Button btnShift;
        private Button btnDiscountItem;
        private Button btnDiscountInvoice;
        private Button btnReturn;
        private Button btnGiftCard;
    }
}
