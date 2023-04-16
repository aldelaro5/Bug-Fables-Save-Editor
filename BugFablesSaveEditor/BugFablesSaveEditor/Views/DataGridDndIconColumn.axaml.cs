using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BugFablesSaveEditor.Views;

public class DataGridDndIconColumn : DataGridTemplateColumn
{
  public DataGridDndIconColumn()
  {
    InitializeComponent();
  }

  private void InitializeComponent()
  {
    AvaloniaXamlLoader.Load(this);
  }
}

