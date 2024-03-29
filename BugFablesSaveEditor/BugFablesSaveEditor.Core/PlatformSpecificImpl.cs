using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using BugFablesLib;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace BugFablesSaveEditor.Core;

internal class PlatformSpecificImpl : IPlatformSpecifics
{
  public async Task<SaveFileReturn> SaveFileAsync(BfSaveData saveData, IBfSaveFileFormat saveFileFormat, string _)
  {
    FilePickerSaveOptions pickerSaveOptions = new()
    {
      Title = "Select the location to save the file",
      ShowOverwritePrompt = true
    };

    if (saveFileFormat is not BfXboxPcSaveDataFormat)
      pickerSaveOptions.FileTypeChoices = new[] { Utils.SaveFileFilter };

    var file = await Utils.TopLevel.StorageProvider.SaveFilePickerAsync(pickerSaveOptions);
    if (file is null)
      return new(false, null);

    Stream? fileStream = null;
    try
    {
      fileStream = await file.OpenWriteAsync();
      byte[] data = await saveData.EncodeToBytes(saveFileFormat);
      await fileStream.WriteAsync(data);
      string filePath = file.Name;
      fileStream.Close();
      await ShowMessageBoxAsync(new()
      {
        ContentTitle = "File saved",
        ContentMessage =
          $"The file was saved successfully at {filePath} " +
          $"\n{(saveFileFormat is BfXboxPcSaveDataFormat ? Utils.MessageXboxSave : "")}",
        Icon = Icon.Warning,
        ButtonDefinitions = ButtonEnum.Ok
      });
      return new(true, filePath);
    }
    catch (Exception ex)
    {
      await ShowMessageBoxAsync(new()
      {
        ContentTitle = "Error opening save file",
        ContentMessage = $"An error occured while saving the save file: {ex.Message}",
        Icon = Icon.Error,
        ButtonDefinitions = ButtonEnum.Ok
      });
      return new(false, null);
    }
    finally
    {
      fileStream?.Close();
    }
  }

  public async Task<ButtonResult> ShowMessageBoxAsync(MessageBoxStandardParams msgBoxParams)
  {
    return await MessageBoxManager.GetMessageBoxStandard(msgBoxParams).ShowWindowDialogAsync((Window)Utils.TopLevel);
  }
}
