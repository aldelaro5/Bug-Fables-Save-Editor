using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class GlobalViewModel : ViewModelBase
{
  [ObservableProperty]
  private string[] _areas;

  [ObservableProperty]
  private string[] _maps;
  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private string[] _saveProgressIcons;

  public GlobalViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Areas = Common.GetEnumDescriptions<Area>();
    Maps = Common.GetEnumDescriptions<Map>();
    SaveProgressIcons = Common.GetEnumDescriptions<SaveProgressIcon>();
  }
}
