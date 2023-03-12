using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
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
    Areas = Utils.GetEnumDescriptions<Area>();
    Maps = Utils.GetEnumDescriptions<Map>();
    SaveProgressIcons = Utils.GetEnumDescriptions<SaveProgressIcon>();
  }
}
