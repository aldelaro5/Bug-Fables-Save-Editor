﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
  private bool _filterUnusedRegionals;

  partial void OnFilterUnusedRegionalsChanged(bool value)
  {
    RegionalsFiltered.Refresh();
  }

  [ObservableProperty]
  private IList<FlagInfo> _flags = null!;

  [ObservableProperty]
  private DataGridCollectionView _flagsFiltered = null!;

  [ObservableProperty]
  private IList<FlagstringInfo> _flagstrings = null!;

  [ObservableProperty]
  private DataGridCollectionView _flagstringsFiltered = null!;

  [ObservableProperty]
  private IList<FlagvarInfo> _flagvars = null!;

  [ObservableProperty]
  private DataGridCollectionView _flagvarsFiltered = null!;

  [ObservableProperty]
  private IList<RegionalInfo> _regionals = null!;

  [ObservableProperty]
  private DataGridCollectionView _regionalsFiltered = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  private string _textFilterFlags = null!;

  partial void OnTextFilterFlagsChanged(string value)
  {
    FlagsFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterFlagstrings = null!;

  partial void OnTextFilterFlagstringsChanged(string value)
  {
    FlagstringsFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterFlagvars = null!;

  partial void OnTextFilterFlagvarsChanged(string value)
  {
    FlagvarsFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterRegionals = null!;

  partial void OnTextFilterRegionalsChanged(string value)
  {
    RegionalsFiltered.Refresh();
  }

  private readonly Global _globalInfo;
  private readonly RegionalFlags _regionalFlags;

  public FlagsViewModel() : this(new SaveData())
  {
  }

  public FlagsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Flags = SaveData.Flags.List;
    Flagvars = SaveData.Flagvars.List;
    Flagstrings = SaveData.Flagstrings.List;
    Regionals = SaveData.RegionalFlags.List;

    _regionalFlags = SaveData.RegionalFlags;
    _globalInfo = SaveData.Global;
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

  private void GlobalInfoChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(Global.CurrentArea))
    {
      _regionalFlags.ChangeCurrentRegionalsArea(_globalInfo.CurrentArea);
      RegionalsFiltered.Refresh();
    }
  }
}
