using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;

namespace BugFablesSaveEditor.ViewModels;

public class DesignSaveDataViewModel : ViewModelBase
{
  private SaveData _saveData;

  public DesignSaveDataViewModel()
  {
    SaveData = new SaveData();
    SaveData.LoadFromFile(@"C:\Users\aldel\Documents\save1.dat");
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

  public string[] Areas => Common.GetEnumDescriptions<Area>();

  public string[] Maps => Common.GetEnumDescriptions<Map>();

  public string[] SaveProgressIcons => Common.GetEnumDescriptions<SaveProgressIcon>();
}
