using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BugFablesSaveEditor.Views
{
  public class SongsView : UserControl
  {
    public SongsView()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
