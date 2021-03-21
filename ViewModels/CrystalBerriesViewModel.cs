using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using static BugFablesSaveEditor.BugFablesSave.Sections.CrystalBerries;

namespace BugFablesSaveEditor.ViewModels
{
  public class CrystalBerriesViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private CrystalBerry[] _crystalBerries;
    public CrystalBerry[] CrystalBerries
    {
      get { return _crystalBerries; }
      set { _crystalBerries = value; this.RaisePropertyChanged(); }
    }

    public CrystalBerriesViewModel()
    {
      SaveData = new SaveData();
      Initialise();
    }

    public CrystalBerriesViewModel(SaveData saveData)
    {
      SaveData = saveData;
      Initialise();
    }

    private void Initialise()
    {
      CrystalBerries = (CrystalBerry[])SaveData.Sections[SaveFileSection.CrystalBerries].Data;
    }
  }
}
