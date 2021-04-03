using Avalonia;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using BugFablesSaveEditor.Views;
using Common.MessageBox;
using Common.MessageBox.Enums;
using DynamicData.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.ViewModels
{
  public class MessageBoxViewModel : ViewModelBase, INotifyPropertyChanged
  {
    private MessageBoxView _parentWindow;
    public bool HasIcon => !(ImagePath is null);
    public FontFamily FontFamily { get; }
    public string ContentTitle { get; }
    public string ContentMessage { get; }
    public WindowIcon WindowIconPath { get; } = null;
    public Bitmap ImagePath { get; } = null;
    public int? MaxWidth { get; }
    public WindowStartupLocation LocationOfMyWindow { get; }
    public bool IsOkShowed { get; private set; }
    public bool IsYesShowed { get; private set; }
    public bool IsNoShowed { get; private set; }
    public bool IsAbortShowed { get; private set; }
    public bool IsCancelShowed { get; private set; }

    public MessageBoxViewModel(MessageBoxParams @params, MessageBoxView parent)
    {
      if (@params.Icon != Icon.None)
      {
        ImagePath = new Bitmap(AvaloniaLocator.Current.GetService<IAssetLoader>()
            .Open(new Uri(
                $" avares://BugFablesSaveEditor/Assets/MessageBox/{@params.Icon.ToString().ToLowerInvariant()}.png")));
      }

      MaxWidth = @params.MaxWidth;
      FontFamily = @params.FontFamily;
      ContentTitle = @params.ContentTitle;
      ContentMessage = @params.ContentMessage;
      WindowIconPath = @params.WindowIcon;
      LocationOfMyWindow = @params.WindowStartupLocation;

      ContentMessage = @params.ContentMessage;
      _parentWindow = parent;
      SetButtons(@params.ButtonDefinitions);
    }

    private void SetButtons(ButtonEnum paramsButtonDefinitions)
    {
      switch (paramsButtonDefinitions)
      {
        case ButtonEnum.Ok:
          IsOkShowed = true;
          break;
        case ButtonEnum.YesNo:
          IsYesShowed = true;
          IsNoShowed = true;
          break;
        case ButtonEnum.OkCancel:
          IsOkShowed = true;
          IsCancelShowed = true;
          break;
        case ButtonEnum.OkAbort:
          IsOkShowed = true;
          IsAbortShowed = true;
          break;
        case ButtonEnum.YesNoCancel:
          IsYesShowed = true;
          IsNoShowed = true;
          IsCancelShowed = true;
          break;
        case ButtonEnum.YesNoAbort:
          IsYesShowed = true;
          IsNoShowed = true;
          IsAbortShowed = true;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(paramsButtonDefinitions), paramsButtonDefinitions,
              null);
      }
    }

    public async Task Copy()
    {
      await AvaloniaLocator.Current.GetService<IClipboard>().SetTextAsync(ContentMessage);
    }

    private void EnterClick()
    {
      if (IsOkShowed)
      {
        ButtonClick("OK");
      }

      if (IsYesShowed)
      {
        ButtonClick("YES");
      }
    }

    public async void ButtonClick(string parameter)
    {
      await Dispatcher.UIThread.InvokeAsync(() =>
      {
        _parentWindow.ButtonResult = (ButtonResult)Enum.Parse(typeof(ButtonResult), parameter.Trim(), true);
        _parentWindow.Close();
      });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
