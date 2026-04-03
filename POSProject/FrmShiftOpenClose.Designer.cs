namespace POSProject
{
    partial class FrmShiftOpenClose
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
            labelCurrentUser = new Label();
            txtBoxOpeningBalance = new TextBox();
            txtBoxShiftComment = new TextBox();
            btnOpenShift = new Button();
            labelShiftStatus = new Label();
            labelOpenedAt = new Label();
            labelOpeningBalance = new Label();
            labelCashSales = new Label();
            labelCashIn = new Label();
            labelCashOut = new Label();
            labelExpectedCash = new Label();
            labelDifference = new Label();
            txtBoxClosingBalanceActual = new TextBox();
            txtBoxClosingComment = new TextBox();
            btnCloseShift = new Button();
            btnCashIn = new Button();
            btnCashOut = new Button();
            labelSuggestedOpening = new Label();
            SuspendLayout();
            // 
            // labelCurrentUser
            // 
            labelCurrentUser.AutoSize = true;
            labelCurrentUser.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            labelCurrentUser.Location = new Point(204, 79);
            labelCurrentUser.Name = "labelCurrentUser";
            labelCurrentUser.Size = new Size(63, 26);
            labelCurrentUser.TabIndex = 0;
            labelCurrentUser.Text = "User";
            // 
            // txtBoxOpeningBalance
            // 
            txtBoxOpeningBalance.Location = new Point(315, 165);
            txtBoxOpeningBalance.Name = "txtBoxOpeningBalance";
            txtBoxOpeningBalance.PlaceholderText = "Balanci fillestar";
            txtBoxOpeningBalance.Size = new Size(286, 25);
            txtBoxOpeningBalance.TabIndex = 1;
            // 
            // txtBoxShiftComment
            // 
            txtBoxShiftComment.Location = new Point(315, 210);
            txtBoxShiftComment.Name = "txtBoxShiftComment";
            txtBoxShiftComment.PlaceholderText = "Koment";
            txtBoxShiftComment.Size = new Size(286, 25);
            txtBoxShiftComment.TabIndex = 2;
            // 
            // btnOpenShift
            // 
            btnOpenShift.BackColor = Color.LightSteelBlue;
            btnOpenShift.Location = new Point(451, 255);
            btnOpenShift.Name = "btnOpenShift";
            btnOpenShift.Size = new Size(150, 59);
            btnOpenShift.TabIndex = 3;
            btnOpenShift.Text = "Fillo ndërrimin";
            btnOpenShift.UseVisualStyleBackColor = false;
            // 
            // labelShiftStatus
            // 
            labelShiftStatus.AutoSize = true;
            labelShiftStatus.Location = new Point(39, 165);
            labelShiftStatus.Name = "labelShiftStatus";
            labelShiftStatus.Size = new Size(54, 19);
            labelShiftStatus.TabIndex = 4;
            labelShiftStatus.Text = "label2";
            // 
            // labelOpenedAt
            // 
            labelOpenedAt.AutoSize = true;
            labelOpenedAt.Location = new Point(39, 210);
            labelOpenedAt.Name = "labelOpenedAt";
            labelOpenedAt.Size = new Size(54, 19);
            labelOpenedAt.TabIndex = 5;
            labelOpenedAt.Text = "label3";
            // 
            // labelOpeningBalance
            // 
            labelOpeningBalance.AutoSize = true;
            labelOpeningBalance.Location = new Point(39, 255);
            labelOpeningBalance.Name = "labelOpeningBalance";
            labelOpeningBalance.Size = new Size(54, 19);
            labelOpeningBalance.TabIndex = 6;
            labelOpeningBalance.Text = "label4";
            // 
            // labelCashSales
            // 
            labelCashSales.AutoSize = true;
            labelCashSales.Location = new Point(39, 303);
            labelCashSales.Name = "labelCashSales";
            labelCashSales.Size = new Size(54, 19);
            labelCashSales.TabIndex = 7;
            labelCashSales.Text = "label5";
            // 
            // labelCashIn
            // 
            labelCashIn.AutoSize = true;
            labelCashIn.Location = new Point(39, 351);
            labelCashIn.Name = "labelCashIn";
            labelCashIn.Size = new Size(54, 19);
            labelCashIn.TabIndex = 8;
            labelCashIn.Text = "label6";
            // 
            // labelCashOut
            // 
            labelCashOut.AutoSize = true;
            labelCashOut.Location = new Point(39, 398);
            labelCashOut.Name = "labelCashOut";
            labelCashOut.Size = new Size(54, 19);
            labelCashOut.TabIndex = 9;
            labelCashOut.Text = "label7";
            // 
            // labelExpectedCash
            // 
            labelExpectedCash.AutoSize = true;
            labelExpectedCash.Location = new Point(39, 444);
            labelExpectedCash.Name = "labelExpectedCash";
            labelExpectedCash.Size = new Size(54, 19);
            labelExpectedCash.TabIndex = 10;
            labelExpectedCash.Text = "label8";
            // 
            // labelDifference
            // 
            labelDifference.AutoSize = true;
            labelDifference.Location = new Point(39, 492);
            labelDifference.Name = "labelDifference";
            labelDifference.Size = new Size(54, 19);
            labelDifference.TabIndex = 11;
            labelDifference.Text = "label9";
            // 
            // txtBoxClosingBalanceActual
            // 
            txtBoxClosingBalanceActual.Location = new Point(315, 371);
            txtBoxClosingBalanceActual.Name = "txtBoxClosingBalanceActual";
            txtBoxClosingBalanceActual.PlaceholderText = "Bilanci aktual";
            txtBoxClosingBalanceActual.Size = new Size(286, 25);
            txtBoxClosingBalanceActual.TabIndex = 12;
            // 
            // txtBoxClosingComment
            // 
            txtBoxClosingComment.Location = new Point(315, 413);
            txtBoxClosingComment.Name = "txtBoxClosingComment";
            txtBoxClosingComment.PlaceholderText = "Koment";
            txtBoxClosingComment.Size = new Size(286, 25);
            txtBoxClosingComment.TabIndex = 13;
            // 
            // btnCloseShift
            // 
            btnCloseShift.BackColor = Color.LightSteelBlue;
            btnCloseShift.Location = new Point(451, 461);
            btnCloseShift.Name = "btnCloseShift";
            btnCloseShift.Size = new Size(150, 61);
            btnCloseShift.TabIndex = 14;
            btnCloseShift.Text = "Mbyll ndërrimin";
            btnCloseShift.UseVisualStyleBackColor = false;
            // 
            // btnCashIn
            // 
            btnCashIn.BackColor = Color.LightGreen;
            btnCashIn.Location = new Point(301, 3);
            btnCashIn.Name = "btnCashIn";
            btnCashIn.Size = new Size(123, 45);
            btnCashIn.TabIndex = 15;
            btnCashIn.Text = "CASH IN";
            btnCashIn.UseVisualStyleBackColor = false;
            // 
            // btnCashOut
            // 
            btnCashOut.BackColor = Color.LightCoral;
            btnCashOut.Location = new Point(430, 4);
            btnCashOut.Name = "btnCashOut";
            btnCashOut.Size = new Size(123, 44);
            btnCashOut.TabIndex = 16;
            btnCashOut.Text = "CASH OUT";
            btnCashOut.UseVisualStyleBackColor = false;
            // 
            // labelSuggestedOpening
            // 
            labelSuggestedOpening.AutoSize = true;
            labelSuggestedOpening.Location = new Point(315, 129);
            labelSuggestedOpening.Name = "labelSuggestedOpening";
            labelSuggestedOpening.Size = new Size(197, 19);
            labelSuggestedOpening.TabIndex = 17;
            labelSuggestedOpening.Text = "Bilanci hyrës i sugjeruar";
            // 
            // FrmShiftOpenClose
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(613, 580);
            Controls.Add(labelSuggestedOpening);
            Controls.Add(btnCashOut);
            Controls.Add(btnCashIn);
            Controls.Add(btnCloseShift);
            Controls.Add(txtBoxClosingComment);
            Controls.Add(txtBoxClosingBalanceActual);
            Controls.Add(labelDifference);
            Controls.Add(labelExpectedCash);
            Controls.Add(labelCashOut);
            Controls.Add(labelCashIn);
            Controls.Add(labelCashSales);
            Controls.Add(labelOpeningBalance);
            Controls.Add(labelOpenedAt);
            Controls.Add(labelShiftStatus);
            Controls.Add(btnOpenShift);
            Controls.Add(txtBoxShiftComment);
            Controls.Add(txtBoxOpeningBalance);
            Controls.Add(labelCurrentUser);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmShiftOpenClose";
            Text = "FrmShiftOpenClose";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelCurrentUser;
        private TextBox txtBoxOpeningBalance;
        private TextBox txtBoxShiftComment;
        private Button btnOpenShift;
        private Label labelShiftStatus;
        private Label labelOpenedAt;
        private Label labelOpeningBalance;
        private Label labelCashSales;
        private Label labelCashIn;
        private Label labelCashOut;
        private Label labelExpectedCash;
        private Label labelDifference;
        private TextBox txtBoxClosingBalanceActual;
        private TextBox txtBoxClosingComment;
        private Button btnCloseShift;
        private Button btnCashIn;
        private Button btnCashOut;
        private Label labelSuggestedOpening;
    }
}