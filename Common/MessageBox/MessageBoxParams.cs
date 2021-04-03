using Avalonia.Controls;
using Avalonia.Media;
using Common.MessageBox.Enums;

namespace Common.MessageBox
{
  public class MessageBoxParams
  {
    public WindowIcon WindowIcon { get; set; } = null;
    public bool CanResize { get; set; } = false;
    public bool ShowInCenter { get; set; } = true;
    public FontFamily FontFamily { get; set; } = FontFamily.Default;
    public string ContentTitle { get; set; } = string.Empty;
    public string ContentHeader { get; set; } = null;
    public string ContentMessage { get; set; } = string.Empty;
    public int? MaxWidth { get; set; } = null;
    public WindowStartupLocation WindowStartupLocation { get; set; } = WindowStartupLocation.Manual;
    public Icon Icon { get; set; } = Icon.None;
    public ButtonEnum ButtonDefinitions { get; set; } = ButtonEnum.Ok;
  }
}
