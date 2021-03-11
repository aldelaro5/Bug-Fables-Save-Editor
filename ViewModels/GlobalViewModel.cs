using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.ViewModels
{
  public class GlobalViewModel : ViewModelBase
  {
    public SaveData SaveData
    {
      get
      {
        return Common.saveData;
      }
      set
      {
        Common.saveData = value;
        this.RaisePropertyChanged();
      }
    }
  }
}
