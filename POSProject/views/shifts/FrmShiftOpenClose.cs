using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSProject.repositories.notifications;
using POSProject.services.notifications;

namespace POSProject
{
    public partial class FrmShiftOpenClose : Form
    {
        private readonly ShiftService _shiftService = new ShiftService();
        private CashierShiftModel? _activeShift;
        private readonly INotificationService _notifsService;

        private decimal _cashSales = 0m;
        private decimal _cashIn = 0m;
        private decimal _cashOut = 0m;
        private decimal _expectedCash = 0m;
        public FrmShiftOpenClose()
        {
            InitializeComponent();
            INotificationRepository notifsRepo = new NotificationRepository();
            _notifsService = new NotificationService(notifsRepo);
            Load += FrmShiftOpenClose_Load;
            btnOpenShift.Click += btnOpenShift_Click;
            btnCloseShift.Click += btnCloseShift_Click;
            txtBoxClosingBalanceActual.TextChanged += txtBoxClosingBalanceActual_TextChanged;
            btnCashIn.Click += btnCashIn_Click;
            btnCashOut.Click += btnCashOut_Click;
        }

        private void FrmShiftOpenClose_Load(object? sender, EventArgs e)
        {
            SetupLabels();
            LoadShiftState();
            labelCurrentUser.Text = Session.Username;
        }

        private void LoadShiftState()
        {
            _activeShift = _shiftService.GetOpenShift();

            if (_activeShift == null)
            {
                SetOpenMode();
                this.AcceptButton = btnOpenShift;

                decimal suggestedOpening = _shiftService.GetSuggestedOpeningBalance();
                txtBoxOpeningBalance.Text = suggestedOpening.ToString("0.00");

                labelSuggestedOpening.Visible = true;
                labelSuggestedOpening.Text = $"Gjendja e propozuar: {suggestedOpening:0.00} €";

                if (suggestedOpening > 0)
                    txtBoxShiftComment.Text = "Trashëguar nga ndërrimi paraprak.";
                else
                    txtBoxShiftComment.Clear();
            }
            else
            {
                labelSuggestedOpening.Visible = false;

                if (_activeShift.PerdoruesiId == Session.UserId)
                {
                    SetCloseMode();
                    this.AcceptButton = btnCloseShift;
                }
                else
                {
                    SetOtherUserOpenMode();
                }

                LoadShiftSummary();
            }
        }

        private void SetOtherUserOpenMode()
        {
            txtBoxOpeningBalance.Enabled = false;
            txtBoxShiftComment.Enabled = false;
            btnOpenShift.Enabled = false;
            btnCashIn.Enabled = false;
            btnCashOut.Enabled = false;

            txtBoxClosingBalanceActual.Enabled = false;
            txtBoxClosingComment.Enabled = false;
            btnCloseShift.Enabled = false;

            txtBoxOpeningBalance.Visible = true;
            txtBoxShiftComment.Visible = true;
            btnOpenShift.Visible = true;

            txtBoxClosingBalanceActual.Visible = false;
            txtBoxClosingComment.Visible = false;
            btnCloseShift.Visible = false;

            labelShiftStatus.Text = $"Statusi: OPEN nga user tjetër (UserId: {_activeShift.PerdoruesiId})";
        }
        private void SetCloseMode()
        {
            txtBoxOpeningBalance.Enabled = false;
            txtBoxShiftComment.Enabled = false;
            btnOpenShift.Enabled = false;

            txtBoxOpeningBalance.Visible = false;
            txtBoxShiftComment.Visible = false;
            btnOpenShift.Visible = false;

            txtBoxClosingBalanceActual.Enabled = true;
            txtBoxClosingComment.Enabled = true;
            btnCloseShift.Enabled = true;

            txtBoxClosingBalanceActual.Visible = true;
            txtBoxClosingComment.Visible = true;
            btnCloseShift.Visible = true;

            btnCashIn.Enabled = true;
            btnCashOut.Enabled = true;

            labelSuggestedOpening.Visible = false;
        }

