using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;

namespace BugFablesSaveEditor.ViewModels;

public class GlobalViewModel : ViewModelBase
{
  private string[] _areas;

  private string[] _maps;
  private SaveData _saveData;

  private string[] _saveProgressIcons;

  public GlobalViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Areas = Common.GetEnumDescriptions<Area>();
    Maps = Common.GetEnumDescriptions<Map>();
    SaveProgressIcons = Common.GetEnumDescriptions<SaveProgressIcon>();
  }

  public SaveData SaveData
  {
    get => _saveData;
    set
    {
      _saveData = value;
      this.RaisePropertyChanged();
    }
  }

  public string[] Areas
  {
    get => _areas;
    set
    {
      _areas = value;
      this.RaisePropertyChanged();
    }
  }

  public string[] Maps
  {
    get => _maps;
    set
    {
      _maps = value;
      this.RaisePropertyChanged();
    }
  }

  public string[] SaveProgressIcons
  {
    get => _saveProgressIcons;
    set
    {
      _saveProgressIcons = value;
      this.RaisePropertyChanged();
    }
  }
}
