#nullable enable

using System.Windows.Forms;

namespace JobRunner.Dialogs
{
    public partial class AutoActionDialog : Form
    {
        private string? TimerText { get; set; }
        private int? SecondsLeft { get; set; } = 5;

        public AutoActionDialog()
        {
            InitializeComponent();
        }

        public static bool CheckAutoStart(IWin32Window owner)
        {
            var x = new AutoActionDialog
            {
                Text = @"Auto start",
                TimerText = "Jobs will start in ¤ seconds..."
            };
            return x.ShowDialog(owner) == DialogResult.OK;
        }

        public static bool CheckAutoClose(IWin32Window owner)
        {
            var x = new AutoActionDialog
            {
                Text = @"Auto close",
                TimerText = "Application will close in ¤ seconds..."
            };
            return x.ShowDialog(owner) == DialogResult.OK;
        }

        private void Timer1_Tick(object sender, System.EventArgs e)
        {
            lblTimer.Text = TimerText.Replace("¤", SecondsLeft.ToString());
            SecondsLeft--;
            if (SecondsLeft > -1)
                return;
            timer1.Enabled = false;
            DialogResult = DialogResult.OK;
        }

        private void AutoActionDialog_Load(object sender, System.EventArgs e)
        {
            Timer1_Tick(sender, e);
            timer1.Enabled = true;
        }

        private void BtnOk_Click(object sender, System.EventArgs e) =>
            DialogResult = DialogResult.OK;
    }
}