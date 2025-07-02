namespace JobRunner.GuiComponents;

public interface IGridVisualFeedback
{
    bool CursorBlink { get; set; }
    void Invalidate();
}