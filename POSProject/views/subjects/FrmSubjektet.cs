using System.Data;
using Npgsql;
using POSProject.models;
using POSProject.repositories.notifications;
using POSProject.repositories.subjects;
using POSProject.services.notifications;
using POSProject.services.subjects;

namespace POSProject
{
    public partial class FrmSubjektet : Form
    {
        private int? selectedSubjektiId = null;
        private AutoCompleteStringCollection subjectList = new AutoCompleteStringCollection();
        private bool isDeleteMode = false;
        private readonly INotificationService _notifsService;
        private readonly SubjectService _subjectService;
        public FrmSubjektet()
        {
            InitializeComponent();
            INotificationRepository notifsRepo = new NotificationRepository();
            _notifsService = new NotificationService(notifsRepo);
            ISubjectRepository subjectRepo = new SubjectRepository();
            _subjectService = new SubjectService(subjectRepo);
            LoadSubjectAutoComplete();
            this.Load += FrmSubjektet_Load;
            btnSave.Click += btnSave_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;
            txtBoxPershkrimi.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBoxPershkrimi.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBoxPershkrimi.KeyDown += txtPershkrimi_KeyDown;
            txtBoxNrFiskal.KeyDown += txtNrFiskal_KeyDown;
            txtBoxAdresa.KeyDown += txtAdresa_KeyDown;
        }

        private void FrmSubjektet_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadLlojet();
            LoadSubjects();
            ClearFields();
        }

