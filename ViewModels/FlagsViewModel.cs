using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Text;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flags;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagstrings;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagvars;
using static BugFablesSaveEditor.BugFablesSave.Sections.Global;
using static BugFablesSaveEditor.BugFablesSave.Sections.RegionalFlags;

namespace BugFablesSaveEditor.ViewModels
{
  public class FlagsViewModel : ViewModelBase
  {
    private enum FlagsType
    {
      Flags,
      Flagvars,
      Flagstrings,
      Regionals
    }

    private GlobalInfo globalInfo;
    private RegionalFlags regionalFlags;

    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private bool _filterUnusedRegionals;
    public bool FilterUnusedRegionals
    {
      get { return _filterUnusedRegionals; }
      set
      {
        _filterUnusedRegionals = value;
        this.RaisePropertyChanged();
        RegionalsFiltered.Refresh();
      }
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

    private DataGridCollectionView _flagsFiltered;
    public DataGridCollectionView FlagsFiltered
    {
      get { return _flagsFiltered; }
      set { _flagsFiltered = value; this.RaisePropertyChanged(); }
    }
    private DataGridCollectionView _flagvarsFiltered;
    public DataGridCollectionView FlagvarsFiltered
    {
      get { return _flagvarsFiltered; }
      set { _flagvarsFiltered = value; this.RaisePropertyChanged(); }
    }
    private DataGridCollectionView _flagstringsFiltered;
    public DataGridCollectionView FlagstringsFiltered
    {
      get { return _flagstringsFiltered; }
      set { _flagstringsFiltered = value; this.RaisePropertyChanged(); }
    }
    private DataGridCollectionView _regionalsFiltered;
    public DataGridCollectionView RegionalsFiltered
    {
      get { return _regionalsFiltered; }
      set { _regionalsFiltered = value; this.RaisePropertyChanged(); }
    }

    private string _textFilterFlags;
    public string TextFilterFlags
    {
      get { return _textFilterFlags; }
      set
      {
        _textFilterFlags = value;
        this.RaisePropertyChanged();
        FlagsFiltered.Refresh();
      }
    }
    private string _textFilterFlagvars;
    public string TextFilterFlagvars
    {
      get { return _textFilterFlagvars; }
      set
      {
        _textFilterFlagvars = value;
        this.RaisePropertyChanged();
        FlagvarsFiltered.Refresh();
      }
    }
    private string _textFilterFlagstrings;
    public string TextFilterFlagstrings
    {
      get { return _textFilterFlagstrings; }
      set
      {
        _textFilterFlagstrings = value;
        this.RaisePropertyChanged();
        FlagstringsFiltered.Refresh();
      }
    }
    private string _textFilterRegionals;
    public string TextFilterRegionals
    {
      get { return _textFilterRegionals; }
      set
      {
        _textFilterRegionals = value;
        this.RaisePropertyChanged();
        RegionalsFiltered.Refresh();
      }
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

      FlagsFiltered = new DataGridCollectionView(Flags);
      FlagsFiltered.Filter = FilterFlags;
      FlagvarsFiltered = new DataGridCollectionView(Flagvars);
      FlagvarsFiltered.Filter = FilterFlagvars;
      FlagstringsFiltered = new DataGridCollectionView(Flagstrings);
      FlagstringsFiltered.Filter = FilterFlagstrings;
      RegionalsFiltered = new DataGridCollectionView(Regionals);
      RegionalsFiltered.Filter = FilterRegionals;
    }

    private bool FilterFlags(object arg)
    {
      return FilterFlag(FlagsType.Flags, arg);
    }

    private bool FilterFlagvars(object arg)
    {
      return FilterFlag(FlagsType.Flagvars, arg);
    }

    private bool FilterFlagstrings(object arg)
    {
      return FilterFlag(FlagsType.Flagstrings, arg);
    }

    private bool FilterRegionals(object arg)
    {
      return FilterFlag(FlagsType.Regionals, arg);
    }

    private bool FilterFlag(FlagsType type, object arg)
    {
      int flagIndex;
      string flagDescription;
      string textFilter;
      switch (type)
      {
        case FlagsType.Flags:
          textFilter = TextFilterFlags;
          var flag = (FlagInfo)arg;
          flagIndex = flag.Index;
          flagDescription = flag.Description;
          break;
        case FlagsType.Flagvars:
          textFilter = TextFilterFlagvars;
          var flagvar = (FlagvarInfo)arg;
          flagIndex = flagvar.Index;
          flagDescription = flagvar.Description;
          break;
        case FlagsType.Flagstrings:
          textFilter = TextFilterFlagstrings;
          var flagstring = (FlagstringInfo)arg;
          flagIndex = flagstring.Index;
          flagDescription = flagstring.Description;
          break;
        case FlagsType.Regionals:
          textFilter = TextFilterRegionals;
          var regional = (RegionalInfo)arg;
          if (regional.Description == "UNUSED" && !FilterUnusedRegionals)
            return false;
          flagIndex = regional.Index;
          flagDescription = regional.Description;
          break;
        default:
          return false;
      }

      if (string.IsNullOrEmpty(textFilter))
        return true;

      StringBuilder sb = new StringBuilder();
      sb.Append(flagIndex).Append(' ');
      sb.Append(flagDescription);
      return sb.ToString().Contains(textFilter, StringComparison.OrdinalIgnoreCase);
    }

    private void GlobalInfoChanged(object? sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(GlobalInfo.CurrentArea))
      {
        regionalFlags.ChangeCurrentRegionalsArea(globalInfo.CurrentArea);
        RegionalsFiltered.Refresh();
      }
    }
  }
}
