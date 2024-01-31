namespace AvaloniaVirtualizedStackPanelInvalidEstimate;

public class ViewModelItem
{
    public ViewModelItem(string text, double indent)
    {
        Text = text;
        Indent = indent;
    }

    public string Text { get; }
    public double Indent { get; }
}