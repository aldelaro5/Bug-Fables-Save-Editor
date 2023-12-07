using System.Threading.Tasks;
using BugFablesLib;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace BugFablesSaveEditor.Core;

public record SaveFileReturn(bool Succeeded, string? NewFilePath);

public interface IPlatformSpecifics
{
  public Task<SaveFileReturn> SaveFileAsync(BfSaveData saveData, IBfSaveFileFormat saveFileFormat, string fileName);
  public Task<ButtonResult> ShowMessageBoxAsync(MessageBoxStandardParams msgBoxParams);
}
