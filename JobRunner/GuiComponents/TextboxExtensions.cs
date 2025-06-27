using System;
using System.Windows.Forms;
using JobRunner.Utils;

namespace JobRunner.GuiComponents;

public static class TextboxExtensions
{
    public static bool ValidateDashAndQuotes(this TextBox me, IWin32Window owner, string fieldName, string formName)
    {
        if (me.Text.IndexOf("\"", StringComparison.Ordinal) >= 0)
        {
            MessageDisplayer.Tell(owner, $"{fieldName} cannot contain quotes.", formName);
            me.Focus();
            return false;
        }

        if (me.Text.StartsWith("-"))
        {
            MessageDisplayer.Tell(owner, $"{fieldName} cannot start with \"-\".", formName);
            me.Focus();
            return false;
        }

        return true;
    }
}