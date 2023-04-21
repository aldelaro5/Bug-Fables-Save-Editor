using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using BugFablesLib;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor;

internal class PlatformSpecificImpl : IPlatformSpecifics
{
  public async Task<string?> SaveFileAsync(BfSaveData saveData)
  {
    FilePickerSaveOptions pickerSaveOptions = new()
    {
      Title = "Select the location to save the file",
      ShowOverwritePrompt = true,
      FileTypeChoices = new[] { Utils.SaveFileFilter }
    };

    var file = await Utils.TopLevel.StorageProvider.SaveFilePickerAsync(pickerSaveOptions);
    if (file is null)
      return null;

    Stream? fileStream = null;
    try
    {
      fileStream = await file.OpenWriteAsync();
      string data = saveData.EncodeToString();
      await fileStream.WriteAsync(Encoding.UTF8.GetBytes(data));
      string filePath = file.Name;
      fileStream.Close();
      await ShowMessageBoxAsync(new()
      {
        ContentTitle = "File saved",
        ContentMessage = $"The file was saved successfully at {filePath}",
        Icon = Icon.Warning,
        ButtonDefinitions = ButtonEnum.Ok
      });
      return filePath;
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
      return null;
    }
    finally
    {
      fileStream?.Close();
    }
  }

  public async Task<ButtonResult> ShowMessageBoxAsync(MessageBoxStandardParams msgBoxParams)
  {
    return await MessageBoxManager.GetMessageBoxStandardWindow(msgBoxParams).ShowDialog((Window)Utils.TopLevel);
  }
}