        private void SetupGrid()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadLlojet()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Biznes");
            comboBox1.Items.Add("Kompani");
            comboBox1.Items.Add("Privat");
            comboBox1.Items.Add("OJQ");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = -1;
        }

        private void LoadSubjects()
        {
            DataTable dt = _subjectService.GetAllSubjects();
            dataGridView1.DataSource = dt;

            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
            if (dataGridView1.Columns["Pershkrimi"] != null)
                dataGridView1.Columns["Pershkrimi"].HeaderText = "Përshkrimi";
            if (dataGridView1.Columns["NumriFiskal"] != null)
                dataGridView1.Columns["NumriFiskal"].HeaderText = "Numri Fiskal";
            if (dataGridView1.Columns["Adresa"] != null)
                dataGridView1.Columns["Adresa"].HeaderText = "Adresa";
            if (dataGridView1.Columns["LlojiSubjektit"] != null)
                dataGridView1.Columns["LlojiSubjektit"].HeaderText = "Lloji";
        }

        private bool ValidateSubject()
        {
            if (string.IsNullOrWhiteSpace(txtBoxPershkrimi.Text))
            {
                AutoClosingMessageBox.Show("Shkruaj përshkrimin.", "Informacion", 2000);
                txtBoxPershkrimi.Focus();
                return false;
            }
            return true;
        }
        private void ClearFields()
        {
            selectedSubjektiId = null;
            isDeleteMode = false;
            txtBoxPershkrimi.Clear();
            txtBoxNrFiskal.Clear();
            txtBoxAdresa.Clear();
            comboBox1.SelectedIndex = -1;
            dataGridView1.ClearSelection();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            SetNormalMode();
            txtBoxPershkrimi.Focus();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            selectedSubjektiId = Convert.ToInt32(row.Cells["Id"].Value);
            isDeleteMode = false;
            txtBoxPershkrimi.Text = row.Cells["Pershkrimi"].Value?.ToString() ?? "";
            txtBoxNrFiskal.Text = row.Cells["NumriFiskal"].Value?.ToString() ?? "";
            txtBoxAdresa.Text = row.Cells["Adresa"].Value?.ToString() ?? "";

            string lloji = row.Cells["LlojiSubjektit"].Value?.ToString() ?? "";
            if (comboBox1.Items.Contains(lloji))
                comboBox1.SelectedItem = lloji;
            else
                comboBox1.SelectedIndex = -1;

            SetNormalMode();
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            txtBoxPershkrimi.Focus();
            txtBoxPershkrimi.SelectionStart = txtBoxPershkrimi.Text.Length;
        }

        private void SetDeleteMode(bool deleteMode)
        {
            txtBoxPershkrimi.Visible = true;
            labelPershkrimi.Visible = true;
            txtBoxNrFiskal.Visible = !deleteMode;
            labelNrFiskal.Visible = !deleteMode;
            txtBoxAdresa.Visible = !deleteMode;
            labelAdresa.Visible = !deleteMode;
            comboBox1.Visible = !deleteMode;
            labelLloji.Visible = !deleteMode;
            if (deleteMode)
            {
                txtBoxNrFiskal.Clear();
                txtBoxAdresa.Clear();
                comboBox1.SelectedIndex = -1;
            }
            btnSave.Enabled = !deleteMode;
            btnUpdate.Enabled = !deleteMode && selectedSubjektiId != null;
            btnDelete.Enabled = selectedSubjektiId != null;
        }
        private void SetNormalMode()
        {
            txtBoxPershkrimi.Visible = true;
            labelPershkrimi.Visible = true;

            txtBoxNrFiskal.Visible = true;
            labelNrFiskal.Visible = true;

            txtBoxAdresa.Visible = true;
            labelAdresa.Visible = true;

            comboBox1.Visible = true;
            labelLloji.Visible = true;

            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateSubject())
                return;

            try
            {
                var subject = new SubjectModel
                {
                    Pershkrimi = txtBoxPershkrimi.Text.Trim(),
                    NumriFiskal = string.IsNullOrWhiteSpace(txtBoxNrFiskal.Text) ? null : txtBoxNrFiskal.Text.Trim(),
                    Adresa = string.IsNullOrWhiteSpace(txtBoxAdresa.Text) ? null : txtBoxAdresa.Text.Trim(),
                    LlojiSubjektit = comboBox1.SelectedIndex == -1 ? null : comboBox1.SelectedItem.ToString()
                };

                _subjectService.CreateSubject(subject);

                AutoClosingMessageBox.Show("Subjekti u ruajt me sukses.", "Informacion", 900);
                _notifsService.Create("SUBJECT_CREATED", "Info", "Subjekt i ri", txtBoxPershkrimi.Text, "Subjektet", null, Session.UserId);

                LoadSubjects();
                LoadSubjectAutoComplete();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ruajtjes: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedSubjektiId == null)
            {
                MessageBox.Show("Zgjedh një subjekt nga lista");
                return;
            }

            if (!ValidateSubject())
                return;

            try
            {
                var subject = new SubjectModel
                {
                    Id = selectedSubjektiId.Value,
                    Pershkrimi = txtBoxPershkrimi.Text.Trim(),
                    NumriFiskal = string.IsNullOrWhiteSpace(txtBoxNrFiskal.Text) ? null : txtBoxNrFiskal.Text.Trim(),
                    Adresa = string.IsNullOrWhiteSpace(txtBoxAdresa.Text) ? null : txtBoxAdresa.Text.Trim(),
                    LlojiSubjektit = comboBox1.SelectedIndex == -1 ? null : comboBox1.SelectedItem.ToString()
                };

                _subjectService.UpdateSubject(subject);

                AutoClosingMessageBox.Show("Subjekti u përditësua me sukses.", "Informacion", 900);
                _notifsService.Create("SUBJECT_UPDATED", "Info", "Subjekt i përditësuar", txtBoxPershkrimi.Text, "Subjektet", selectedSubjektiId.Value, Session.UserId);

                LoadSubjects();
                LoadSubjectAutoComplete();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë editimit: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedSubjektiId == null)
            {
                MessageBox.Show("Zgjedh një subjekt nga lista.");
                return;
            }

            if (!isDeleteMode)
            {
                isDeleteMode = true;
                SetDeleteMode(true);
                txtBoxPershkrimi.Focus();

                AutoClosingMessageBox.Show(
                    "Delete mode aktiv. Kliko përsëri Fshij për ta konfirmuar.",
                    "Informacion",
                    2000
                );
                return;
            }

            DialogResult result = MessageBox.Show(
                "A jeni të sigurt që dëshironi të fshini këtë subjekt?",
                "Konfirmim",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                _subjectService.DeleteSubject(selectedSubjektiId.Value);

                AutoClosingMessageBox.Show("Subjekti u fshi me sukses.", "Informacion", 900);
                _notifsService.Create("SUBJECT_DELETED", "Warning", "Subjekt i fshirë", txtBoxPershkrimi.Text, "Subjektet", selectedSubjektiId.Value, Session.UserId);

                LoadSubjects();
                LoadSubjectAutoComplete();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë fshirjes: " + ex.Message);
            }
        }

        private void LoadSubjectAutoComplete()
        {
            subjectList.Clear();

            List<string> names = _subjectService.GetSubjectNames();
            foreach (var name in names)
            {
                subjectList.Add(name);
            }

            txtBoxPershkrimi.AutoCompleteCustomSource = subjectList;
        }

        private void txtPershkrimi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtBoxNrFiskal.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtNrFiskal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtBoxAdresa.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtAdresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                comboBox1.Focus();
                e.SuppressKeyPress = true;
            }
        }

    }
}
