using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class GlobalViewModel : ObservableObject
{
  [ObservableProperty]
  private string[] _areas = null!;

  [ObservableProperty]
  private string[] _maps = null!;
  
  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  private string[] _saveProgressIcons = null!;

  public GlobalViewModel() : this(new SaveData())
  {
  }

  public GlobalViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Areas = Common.GetEnumDescriptions<Area>();
    Maps = Common.GetEnumDescriptions<Map>();
    SaveProgressIcons = Common.GetEnumDescriptions<SaveProgressIcon>();
  }
}
