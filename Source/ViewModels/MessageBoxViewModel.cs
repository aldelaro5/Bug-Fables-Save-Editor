using Avalonia.Threading;
using BugFablesSaveEditor.Views;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.ViewModels;
using MessageBox.Avalonia.ViewModels.Commands;
using System;

namespace BugFablesSaveEditor.ViewModels
{
  public class MessageBoxViewModel : AbstractMsBoxViewModel
  {
    private MessageBoxView _parentWindow;
    public string ContentMessage { get; }
    public bool IsOkShowed { get; private set; }
    public bool IsYesShowed { get; private set; }
    public bool IsNoShowed { get; private set; }
    public bool IsAbortShowed { get; private set; }
    public bool IsCancelShowed { get; private set; }
    public RelayCommand ButtonClickCommand { get; }
    public RelayCommand EnterClickCommand { get; }
    public RelayCommand EscClickCommand { get; }

    public MessageBoxViewModel(MessageBoxStandardParams @params, MessageBoxView parent) :
        base(@params, @params.Icon)
    {
      ContentMessage = @params.ContentMessage;
      _parentWindow = parent;
      SetButtons(@params.ButtonDefinitions);
      ButtonClickCommand = new RelayCommand(o => ButtonClick(o.ToString()));
      EnterClickCommand = new RelayCommand(o => EnterClick());
      EscClickCommand = new RelayCommand(o => EscClick());
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

    private async void EscClick()
    {
      await Dispatcher.UIThread.InvokeAsync(() => _parentWindow.Close());

    }

    public async void ButtonClick(string parameter)
    {
      await Dispatcher.UIThread.InvokeAsync(() =>
      {
        _parentWindow.ButtonResult = (ButtonResult)Enum.Parse(typeof(ButtonResult), parameter.Trim(), true);
        _parentWindow.Close();
      });
    }
  }
}
