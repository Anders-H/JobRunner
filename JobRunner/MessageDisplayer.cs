﻿using System.Windows.Forms;

namespace JobRunner
{
    public static class MessageDisplayer
    {
        public static void ShowValidationError(string message, string text, bool quiet)
        {
            if (!quiet)
                MessageBox.Show(message, text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Yell(string message, string text) =>
            MessageBox.Show(message, text, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}