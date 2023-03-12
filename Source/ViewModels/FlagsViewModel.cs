using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using CommunityToolkit.Mvvm.ComponentModel;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flags;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagstrings;
using static BugFablesSaveEditor.BugFablesSave.Sections.Flagvars;
using static BugFablesSaveEditor.BugFablesSave.Sections.RegionalFlags;

namespace BugFablesSaveEditor.ViewModels;

public partial class FlagsViewModel : ObservableObject
{
  private enum FlagsType
  {
    Flags,
    Flagvars,
    Flagstrings,
    Regionals
  }

  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private IList<FlagInfo> _flags;

  [ObservableProperty]
  private DataGridCollectionView _flagsFiltered;

  [ObservableProperty]
  private string _textFilterFlags = "";

  partial void OnTextFilterFlagsChanged(string value) => FlagsFiltered.Refresh();

  [ObservableProperty]
  private IList<FlagstringInfo> _flagstrings;

  [ObservableProperty]
  private DataGridCollectionView _flagstringsFiltered;

  [ObservableProperty]
  private string _textFilterFlagstrings = "";

  partial void OnTextFilterFlagstringsChanged(string value) => FlagstringsFiltered.Refresh();

  [ObservableProperty]
  private IList<FlagvarInfo> _flagvars;

  [ObservableProperty]
  private DataGridCollectionView _flagvarsFiltered;

  [ObservableProperty]
  private string _textFilterFlagvars = "";

  partial void OnTextFilterFlagvarsChanged(string value) => FlagvarsFiltered.Refresh();

  [ObservableProperty]
  private IList<RegionalInfo> _regionals;

  [ObservableProperty]
  private DataGridCollectionView _regionalsFiltered;

  [ObservableProperty]
  private string _textFilterRegionals = "";

  partial void OnTextFilterRegionalsChanged(string value) => RegionalsFiltered.Refresh();

  [ObservableProperty]
  private bool _filterUnusedRegionals;

  partial void OnFilterUnusedRegionalsChanged(bool value) => RegionalsFiltered.Refresh();

  private readonly Global _globalInfo;
  private readonly RegionalFlags _regionalFlags;

  public FlagsViewModel() : this(new SaveData())
  {
  }

  public FlagsViewModel(SaveData saveData)
  {
    _saveData = saveData;
    _flags = _saveData.Flags.List;
    _flagvars = _saveData.Flagvars.List;
    _flagstrings = _saveData.Flagstrings.List;
    _regionals = _saveData.RegionalFlags.List;

    _regionalFlags = _saveData.RegionalFlags;
    _globalInfo = _saveData.Global;
    _globalInfo.PropertyChanged += (_, e) =>
    {
      if (e.PropertyName != nameof(Global.CurrentArea))
        return;

      _regionalFlags.ChangeCurrentRegionalsArea(_globalInfo.CurrentArea);
      RegionalsFiltered.Refresh();
    };

    _flagsFiltered = new(_flags);
    _flagsFiltered.Filter = arg => FilterFlag(FlagsType.Flags, arg);
    _flagvarsFiltered = new(_flagvars);
    _flagvarsFiltered.Filter = arg => FilterFlag(FlagsType.Flagvars, arg);
    _flagstringsFiltered = new(_flagstrings);
    _flagstringsFiltered.Filter = arg => FilterFlag(FlagsType.Flagstrings, arg);
    _regionalsFiltered = new(_regionals);
    _regionalsFiltered.Filter = arg => FilterFlag(FlagsType.Regionals, arg);
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
          return false;

        flagIndex = regional.Index;
        flagDescription = regional.Description;
        break;
      default:
        return false;
    }

    if (string.IsNullOrEmpty(textFilter))
      return true;

    StringBuilder sb = new();
    sb.Append(flagIndex).Append(' ');
    sb.Append(flagDescription);
    return sb.ToString().Contains(textFilter, StringComparison.OrdinalIgnoreCase);
  }
}
