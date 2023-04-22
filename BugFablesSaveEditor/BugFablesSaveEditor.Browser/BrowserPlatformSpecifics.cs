using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using BugFablesLib;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor.Browser;

public partial class BrowserPlatformSpecifics : IPlatformSpecifics
{
  public Task<SaveFileReturn> SaveFileAsync(BfSaveData saveData)
  {
    throw new System.NotImplementedException();
  }

  public async Task<ButtonResult> ShowMessageBoxAsync(MessageBoxStandardParams msgBoxParams)
  {
    string iconStr = msgBoxParams.Icon switch
    {
      Icon.Question => "question",
      Icon.Success => "success",
      Icon.Error => "error",
      Icon.Info => "info",
      Icon.Warning => "warning",
      _ => throw new ArgumentException($"{msgBoxParams.Icon} is not a supported icon", nameof(msgBoxParams.Icon))
    };
    string buttonsTypeStr = msgBoxParams.ButtonDefinitions switch
    {
      ButtonEnum.Ok => "ok",
      ButtonEnum.YesNo => "yesNo",
      _ => throw new ArgumentException($"{msgBoxParams.ButtonDefinitions} is not a supported button definitions",
        nameof(msgBoxParams.ButtonDefinitions))
    };

    bool hasConfirmed =
      await ShowMessageBoxAsync(msgBoxParams.ContentTitle, msgBoxParams.ContentMessage, iconStr, buttonsTypeStr);

    return !hasConfirmed ? ButtonResult.No :
      msgBoxParams.ButtonDefinitions == ButtonEnum.Ok ? ButtonResult.Ok : ButtonResult.Yes;
  }

  [JSImport("ShowMessageBoxAsync", "main.js")]
  private static partial Task<bool> ShowMessageBoxAsync(string title, string message, string icon, string buttonsType);
}
