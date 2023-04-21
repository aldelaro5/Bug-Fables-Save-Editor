using System.Threading.Tasks;
using BugFablesLib;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor;

public record SaveFileReturn(bool succeeded, string? newFilePath);

public interface IPlatformSpecifics
{
  public Task<SaveFileReturn> SaveFileAsync(BfSaveData saveData);
  public Task<ButtonResult> ShowMessageBoxAsync(MessageBoxStandardParams msgBoxParams);
}
