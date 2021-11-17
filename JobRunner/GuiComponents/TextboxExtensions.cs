using System;
using System.Windows.Forms;
using JobRunner.Utils;

namespace JobRunner.GuiComponents
{
    public static class TextboxExtensions
    {
        public static bool ValidateDashAndQuotes(this TextBox me, string fieldName, string formName)
        {
            if (me.Text.IndexOf("\"", StringComparison.Ordinal) >= 0)
            {
                MessageDisplayer.Tell($"{fieldName} cannot contain quotes.", formName);
                me.Focus();
                return false;
            }

            if (me.Text.StartsWith("-"))
            {
                MessageDisplayer.Tell($"{fieldName} cannot start with \"-\".", formName);
                me.Focus();
                return false;
            }

            return true;
        }
    }
}