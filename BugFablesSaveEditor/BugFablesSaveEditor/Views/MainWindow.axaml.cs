using Avalonia.Controls;

namespace BugFablesSaveEditor.Views;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
  }

  // Workaround for https://github.com/AvaloniaUI/Avalonia/issues/11149
  // TODO: Remove this hack when we upgrade to any version past preview7 as the fix got merged
  private void Window_OnClosing(object? sender, WindowClosingEventArgs e) => MainView.DataContext = null;
}
