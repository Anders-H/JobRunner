#nullable enable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JobRunner.ObjectModel;

namespace JobRunner.GuiComponents
{
    public class Grid : DataGridView, IGridVisualFeedback
    {
        public event EventHandler? EditJob;
        public event ContextMenuEventHandler? ShowContextMenu;
        public bool CursorBlink { get; set; }
        public bool Running { get; set; }
        public int RunSingle { get; set; }

        public void Initialize(IJobList jobs)
        {
            ConfigureGrid();
            RefreshList(jobs);
        }

        private void ConfigureGrid()
        {
            Rows.Clear();
            Columns.Clear();
            RowHeadersVisible = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeColumns = true;
            AllowUserToResizeRows = false;
            AllowUserToResizeColumns = false;
            SelectionMode = DataGridViewSelectionMode.CellSelect;
            MultiSelect = false;
            BorderStyle = BorderStyle.None;
            BackColor = Color.Black;
            DoubleBuffered = true;

            Columns.Add(new DataGridViewColumn
            {
                HeaderText = @"No",
                Width = 30,
                ReadOnly = true
            });
            
            Columns.Add(new DataGridViewColumn
            {
                HeaderText = @"Name",
                Width = 200,
                ReadOnly = true
            });
            
            Columns.Add(new DataGridViewColumn
            {
                HeaderText = @"Start time",
                Width = 110,
                ReadOnly = true
            });
            
            Columns.Add(new DataGridViewColumn
            {
                HeaderText = @"End time",
                Width = 110,
                ReadOnly = true
            });
            
            Columns.Add(new DataGridViewColumn
            {
                HeaderText = @"Step time",
                Width = 65,
                ReadOnly = true
            });
            
            Columns.Add(new DataGridViewColumn
            {
                HeaderText = @"Total time",
                Width = 65,
                ReadOnly = true
            });
            
            Columns.Add(new DataGridViewColumn
            {
                HeaderText = @"Status",
                Width = 100,
                ReadOnly = true
            });
            
            Columns.Add(new DataGridViewColumn
            {
                HeaderText = @"Result",
                Width = 270,
                ReadOnly = true
            });
        }

        private void RefreshList(IJobList jobs)
        {
            Rows.Clear();
            var rows = new List<DataGridViewRow>();
            var rowIndex = 0;

            foreach (var job in jobs.All)
            {
                var row = new DataGridViewRow
                {
                    Tag = job
                };

                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                row.Cells.Add(new DataGridViewTextBoxCell());
                job.RowIndex = rowIndex;
                rows.Add(row);
                rowIndex++;
            }

            Rows.AddRange(rows.ToArray());
        }

        protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds) =>
            graphics.FillRectangle(Brushes.Black, clipBounds);

