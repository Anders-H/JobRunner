using System;

namespace JobRunner.GuiComponents;

public delegate void ContextMenuEventHandler(object sender, ContextMenuEventArgs e);
    
public class ContextMenuEventArgs : EventArgs
{
    public int X { get; }
    public int Y { get; }

    public ContextMenuEventArgs(int x, int y)
    {
        X = x;
        Y = y;
    }
}