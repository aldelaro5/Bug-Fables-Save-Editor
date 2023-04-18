using Avalonia.Controls;

namespace BugFablesSaveEditor.Views;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
  }

  // Workaround an Avalonia bug where the app could crash on close when rendering certain tabs
  private void Window_OnClosing(object? sender, WindowClosingEventArgs e) => MainView.DataContext = null;
}
