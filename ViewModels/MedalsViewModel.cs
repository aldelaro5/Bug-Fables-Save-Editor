using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.ViewModels
{
  public class MedalsViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    public MedalsViewModel(SaveData saveData)
    {
      SaveData = saveData;
    }
  }
}