        private void LoadShiftSummary()
        {
            if (_activeShift == null)
                return;
            _cashSales = _shiftService.GetCashSales(_activeShift.Id);
            _cashIn = _shiftService.GetCashIn(_activeShift.Id);
            _cashOut = _shiftService.GetCashOut(_activeShift.Id);
            _expectedCash = _shiftService.CalculateExpectedCash(_activeShift.OpeningBalance, _cashSales, _cashIn, _cashOut);
            labelShiftStatus.Text = $"Statusi: {_activeShift.Status}";
            labelOpenedAt.Text = $"Hapur më: {_activeShift.Opened_At:dd/MM/yyyy HH:mm}";
            labelOpeningBalance.Text = $"Gjendja hapëse: {_activeShift.OpeningBalance:0.00} €";
            labelCashSales.Text = $"Shitje cash: {_cashSales:0.00} €";
            labelCashIn.Text = $"Cash IN: {_cashIn:0.00} €";
            labelCashOut.Text = $"Cash OUT: {_cashOut:0.00} €";
            labelExpectedCash.Text = $"Cash i pritur: {_expectedCash:0.00} €";

            UpdateDifferencePreview();
        }

        private void UpdateDifferencePreview()
        {
            if (_activeShift == null)
            {
                labelDifference.Text = "Diferenca: -";
                return;
            }

            if (decimal.TryParse(txtBoxClosingBalanceActual.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal actualClosing))
            {
                decimal difference = actualClosing - _expectedCash;
                labelDifference.Text = $"Diferenca: {difference:0.00} €";
            }
            else
            {
                labelDifference.Text = "Diferenca: -";
            }

        }
        private void SetOpenMode()
        {
            txtBoxOpeningBalance.Enabled = true;
            txtBoxShiftComment.Enabled = true;
            btnOpenShift.Enabled = true;

            txtBoxOpeningBalance.Visible = true;
            txtBoxShiftComment.Visible = true;
            btnOpenShift.Visible = true;

            txtBoxClosingBalanceActual.Enabled = false;
            txtBoxClosingComment.Enabled = false;
            btnCloseShift.Enabled = false;

            txtBoxClosingBalanceActual.Visible = false;
            txtBoxClosingComment.Visible = false;
            btnCloseShift.Visible = false;

            btnCashIn.Enabled = false;
            btnCashOut.Enabled = false;

            labelSuggestedOpening.Visible = true;

            labelShiftStatus.Text = "Statusi: PA NDËRRIM AKTIV";
            labelOpenedAt.Text = "Hapur më: -";
            labelOpeningBalance.Text = "Gjendja hapëse: -";
            labelCashSales.Text = "Shitje cash: -";
            labelCashIn.Text = "Cash IN: -";
            labelCashOut.Text = "Cash OUT: -";
            labelExpectedCash.Text = "Cash i pritur: -";
            labelDifference.Text = "Diferenca: -";
        }
        private void SetupLabels()
        {
            labelCurrentUser.Text = $"Përdoruesi: {Session.Username}";
            labelShiftStatus.Text = "Statusi: -";
            labelOpenedAt.Text = "Hapur në: -";
            labelOpeningBalance.Text = "Gjendja hapëse: 0.00€";
            labelCashSales.Text = "Shitjet në cash: 0.00€";
            labelCashIn.Text = "Cash IN: 0.00€";
            labelCashOut.Text = "Cash OUT: 0.00€";
            labelExpectedCash.Text = "Cash i pritur: 0.00€";
            labelDifference.Text = "Diferenca: 0.00€";
        }

