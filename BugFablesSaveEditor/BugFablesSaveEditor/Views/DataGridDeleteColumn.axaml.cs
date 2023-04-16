using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BugFablesSaveEditor.Views;

public class DataGridDeleteColumn : DataGridTemplateColumn
{
  private ICommand _deleteCommand = null!;

  public static readonly DirectProperty<DataGridDeleteColumn, ICommand> DeleteCommandProperty =
    AvaloniaProperty.RegisterDirect<DataGridDeleteColumn, ICommand>(
      nameof(DeleteCommand),
      o => o.DeleteCommand,
      (o, v) => o.DeleteCommand = v);

  public ICommand DeleteCommand
  {
    get => _deleteCommand;
    set => SetAndRaise(DeleteCommandProperty, ref _deleteCommand, value);
  }

  public DataGridDeleteColumn()
  {
    InitializeComponent();
  }

  private void InitializeComponent()
  {
    AvaloniaXamlLoader.Load(this);
  }
}

