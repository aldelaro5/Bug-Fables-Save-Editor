using System;
using System.ComponentModel;
using System.Text;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using CommunityToolkit.Mvvm.ComponentModel;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flags;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagstrings;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagvars;
using static BugFablesSaveEditor.BugFablesSave.Sections.Global;
using static BugFablesSaveEditor.BugFablesSave.Sections.RegionalFlags;

namespace BugFablesSaveEditor.ViewModels;

public partial class FlagsViewModel : ObservableObject
{
  [ObservableProperty]
  private bool _filterUnusedRegionals;

  partial void OnFilterUnusedRegionalsChanged(bool value)
  {
    RegionalsFiltered.Refresh();
  }

  [ObservableProperty]
  private FlagInfo[] _flags;

  [ObservableProperty]
  private DataGridCollectionView _flagsFiltered;

  [ObservableProperty]
  private FlagstringInfo[] _flagstrings;
  [ObservableProperty]
  private DataGridCollectionView _flagstringsFiltered;

  [ObservableProperty]
  private FlagvarInfo[] _flagvars;
  [ObservableProperty]
  private DataGridCollectionView _flagvarsFiltered;

  [ObservableProperty]
  private RegionalInfo[] _regionals;
  [ObservableProperty]
  private DataGridCollectionView _regionalsFiltered;

  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private string _textFilterFlags;

  partial void OnTextFilterFlagsChanged(string value)
  {
    _flagsFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterFlagstrings;

  partial void OnTextFilterFlagstringsChanged(string value)
  {
    _flagstringsFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterFlagvars;

  partial void OnTextFilterFlagvarsChanged(string value)
  {
    _flagvarsFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterRegionals;


  partial void OnTextFilterRegionalsChanged(string value)
  {
    _regionalsFiltered.Refresh();
  }

  private GlobalInfo _globalInfo;
  private RegionalFlags _regionalFlags;

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

    _regionalFlags = (RegionalFlags)SaveData.Sections[SaveFileSection.RegionalFlags];
    _globalInfo = (GlobalInfo)SaveData.Sections[SaveFileSection.Global].Data;
    _globalInfo.PropertyChanged += GlobalInfoChanged;

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
      _regionalFlags.ChangeCurrentRegionalsArea(_globalInfo.CurrentArea);
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
