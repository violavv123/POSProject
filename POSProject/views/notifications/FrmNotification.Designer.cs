namespace POSProject
{
    partial class FrmNotification
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
            panelButtons = new Panel();
            btnClose = new Button();
            btnRefresh = new Button();
            btnMarkAsRead = new Button();
            labelNotifications = new Label();
            labelCount = new Label();
            chckOnlyUnread = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panelButtons.SuspendLayout();
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
            dataGridView1.Location = new Point(12, 164);
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
            dataGridView1.Size = new Size(880, 423);
            dataGridView1.TabIndex = 0;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnClose);
            panelButtons.Controls.Add(btnRefresh);
            panelButtons.Controls.Add(btnMarkAsRead);
            panelButtons.Location = new Point(194, 95);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(469, 63);
            panelButtons.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.LightCoral;
            btnClose.Location = new Point(323, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(143, 57);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.LightGoldenrodYellow;
            btnRefresh.Location = new Point(172, 3);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(145, 57);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnMarkAsRead
            // 
            btnMarkAsRead.BackColor = Color.LightGreen;
            btnMarkAsRead.Location = new Point(3, 3);
            btnMarkAsRead.Name = "btnMarkAsRead";
            btnMarkAsRead.Size = new Size(163, 57);
            btnMarkAsRead.TabIndex = 2;
            btnMarkAsRead.Text = "Mark as Read";
            btnMarkAsRead.UseVisualStyleBackColor = false;
            // 
            // labelNotifications
            // 
            labelNotifications.AutoSize = true;
            labelNotifications.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelNotifications.Location = new Point(194, 55);
            labelNotifications.Name = "labelNotifications";
            labelNotifications.Size = new Size(132, 23);
            labelNotifications.TabIndex = 2;
            labelNotifications.Text = "Notifications";
            // 
            // labelCount
            // 
            labelCount.AutoSize = true;
            labelCount.Font = new Font("Bookman Old Style", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCount.Location = new Point(592, 55);
            labelCount.Name = "labelCount";
            labelCount.Size = new Size(71, 23);
            labelCount.TabIndex = 5;
            labelCount.Text = "Count";
            // 
            // chckOnlyUnread
            // 
            chckOnlyUnread.AutoSize = true;
            chckOnlyUnread.Location = new Point(671, 616);
            chckOnlyUnread.Name = "chckOnlyUnread";
            chckOnlyUnread.Size = new Size(221, 23);
            chckOnlyUnread.TabIndex = 7;
            chckOnlyUnread.Text = "New unread Notifications";
            chckOnlyUnread.UseVisualStyleBackColor = true;
            // 
            // FrmNotification
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(904, 651);
            Controls.Add(chckOnlyUnread);
            Controls.Add(labelCount);
            Controls.Add(labelNotifications);
            Controls.Add(panelButtons);
            Controls.Add(dataGridView1);
            Font = new Font("Bookman Old Style", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmNotification";
            Text = "FrmNotification";
            Load += FrmNotification_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel panelButtons;
        private Button btnClose;
        private Button btnRefresh;
        private Button btnMarkAsRead;
        private Label labelNotifications;
        private Label labelCount;
        private CheckBox chckOnlyUnread;
    }
}