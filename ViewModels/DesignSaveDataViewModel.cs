using BugFablesSaveEditor.BugFablesSave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using BugFablesSaveEditor.BugFablesEnums;

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
        return Common.GetEnumDescriptions<Area>();
      }
    }

    public string[] Maps
    {
      get
      {
        return Common.GetEnumDescriptions<Map>();
      }
    }

    public string[] SaveProgressIcons
    {
      get
      {
        return Common.GetEnumDescriptions<SaveProgressIcon>();
      }
    }

    public DesignSaveDataViewModel()
    {
      SaveData = new SaveData();
      SaveData.LoadFromFile(@"C:\Users\aldel\Documents\save1.dat");
    }
  }
}
