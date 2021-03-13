using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave.Sections;

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
      Areas = Common.GetEnumDescriptions<Area>();
      Maps = Common.GetEnumDescriptions<Map>();
      SaveProgressIcons = Common.GetEnumDescriptions<SaveProgressIcon>();
    }
  }
}
