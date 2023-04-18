using Avalonia.Controls;
using Avalonia.Interactivity;

namespace BugFablesSaveEditor.Views;

public partial class MainView : UserControl
{
  public MainView()
  {
    InitializeComponent();
  }

  public async void OnAbout_Click(object sender, RoutedEventArgs e)
  {
    AboutView view = new();
    await view.ShowDialog((MainWindow)Utils.TopLevel);
  }
}
