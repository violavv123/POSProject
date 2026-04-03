using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSProject;

namespace POSProject
{
    public partial class FrmCashMovements : Form
    {
        private readonly ShiftService _shiftService = new ShiftService();
        private readonly int _shiftId;
        private readonly string _tipi;
        public FrmCashMovements(int shiftId, string tipi)
        {
            InitializeComponent();
            Load += FrmCashMovements_Load;
            _shiftId = shiftId;
            _tipi = tipi;

            btnCancel.MouseEnter += btn_MouseEnter;
            btnCancel.MouseLeave += btn_MouseLeave;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;

            btnSave.MouseEnter += btn_MouseEnter;
            btnSave.MouseLeave += btn_MouseLeave;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;

            btnSave.Click += btnSave_Click;

        }

        private void FrmCashMovements_Load(object sender, EventArgs e)
        {
            if(_tipi == "IN")
            {
                this.Text = "Cash In";
                labelTitle.Text = "Shto Cash";
            }
            else if(_tipi == "OUT")
            {
                this.Text = "Cash Out";
                labelTitle.Text = "Largo Cash";
            }
        }
        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.LightSteelBlue;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            btn.FlatAppearance.BorderSize = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtBoxShuma.Text.Trim(), out decimal shuma) || shuma <= 0)
            {
                AutoClosingMessageBox.Show("Shkruaj një shumë valide.", "Info", 900);
                txtBoxShuma.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBoxArsyeja.Text))
            {
                AutoClosingMessageBox.Show("Shkruaj arsyen.", "Info", 900);
                txtBoxArsyeja.Focus();
                return;
            }

            var shift = _shiftService.GetOpenShift();
            if (shift == null)
            {
                AutoClosingMessageBox.Show("Nuk ka ndërrim aktiv.", "Info", 900);
                return;
            }

            if (shift.Id != _shiftId)
            {
                AutoClosingMessageBox.Show("Shift-i aktiv nuk përputhet me këtë veprim.", "Info", 900);
                return;
            }

            if (_tipi == "OUT")
            {
                decimal availableCash = _shiftService.GetAvailableCash(_shiftId);

                if (shuma > availableCash)
                {
                    AutoClosingMessageBox.Show(
                        $"Nuk ka cash të mjaftueshëm në arkë. Gjendja aktuale: {availableCash:0.00} €.",
                        "Info",
                        1200);
                    return;
                }
            }

            try
            {
                _shiftService.AddCashMovement(
                    _shiftId,
                    _tipi,
                    shuma,
                    txtBoxArsyeja.Text.Trim(),
                    txtBoxKoment.Text.Trim(),
                    Session.UserId);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë ruajtjes: " + ex.Message);
            }
        }
    }
}
