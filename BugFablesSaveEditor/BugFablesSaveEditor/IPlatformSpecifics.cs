using System.Threading.Tasks;
using BugFablesLib;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor;

public record SaveFileReturn(bool Succeeded, string? NewFilePath);

public interface IPlatformSpecifics
{
  public Task<SaveFileReturn> SaveFileAsync(BfSaveData saveData, string fileName);
  public Task<ButtonResult> ShowMessageBoxAsync(MessageBoxStandardParams msgBoxParams);
}
