using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AvaloniaVirtualizedStackPanelInvalidEstimate;

public class MainWindowViewModel : INotifyPropertyChanged, ICommand
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public ViewModelItem? selectedRow;
    public ViewModelItem? SelectedRow
    {
        get => selectedRow;
        set => SetField(ref selectedRow, value);
    }
    public ObservableCollection<ViewModelItem> Items { get; } = new();

    public ICommand SelectRowCommand { get; }

    public MainWindowViewModel()
    {
        SelectRowCommand = this;

        int index = 0;
        for (int j = 0; j < 100; ++j)
        {
            Items.Add(new ViewModelItem($"Item {index++}", 100 + j * 50));
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        if (parameter?.ToString() is { } s &&
            int.TryParse(s, out var index))
            SelectedRow = Items[index];
    }

    public event EventHandler? CanExecuteChanged;
}