using Avalonia.Controls;
using Avalonia.Interactivity;
using DialogHostAvalonia;

namespace BugFablesSaveEditor.Core.Views;

public partial class MainView : UserControl
{
  public MainView()
  {
    InitializeComponent();
  }

  public async void OnAbout_Click(object sender, RoutedEventArgs e)
  {
    await DialogHost.Show(new AboutView(), Utils.DialogSessionName);
  }
}
