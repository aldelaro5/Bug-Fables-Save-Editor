using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class GlobalViewModel : ObservableObject
{
  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private string[] _areas = Utils.GetEnumDescriptions<Area>();

  [ObservableProperty]
  private string[] _maps = Utils.GetEnumDescriptions<Map>();

  [ObservableProperty]
  private string[] _saveProgressIcons = Utils.GetEnumDescriptions<SaveProgressIcon>();

  public GlobalViewModel() : this(new SaveData())
  {
  }

  public GlobalViewModel(SaveData saveData)
  {
    _saveData = saveData;
  }
}
