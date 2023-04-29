using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using BugFablesLib;
using BugFablesSaveEditor.Core;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using Utils = BugFablesSaveEditor.Core.Utils;

namespace BugFablesSaveEditor.Browser;

public partial class BrowserPlatformSpecifics : IPlatformSpecifics
{
  public async Task<SaveFileReturn> SaveFileAsync(BfSaveData saveData, IBfSaveFileFormat saveFileFormat,
                                                  string fileName)
  {
    try
    {
      string dataStr = Convert.ToBase64String(await saveData.EncodeToBytes(saveFileFormat));
      await ShowMessageBoxAsync(new()
      {
        ContentTitle = "File saved",
        ContentMessage = "The save file was encoded successfully. Click Ok to download it. " +
                         $"{(saveFileFormat is BfXboxPcSaveDataFormat ? Utils.MessageXboxSave : "")}",
        Icon = Icon.Success,
        ButtonDefinitions = ButtonEnum.Ok
      });
      await Task.Run(() => DownloadSaveFileAsync(dataStr, fileName));
      return new(true, fileName);
    }
    catch (Exception ex)
    {
      await ShowMessageBoxAsync(new()
      {
        ContentTitle = "Error saving save file",
        ContentMessage = $"An error occured while saving the save file: {ex.Message}",
        Icon = Icon.Error,
        ButtonDefinitions = ButtonEnum.Ok
      });
      return new(false, null);
    }
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

  [JSImport(nameof(DownloadSaveFileAsync), "main.js")]
  private static partial void DownloadSaveFileAsync(string base64Date, string fileName);

  [JSImport(nameof(ShowMessageBoxAsync), "main.js")]
  private static partial Task<bool> ShowMessageBoxAsync(string title, string message, string icon, string buttonsType);
}
