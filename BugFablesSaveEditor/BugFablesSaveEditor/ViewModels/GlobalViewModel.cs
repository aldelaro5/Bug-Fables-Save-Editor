using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class GlobalViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableGlobalSaveData _globalSaveData;

  [ObservableProperty]
  private ObservableHeaderSaveData _headerSaveData;

  public GlobalViewModel() : this(new(), new()) { }

  public GlobalViewModel(GlobalSaveData globalSaveData,
                         HeaderSaveData headerSaveData)
  {
    _globalSaveData = new(globalSaveData);
    _headerSaveData = new(headerSaveData);
  }
}
