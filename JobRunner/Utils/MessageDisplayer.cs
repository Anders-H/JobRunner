using System.Windows.Forms;

namespace JobRunner.Utils;

public static class MessageDisplayer
{
    public static void ShowValidationError(IWin32Window owner, string message, string text, bool quiet)
    {
        if (!quiet)
            MessageBox.Show(owner, message, text, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static bool Ask(IWin32Window owner, string message, string text) =>
        MessageBox.Show(owner, message, text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;

    public static void Tell(IWin32Window owner, string message, string text) =>
        MessageBox.Show(owner, message, text, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static void Yell(IWin32Window owner, string message, string text) =>
        MessageBox.Show(owner, message, text, MessageBoxButtons.OK, MessageBoxIcon.Error);
}