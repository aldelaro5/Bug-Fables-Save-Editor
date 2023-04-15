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
    AboutView view = new() { Width = 500, Height = 400 };
    await view.ShowDialog(Utils.MainWindow);
  }
}
