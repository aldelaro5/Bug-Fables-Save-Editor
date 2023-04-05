using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Styling;

namespace BugFablesSaveEditor.Controls;

public partial class BfNamedIdDataGrid : DataGrid, IStyleable
{
  public static readonly DirectProperty<BfNamedIdDataGrid, ICommand> DeleteCommandProperty =
    AvaloniaProperty.RegisterDirect<BfNamedIdDataGrid, ICommand>(
      nameof(DeleteCommand),
      o => o.DeleteCommand,
      (o, v) => o.DeleteCommand = v,
      defaultBindingMode: BindingMode.OneWay);

  private ICommand _deleteCommand = null!;

  public ICommand DeleteCommand
  {
    get => _deleteCommand;
    set => SetAndRaise(DeleteCommandProperty, ref _deleteCommand, value);
  }

  Type IStyleable.StyleKey => typeof(DataGrid);

  public BfNamedIdDataGrid()
  {
    InitializeComponent();
  }
}
