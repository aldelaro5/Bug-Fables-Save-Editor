using System;
using System.ComponentModel;
using System.Text;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using ReactiveUI;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flags;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagstrings;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagvars;
using static BugFablesSaveEditor.BugFablesSave.Sections.Global;
using static BugFablesSaveEditor.BugFablesSave.Sections.RegionalFlags;

namespace BugFablesSaveEditor.ViewModels;

public class FlagsViewModel : ViewModelBase
{
  private bool _filterUnusedRegionals;

  private FlagInfo[] _flags;

  private DataGridCollectionView _flagsFiltered;

  private FlagstringInfo[] _flagstrings;
  private DataGridCollectionView _flagstringsFiltered;

  private FlagvarInfo[] _flagvars;
  private DataGridCollectionView _flagvarsFiltered;

  private RegionalInfo[] _regionals;
  private DataGridCollectionView _regionalsFiltered;

  private SaveData _saveData;

  private string _textFilterFlags;
  private string _textFilterFlagstrings;
  private string _textFilterFlagvars;
  private string _textFilterRegionals;

  private GlobalInfo globalInfo;
  private RegionalFlags regionalFlags;

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

  public SaveData SaveData
  {
    get => _saveData;
    set
    {
      _saveData = value;
      this.RaisePropertyChanged();
    }
  }

  public bool FilterUnusedRegionals
  {
    get => _filterUnusedRegionals;
    set
    {
      _filterUnusedRegionals = value;
      this.RaisePropertyChanged();
      RegionalsFiltered.Refresh();
    }
  }

  public FlagInfo[] Flags
  {
    get => _flags;
    set
    {
      _flags = value;
      this.RaisePropertyChanged();
    }
  }

  public FlagvarInfo[] Flagvars
  {
    get => _flagvars;
    set
    {
      _flagvars = value;
      this.RaisePropertyChanged();
    }
  }

  public FlagstringInfo[] Flagstrings
  {
    get => _flagstrings;
    set
    {
      _flagstrings = value;
      this.RaisePropertyChanged();
    }
  }

  public RegionalInfo[] Regionals
  {
    get => _regionals;
    set
    {
      _regionals = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView FlagsFiltered
  {
    get => _flagsFiltered;
    set
    {
      _flagsFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView FlagvarsFiltered
  {
    get => _flagvarsFiltered;
    set
    {
      _flagvarsFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView FlagstringsFiltered
  {
    get => _flagstringsFiltered;
    set
    {
      _flagstringsFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView RegionalsFiltered
  {
    get => _regionalsFiltered;
    set
    {
      _regionalsFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public string TextFilterFlags
  {
    get => _textFilterFlags;
    set
    {
      _textFilterFlags = value;
      this.RaisePropertyChanged();
      FlagsFiltered.Refresh();
    }
  }

  public string TextFilterFlagvars
  {
    get => _textFilterFlagvars;
    set
    {
      _textFilterFlagvars = value;
      this.RaisePropertyChanged();
      FlagvarsFiltered.Refresh();
    }
  }

  public string TextFilterFlagstrings
  {
    get => _textFilterFlagstrings;
    set
    {
      _textFilterFlagstrings = value;
      this.RaisePropertyChanged();
      FlagstringsFiltered.Refresh();
    }
  }

  public string TextFilterRegionals
  {
    get => _textFilterRegionals;
    set
    {
      _textFilterRegionals = value;
      this.RaisePropertyChanged();
      RegionalsFiltered.Refresh();
    }
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
        FlagInfo flag = (FlagInfo)arg;
        flagIndex = flag.Index;
        flagDescription = flag.Description;
        break;
      case FlagsType.Flagvars:
        textFilter = TextFilterFlagvars;
        FlagvarInfo flagvar = (FlagvarInfo)arg;
        flagIndex = flagvar.Index;
        flagDescription = flagvar.Description;
        break;
      case FlagsType.Flagstrings:
        textFilter = TextFilterFlagstrings;
        FlagstringInfo flagstring = (FlagstringInfo)arg;
        flagIndex = flagstring.Index;
        flagDescription = flagstring.Description;
        break;
      case FlagsType.Regionals:
        textFilter = TextFilterRegionals;
        RegionalInfo regional = (RegionalInfo)arg;
        if (regional.Description == "UNUSED" && !FilterUnusedRegionals)
        {
          return false;
        }

        flagIndex = regional.Index;
        flagDescription = regional.Description;
        break;
      default:
        return false;
    }

    if (string.IsNullOrEmpty(textFilter))
    {
      return true;
    }

    StringBuilder sb = new();
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

  private enum FlagsType
  {
    Flags,
    Flagvars,
    Flagstrings,
    Regionals
  }
}
