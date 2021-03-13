using BugFablesSaveEditor.BugFablesSave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor
{
  public class SharedData : INotifyPropertyChanged
  {
    private SaveData _saveData = new SaveData();
    public SaveData saveData
    {
      get { return _saveData; }
      set { _saveData = value; NotifyPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