        private Brush BackgroundFromStatus(Job job)
        {
            switch (job.Status)
            {
                case JobStatus.Pending:
                    return Brushes.Black;
                case JobStatus.Running:
                    return Brushes.Green;
                case JobStatus.Completed:
                    return Brushes.Black;
                case JobStatus.Failed:
                    return Brushes.Black;
                case JobStatus.Timeout:
                    return Brushes.Black;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Brush ForegroundFromStatus(Job job)
        {
            if (!job.Enabled)
                return Brushes.DimGray;

            switch (job.Status)
            {
                case JobStatus.Pending:
                    return Brushes.Green;
                case JobStatus.Running:
                    return Brushes.White;
                case JobStatus.Completed:
                    return Brushes.LawnGreen;
                case JobStatus.Failed:
                    return Brushes.OrangeRed;
                case JobStatus.Timeout:
                    return Brushes.Yellow;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int CenterY(Graphics g, Rectangle cellBounds, out int width)
        {
            const string text = "JjÖg";
            var textSize = g.MeasureString(text, Font);
            width = (int)textSize.Width;
            return (int)(cellBounds.Height / 2.0 - textSize.Height / 2.0) + 1;
        }

        public Job? SelectedJob
        {
            get => SelectedCells.Count <= 0
                ? null
                : (Job) Rows[SelectedCells[0].RowIndex].Tag;
            set
            {
                if (Rows.Count <= 0)
                    return;

                for (var i = 0; i < Rows.Count; i++)
                {
                    if (Rows[i].Tag != value)
                        continue;

                    Rows[i].Cells[1].Selected = true;
                    return;
                }
            }
        }

    public int SelectedRow =>
            SelectedCells.Count > 0
                ? SelectedCells[0].RowIndex
                : -1;

        public void RemoveJob(int index) =>
            Rows.RemoveAt(index);

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < RowCount)
                EditJob?.Invoke(this, EventArgs.Empty);

            base.OnCellDoubleClick(e);
        }

        public void MoveUp(int index)
        {
            var x = Rows[index - 1].Tag;
            Rows[index - 1].Tag = Rows[index].Tag;
            Rows[index].Tag = x;
            CurrentCell = Rows[index - 1].Cells[CurrentCell.ColumnIndex];
        }

        public void MoveDown(int index)
        {
            var x = Rows[index + 1].Tag;
            Rows[index + 1].Tag = Rows[index].Tag;
            Rows[index].Tag = x;
            CurrentCell = Rows[index + 1].Cells[CurrentCell.ColumnIndex];
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = HitTest(e.X, e.Y);

                if (hit is { Type: DataGridViewHitTestType.Cell })
                {
                    ClearSelection();
                    Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                    base.OnMouseClick(e);
                    ShowContextMenu?.Invoke(this, new ContextMenuEventArgs(e.X, e.Y));
                    return;
                }
            }

            base.OnMouseClick(e);
        }
        
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            var centerY = CenterY(e.Graphics, e.CellBounds, out var width);

            if (e.RowIndex < 0)
            {
                var t = e.Value.ToString();

                if (Running)
                {
                    e.Graphics.FillRectangle(CursorBlink ? Brushes.DarkBlue : Brushes.DarkRed, e.CellBounds);
                    var brush = CursorBlink ? Brushes.Cyan : Brushes.Yellow;
                    e.Graphics.DrawString(t, Font, brush, e.CellBounds.X + 3, e.CellBounds.Y + centerY);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);
                    e.Graphics.DrawString(t, Font, Brushes.White, e.CellBounds.X + 3, e.CellBounds.Y + centerY);
                }
            }
            else
            {
                var job = (Job)Rows[e.RowIndex].Tag;

                if (job.Status == JobStatus.Running)
                    e.Graphics.FillRectangle(CursorBlink ? BackgroundFromStatus(job) : Brushes.Black, e.CellBounds);
                else
                    e.Graphics.FillRectangle(BackgroundFromStatus(job), e.CellBounds);
                
                switch (e.ColumnIndex)
                {
                    case 0:
                        {
                            var x = e.CellBounds.X + e.CellBounds.Width - 2 - width;
                            var y = e.CellBounds.Y + centerY;
                            e.Graphics.DrawString(job.Number.ToString(), Font, ForegroundFromStatus(job), x, y);
                            break;
                        }
                    case 1:
                        {
                            var y = e.CellBounds.Y + centerY;
                            e.Graphics.DrawString(job.Name, Font, ForegroundFromStatus(job), e.CellBounds.X + 3, y);
                            break;
                        }
                    case 2:
                        if (job.StartTime.HasValue)
                        {
                            var d = job.StartTime.Value.ToShortDateString();
                            var text = $"{d} {job.StartTime.Value.ToLongTimeString()}";
                            var y = e.CellBounds.Y + centerY;
                            e.Graphics.DrawString(text, Font, ForegroundFromStatus(job), e.CellBounds.X + 3, y);
                        }
                        break;
                    case 3:
                        if (job.EndTime.HasValue)
                        {
                            var d = job.EndTime.Value.ToShortDateString();
                            var text = $"{d} {job.EndTime.Value.ToLongTimeString()}";
                            var x = e.CellBounds.X + 3;
                            var y = e.CellBounds.Y + centerY;
                            e.Graphics.DrawString(text, Font, ForegroundFromStatus(job), x, y);
                        }
                        else if (job.StartTime.HasValue)
                        {
                            var span = DateTime.Now.Subtract(job.StartTime.Value);
                            var h = Math.Abs(span.Hours);
                            var m = Math.Abs(span.Minutes);
                            var s = Math.Abs(span.Seconds);
                            var x = $"{h:00}:{m:00}:{s:00}";
                            var brush = CursorBlink ? Brushes.Black : Brushes.White;
                            e.Graphics.DrawString(x, Font, brush, e.CellBounds.X + 3, e.CellBounds.Y + centerY);
                        }
                        break;
                    case 4:
                        if (job.StartTime.HasValue && job.EndTime.HasValue)
                        {
                            var span = job.EndTime.Value.Subtract(job.StartTime.Value);
                            var h = Math.Abs(span.Hours);
                            var m = Math.Abs(span.Minutes);
                            var s = Math.Abs(span.Seconds);
                            var x = $"{h:00}:{m:00}:{s:00}";
                            var y = e.CellBounds.Y + centerY;
                            e.Graphics.DrawString(x, Font, ForegroundFromStatus(job), e.CellBounds.X + 3, y);
                        }
                        break;
                    case 5:
                        if (job.AllJobsStartTime.HasValue && job.EndTime.HasValue)
                        {
                            var span = job.EndTime.Value.Subtract(job.AllJobsStartTime.Value);
                            var h = Math.Abs(span.Hours);
                            var m = Math.Abs(span.Minutes);
                            var s = Math.Abs(span.Seconds);
                            var x = $"{h:00}:{m:00}:{s:00}";
                            var y = e.CellBounds.Y + centerY;
                            e.Graphics.DrawString(x, Font, ForegroundFromStatus(job), e.CellBounds.X + 3, y);
                        }
                        break;
                    case 6:
                        {
                            var x = e.CellBounds.X + 3;
                            var y = e.CellBounds.Y + centerY;
                            e.Graphics.DrawString(job.Enabled ? JobStatusHelper.GetStatusText(job.Status, job.CurrentRetry) : "Disabled", Font, ForegroundFromStatus(job), x, y);
                        }
                        break;
                    case 7:
                        {
                            var text = $"{(job.ExitCode == 0 ? "" : $"{job.ExitCode}: ")}{job.FailMessage}";
                            var x = e.CellBounds.X + 3;
                            var y = e.CellBounds.Y + centerY;
                            e.Graphics.DrawString(text, Font, ForegroundFromStatus(job), x, y);
                        }
                        break;
                }
            }

            e.Graphics.DrawRectangle(Pens.Green, e.CellBounds);
            
            if (SelectedCells.Count > 0)
            {
                var c = SelectedCells[0];
                if (c.RowIndex == e.RowIndex && c.ColumnIndex == e.ColumnIndex)
                {
                    var w = e.CellBounds.Width - 4;
                    var h = e.CellBounds.Height - 4;
                    e.Graphics.DrawRectangle(Pens.LawnGreen, e.CellBounds.X + 2, e.CellBounds.Y + 2, w, h);
                }
            }

            e.Handled = true;
        }
    }
}