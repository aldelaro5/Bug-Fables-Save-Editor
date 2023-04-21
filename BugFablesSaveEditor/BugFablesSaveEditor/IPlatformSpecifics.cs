using System.Threading.Tasks;
using BugFablesLib;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor;

public interface IPlatformSpecifics
{
  public Task<string?> SaveFileAsync(BfSaveData saveData);
  public Task<ButtonResult> ShowMessageBoxAsync(MessageBoxStandardParams msgBoxParams);
}
