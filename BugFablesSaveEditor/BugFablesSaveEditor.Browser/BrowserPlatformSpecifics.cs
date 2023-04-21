using System.Threading.Tasks;
using BugFablesLib;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor.Browser;

public class BrowserPlatformSpecifics : IPlatformSpecifics
{
  public Task<SaveFileReturn> SaveFileAsync(BfSaveData saveData)
  {
    throw new System.NotImplementedException();
  }

  public Task<ButtonResult> ShowMessageBoxAsync(MessageBoxStandardParams msgBoxParams)
  {
    throw new System.NotImplementedException();
  }
}
