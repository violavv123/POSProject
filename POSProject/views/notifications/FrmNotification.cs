using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSProject.models;
using POSProject.repositories.notifications;
using POSProject.services.notifications;

namespace POSProject
{
    public partial class FrmNotification : Form
    {
        private DataTable notificationsTable = new DataTable();
        private readonly INotificationService _notifService;
        private List<NotificationModel> _notifs = new List<NotificationModel>();
        public FrmNotification()
        {
            InitializeComponent();
            INotificationRepository notifRepo = new NotificationRepository();
            _notifService = new NotificationService(notifRepo);
            
            btnRefresh.Click += btnRefresh_Click;
            chckOnlyUnread.CheckedChanged += chckOnlyUnread_CheckedChanged;
            btnMarkAsRead.Click += btnMarkAsRead_Click;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            btnClose.Click += btnClose_Click;
        }

        private void FrmNotification_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadNotifications();
        }

        private void SetupGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns.Clear();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                Visible = false
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Created_At",
                HeaderText = "Data",
                DataPropertyName = "Created_At",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Severity",
                HeaderText = "Severity",
                DataPropertyName = "Severity",
                Width = 90
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Type",
                HeaderText = "Type",
                DataPropertyName = "Type",
                Width = 120
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Titulli",
                DataPropertyName = "Title",
                Width = 180
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Message",
                HeaderText = "Mesazhi",
                DataPropertyName = "Message",
                Width = 350,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IsRead",
                HeaderText = "Lexuar",
                DataPropertyName = "IsRead",
                Width = 70
            });
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }

        private void LoadNotifications()
        {
            _notifs = _notifService.GetNotifications(chckOnlyUnread.Checked, 30, 200);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _notifs;
            labelCount.Text = $"Gjithsej njoftime: {dataGridView1.Rows.Count}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadNotifications();
        }

        private void chckOnlyUnread_CheckedChanged(object sender, EventArgs e)
        {
            LoadNotifications();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells["IsRead"].Value == DBNull.Value)
                return;
            bool isRead = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["IsRead"].Value);
            string severity = dataGridView1.Rows[e.RowIndex].Cells["Severity"].Value?.ToString() ?? "";
            if (!isRead)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Regular);
            }

            if (severity.Equals("Error", StringComparison.OrdinalIgnoreCase))
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
            }
            else if (severity.Equals("Warning", StringComparison.OrdinalIgnoreCase))
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LemonChiffon;
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMarkAsRead_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                AutoClosingMessageBox.Show("Zgjedh një njoftim.", "Informacion", 900);
                return;
            }
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            MarkNotificationAsRead(id);
            LoadNotifications();
        }

        private void MarkNotificationAsRead(int id)
        {
            _notifService.MarkAsRead(id);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string title = dataGridView1.Rows[e.RowIndex].Cells["Title"].Value?.ToString() ?? "";
            string message = dataGridView1.Rows[e.RowIndex].Cells["Message"].Value?.ToString() ?? "";
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            MarkNotificationAsRead(id);
            LoadNotifications();
        }

    }
}
