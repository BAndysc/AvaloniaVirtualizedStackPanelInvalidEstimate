using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace AvaloniaVirtualizedStackPanelInvalidEstimate;

public partial class MainWindow : Window
{
    private HashSet<Control> attached = new();

    public MainWindow()
    {
        DataContext = new MainWindowViewModel();
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        //this.GetControl<ListBox>("ListBox").ApplyTemplate();
        // this.GetControl<ListBox>("ListBox").FindDescendantOfType<ScrollViewer>()
        //     .GetObservable(ScrollViewer.OffsetProperty).Subscribe(
        //         off =>
        //         {
        //             Console.WriteLine(off);
        //         });
        this.GetControl<ListBox>("ListBox").ContainerPrepared += (_, e) =>
        {
            if (!attached.Add(e.Container))
                return;

            e.Container.AddHandler(RequestBringIntoViewEvent, (s, e) =>
            {
                var text = e.TargetObject.FindDescendantOfType<TextBlock>();
                var m = text.TransformToVisual(e.TargetObject);
                if (m.HasValue)
                {
                    var bounds = new Rect(text.Bounds.Size);
                    var rect = bounds.TransformToAABB(m.Value);
                    e.TargetRect = rect;
                }
            });
        };
    }
}