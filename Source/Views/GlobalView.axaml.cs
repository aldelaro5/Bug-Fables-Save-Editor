using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BugFablesSaveEditor.Views
{
  public class GlobalView : UserControl
  {
    public GlobalView()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
