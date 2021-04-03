using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;

namespace BugFablesSaveEditor.ViewModels
{
  public class GlobalViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private string[] _areas;
    public string[] Areas
    {
      get { return _areas; }
      set { _areas = value; this.RaisePropertyChanged(); }
    }

    private string[] _maps;
    public string[] Maps
    {
      get { return _maps; }
      set { _maps = value; this.RaisePropertyChanged(); }
    }

    private string[] _saveProgressIcons;
    public string[] SaveProgressIcons
    {
      get { return _saveProgressIcons; }
      set { _saveProgressIcons = value; this.RaisePropertyChanged(); }
    }

    public GlobalViewModel(SaveData saveData)
    {
      SaveData = saveData;
      Areas = CommonUtils.GetEnumDescriptions<Area>();
      Maps = CommonUtils.GetEnumDescriptions<Map>();
      SaveProgressIcons = CommonUtils.GetEnumDescriptions<SaveProgressIcon>();
    }
  }
}