        private void btnOpenShift_Click(object sender, EventArgs e)
        {
            LoadShiftState();

            var existingOpenShift = _shiftService.GetOpenShift();
            if (existingOpenShift != null)
            {
                MessageBox.Show(
                    $"Nuk mund të hapet ndërrim i ri, sepse aktualisht ekziston një ndërrim aktiv.\n\n" +
                    $"Useri: {existingOpenShift.PerdoruesiId}\n" +
                    $"Hapur më: {existingOpenShift.Opened_At:dd/MM/yyyy HH:mm}",
                    "Ndërrim aktiv ekziston",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            decimal suggestedOpening = _shiftService.GetSuggestedOpeningBalance();
            decimal openingBalance;

            if (!decimal.TryParse(txtBoxOpeningBalance.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out openingBalance))
            {
                AutoClosingMessageBox.Show("Shkruaj një gjendje hapëse valide.", "Info", 900);
                _notifsService.Create("INVALID_OPENING_BALANCE", "Warning", "Gjendje hapëse e pavlefshme", $"Përdoruesi {Session.Username} shkroi një gjendje hapëse të pavlefshme: '{txtBoxOpeningBalance.Text.Trim()}'.", Session.Username, Session.UserId);
                txtBoxOpeningBalance.Focus();
                return;
            }

            if (openingBalance < 0)
            {
                AutoClosingMessageBox.Show("Gjendja hapëse nuk mund të jetë negative.", "Info", 900);
                return;
            }

            if (suggestedOpening > 0 && openingBalance != suggestedOpening)
            {
                AutoClosingMessageBox.Show(
                    $"Gjendja hapëse duhet të jetë e trashëguar nga ndërrimi paraprak: {suggestedOpening:0.00} €.",
                    "Info",
                    1400);
                txtBoxOpeningBalance.Focus();
                txtBoxOpeningBalance.SelectAll();
                return;
            }

            try
            {
                _shiftService.OpenShift(Session.UserId, openingBalance, txtBoxShiftComment.Text.Trim());

                AutoClosingMessageBox.Show("Ndërrimi u hap me sukses.", "Info", 900);
                _notifsService.Create("SHIFT_OPENED", "Success", "Ndërrimi u hap", $"Përdoruesi {Session.Username} hapi një ndërrim me gjendje hapëse {openingBalance:0.00} €.", Session.Username, Session.UserId);
                txtBoxOpeningBalance.Clear();
                txtBoxShiftComment.Clear();

                LoadShiftState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim gjatë hapjes së ndërrimit: " + ex.Message);
            }
        }

        private void btnCloseShift_Click(object sender, EventArgs e)
        {
            {
                if (_activeShift == null)
                {
                    AutoClosingMessageBox.Show("Nuk ka ndërrim aktiv për t'u mbyllur.", "Informacion", 1000);
                    _notifsService.Create("SHIFT_NOT_ACTIVE", "Warning", "Përpjekje për mbyllje ndërrimi", $"Përdoruesi {Session.Username} përpiqet të mbyll[ ndërrimin pa e hapur.", Session.Username, Session.UserId);
                    return;
                }

                if (!decimal.TryParse(txtBoxClosingBalanceActual.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal actualClosing))
                {
                    AutoClosingMessageBox.Show("Shkruaj një gjendje reale valide.", "Info", 900);
                    txtBoxClosingBalanceActual.Focus();
                    return;
                }

                if (actualClosing < 0)
                {
                    AutoClosingMessageBox.Show("Gjendja reale nuk mund të jetë negative.", "Info", 900);
                    return;
                }

                decimal difference = actualClosing - _expectedCash;

                try
                {
                    _shiftService.CloseShift(
                        _activeShift.Id,
                        _expectedCash,
                        actualClosing,
                        difference,
                        txtBoxClosingComment.Text.Trim()
                    );

                    AutoClosingMessageBox.Show("Ndërrimi u mbyll me sukses.", "Info", 900);
                    _notifsService.Create("SHIFT_CLOSED", "Success", "Ndërrimi u mbyll", $"Përdoruesi {Session.Username} mbylli ndërrimin me gjendje {actualClosing:0.00} €, diferencë {difference:0.00} €.", Session.Username, Session.UserId);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gabim gjatë mbylljes së ndërrimit: " + ex.Message);
                }

            }
        }

        private void txtBoxClosingBalanceActual_TextChanged(object? sender, EventArgs e)
        {
            UpdateDifferencePreview();
        }

        private void btnCashIn_Click(object sender, EventArgs e)
        {
            if (_activeShift == null)
            {
                AutoClosingMessageBox.Show("Nuk ka ndërrim aktiv", "Info", 900);
                _notifsService.Create("NO_ACTIVE_SHIFT", "Warning", "Nuk ka ndërrim aktiv për shtim të cash", "Cash in nuk mund të realizohet pa filluar ndërrimi", null, null, Session.UserId);
                return;
            }
            using (var frm = new FrmCashMovements(_activeShift.Id, "IN"))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadShiftSummary();
            }

        }

        private void btnCashOut_Click(object sender, EventArgs e)
        {
            if (_activeShift == null)
            {
                AutoClosingMessageBox.Show("Nuk ka ndërrim aktiv", "Info", 900);
                _notifsService.Create("NO_ACTIVE_SHIFT", "Warning", "Nuk ka ndërrim aktiv për largim të cash", "Cash out nuk mund të realizohet pa filluar ndërrimi", null, null, Session.UserId);
                return;
            }
            using (var frm = new FrmCashMovements(_activeShift.Id, "OUT"))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadShiftSummary();
            }
        }

    }
}
