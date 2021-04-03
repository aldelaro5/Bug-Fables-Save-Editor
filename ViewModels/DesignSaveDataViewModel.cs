using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;

namespace BugFablesSaveEditor.ViewModels
{
  public class DesignSaveDataViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    public string[] Areas
    {
      get
      {
        return CommonUtils.GetEnumDescriptions<Area>();
      }
    }

    public string[] Maps
    {
      get
      {
        return CommonUtils.GetEnumDescriptions<Map>();
      }
    }

    public string[] SaveProgressIcons
    {
      get
      {
        return CommonUtils.GetEnumDescriptions<SaveProgressIcon>();
      }
    }

    public DesignSaveDataViewModel()
    {
      SaveData = new SaveData();
      SaveData.LoadFromFile(@"C:\Users\aldel\Documents\save1.dat");
    }
  }
}
