using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BugFablesSaveEditor.Views
{
  public class MedalsView : UserControl
  {
    public MedalsView()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
