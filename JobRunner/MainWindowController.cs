using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JobRunner.Dialogs;
using JobRunner.Dialogs.ViewList;
using JobRunner.GuiComponents;
using JobRunner.ObjectModel;
using JobRunner.Utils;

namespace JobRunner
{
    public class MainWindowController
    {
        public void InitializeStatus(Form mainWindow, ToolStripStatusLabel statusBar)
        {
            if (Config.IsAdministrator)
            {
                mainWindow.Text = @"JobRunner (Administrator)";
                statusBar.Text = @"Done.";
            }
            else
            {
                mainWindow.Text = @"JobRunner";
                statusBar.Text = @"Done (read only).";
            }
        }

        public bool TellUserIfProgramHasJobs(IJobList jobs, string text)
        {
            if (jobs.Count > 0)
                return true;
            MessageDisplayer.Tell(@"Job list is empty.", text);
            return false;
        }

        public void SetGuiToRunningState(ToolStripMenuItem runMenuItem, ToolStripMenuItem runSelectedMenuItem, bool runningState)
        {
            runMenuItem.Enabled = !runningState;
            runSelectedMenuItem.Enabled = !runningState;
        }

        public void RunJobsCompleted(Form mainWindow, bool cleanExit, ToolStripStatusLabel statusBar, IJobList jobs, Grid grid)
        {
            statusBar.Text = cleanExit
                ? @"Done."
                : $@"Break. Completed: {jobs.Completed}, error: {jobs.Error}, no action: {jobs.Pending}";

            grid.Running = false;
            grid.Invalidate();

            if (Config.AutoClose && jobs.RunSuccess && AutoActionDialog.CheckAutoClose(mainWindow))
                mainWindow.Close();
        }

        public void MoveJobUp(IJobList jobs, Grid grid)
        {
            if (!Config.IsAdministrator)
                return;

            var job = grid.SelectedJob;
            
            if (job == null)
            {
                MessageDisplayer.Tell("No job is selected.", "Move job up");
                return;
            }
            
            if (jobs.FirstJob == job)
            {
                MessageDisplayer.Tell("Selected job is already the first job.", "Move job up");
                return;
            }
            
            grid.MoveUp(grid.SelectedRow);
            jobs.MoveUp(job);
            grid.Refresh();
        }

        public void MoveJobDown(IJobList jobs, Grid grid)
        {
            if (!Config.IsAdministrator)
                return;
            
            var job = grid.SelectedJob;
            
            if (job == null)
            {
                MessageDisplayer.Tell("No job is selected.", "Move job down");
                return;
            }
            
            if (jobs.LastJob == job)
            {
                MessageDisplayer.Tell("Selected job is already the last job.", "Move job down");
                return;
            }
            
            grid.MoveDown(grid.SelectedRow);
            jobs.MoveDown(job);
            grid.Refresh();
        }

        public void ShowVariables(MainWindow parent, IVariableList variables, IJobList jobList)
        {
            var listDescriptor = new SimpleListDescriptor
            {
                WindowTitle = "Variables",
                PrimaryColumnTitle = "Variable",
                SecondaryColumnTitle = "Usage (job name)"
            };

            listDescriptor.AddRange(
                from variable
                in variables.All
                let jobs = jobList.GetVariableUsage(variable)
                select new SimpleListItem(
                    $"\"{variable.Name}\"=\"{variable.Value}\"",
                    jobs.Names,
                    $"[{variable.Name}]"
                )
            );

            using var x = new SimpleListDialog(parent, variables, jobList);

            x.ShowListDialog(
                parent,
                listDescriptor,
                Config.IsAdministrator ? (AddVariableDelegate)AddVariable : null
            );
        }

        private void AddVariable(MainWindow parent, IVariableList variables, IJobList jobList, SimpleListDescriptor descriptor)
        {
            using var x = new AddVariableDialogSmall
            {
                Variables = variables
            };
            x.ShowDialog(parent);
            descriptor.Clear();
            descriptor.AddRange(
                from variable
                    in variables.All
                let jobs = jobList.GetVariableUsage(variable)
                select new SimpleListItem(
                    $"\"{variable.Name}\"=\"{variable.Value}\"",
                    jobs.Names,
                    $"[{variable.Name}]"
                )
            );
            SaveVariables(parent, variables);
        }

        public void SaveVariables(MainWindow parent, IVariableList variables)
        {
            parent.Cursor = Cursors.WaitCursor;
            try
            {
                using var sw = new StreamWriter(Config.GetVariableFilePath(), false, Encoding.UTF8);
                sw.Write(variables.GetXml());
                sw.Flush();
                sw.Close();
                parent.Cursor = Cursors.Default;
            }
            catch
            {
                parent.Cursor = Cursors.Default;
                MessageDisplayer.Yell($@"Failed to save the file ""{Config.GetVariableFilePath()}"".", parent.Text);
            }
        }
    }
}