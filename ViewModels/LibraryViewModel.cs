using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;

namespace BugFablesSaveEditor.ViewModels
{
  public class LibraryViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    public LibraryViewModel(SaveData saveData)
    {
      SaveData = saveData;
    }
  }
}
