using JobRunner.ObjectModel;
using System;
using System.Windows.Forms;

namespace JobRunner.Dialogs;

public class EditJobDialogController : Controller
{
    private readonly EditJobDialog _form;

    public EditJobDialogController(EditJobDialog form)
    {
        _form = form;
    }

    public override void ThrowIfRequiredPropertiesAreNull()
    {
        if (_form.Variables == null)
            throw new SystemException($"Property not initialized: {nameof(_form.Variables)}");

        if (_form.Jobs == null)
            throw new SystemException($"Property not initialized: {nameof(_form.Jobs)}");

        if (_form.SaveVariables == null)
            throw new SystemException($"Property not initialized: {nameof(_form.SaveVariables)}");
    }

    public void PopulateTimeout(ComboBox cboTimeout)
    {
        foreach (var x in new TimeSpanList())
            cboTimeout.Items.Add(x);

        foreach (TimeSpan i in cboTimeout.Items)
        {
            if (i != _form.Job!.Timeout)
                continue;

            cboTimeout.SelectedItem = i;
            break;
        }

        if (cboTimeout.SelectedIndex < 0)
        {
            cboTimeout.Items.Add(_form.Job!.Timeout);
            cboTimeout.SelectedIndex = cboTimeout.Items.Count - 1;
        }
    }

    public void PopulateRunningConditions(ComboBox cboRunIf, JobRunConditionEnum runIf)
    {
        var index = -1;
        var selectedIndex = 0;

        foreach (var condition in JobRunConditionEnumHelper.GetAllEnumItems())
        {
            index++;
            cboRunIf.Items.Add(condition.ToFriendlyString());

            if (condition == runIf)
                selectedIndex = index;
        }

        cboRunIf.SelectedIndex = selectedIndex;
    }
}