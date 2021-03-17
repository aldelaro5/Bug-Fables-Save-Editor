using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;

namespace BugFablesSaveEditor.ViewModels
{
  public class QuestsViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    public QuestsViewModel(SaveData saveData)
    {
      SaveData = saveData;
    }
  }
}
