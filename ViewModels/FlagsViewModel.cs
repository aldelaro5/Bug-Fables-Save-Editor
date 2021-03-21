using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using ReactiveUI;
using System.ComponentModel;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flags;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagstrings;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagvars;
using static BugFablesSaveEditor.BugFablesSave.Sections.Global;
using static BugFablesSaveEditor.BugFablesSave.Sections.RegionalFlags;

namespace BugFablesSaveEditor.ViewModels
{
  public class FlagsViewModel : ViewModelBase
  {
    private GlobalInfo globalInfo;
    private RegionalFlags regionalFlags;

    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private FlagInfo[] _flags;
    public FlagInfo[] Flags
    {
      get { return _flags; }
      set { _flags = value; this.RaisePropertyChanged(); }
    }

    private FlagvarInfo[] _flagvars;
    public FlagvarInfo[] Flagvars
    {
      get { return _flagvars; }
      set { _flagvars = value; this.RaisePropertyChanged(); }
    }

    private FlagstringInfo[] _flagstrings;
    public FlagstringInfo[] Flagstrings
    {
      get { return _flagstrings; }
      set { _flagstrings = value; this.RaisePropertyChanged(); }
    }

    private RegionalInfo[] _regionals;
    public RegionalInfo[] Regionals
    {
      get { return _regionals; }
      set { _regionals = value; this.RaisePropertyChanged(); }
    }

    public FlagsViewModel()
    {
      SaveData = new SaveData();
      Initialise();
    }

    public FlagsViewModel(SaveData saveData)
    {
      SaveData = saveData;
      Initialise();
    }

    private void Initialise()
    {
      Flags = (FlagInfo[])SaveData.Sections[SaveFileSection.Flags].Data;
      Flagvars = (FlagvarInfo[])SaveData.Sections[SaveFileSection.Flagvars].Data;
      Flagstrings = (FlagstringInfo[])SaveData.Sections[SaveFileSection.Flagstrings].Data;
      Regionals = (RegionalInfo[])SaveData.Sections[SaveFileSection.RegionalFlags].Data;

      regionalFlags = (RegionalFlags)SaveData.Sections[SaveFileSection.RegionalFlags];
      globalInfo = (GlobalInfo)SaveData.Sections[SaveFileSection.Global].Data;
      globalInfo.PropertyChanged += GlobalInfoChanged;
    }

    private void GlobalInfoChanged(object? sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(GlobalInfo.CurrentArea))
        regionalFlags.ChangeCurrentRegionalsArea(globalInfo.CurrentArea);
    }
  }
}
