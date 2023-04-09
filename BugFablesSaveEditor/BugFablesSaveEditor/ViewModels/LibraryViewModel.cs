using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using BugFablesLib;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;
using static BugFablesSaveEditor.FilterUtils;

namespace BugFablesSaveEditor.ViewModels;

public partial class LibraryViewModel : ObservableObject, IDisposable
{
  private readonly IDisposable _discoveriesDisposable;
  private readonly IDisposable _bestiaryDisposable;
  private readonly IDisposable _recipesDisposable;
  private readonly IDisposable _recordsDisposable;
  private readonly IDisposable _seenAreasDisposable;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _discoveries;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _bestiary;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _recipes;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _records;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _seenAreas;

  [ObservableProperty]
  private string _textFilterDiscoveries = "";

  [ObservableProperty]
  private bool _filterUnusedDiscoveries;

  [ObservableProperty]
  private string _textFilterBestiary = "";

  [ObservableProperty]
  private bool _filterUnusedBestiary;

  [ObservableProperty]
  private string _textFilterRecipes = "";

  [ObservableProperty]
  private bool _filterUnusedRecipes;

  [ObservableProperty]
  private string _textFilterRecords = "";

  [ObservableProperty]
  private bool _filterUnusedRecords;

  [ObservableProperty]
  private string _textFilterSeenAreas = "";

  [ObservableProperty]
  private bool _filterUnusedSeenAreas;

  public LibraryViewModel() : this(new()) { }

  public LibraryViewModel(LibrarySaveData librarySaveData)
  {
    _discoveriesDisposable = ObserveFlagsWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.Discoveries, BfVanillaNames.Discoveries),
      GetFilter(x => x.TextFilterDiscoveries, x => x.FilterUnusedDiscoveries), out _discoveries);

    _bestiaryDisposable = ObserveFlagsWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.Bestiary, BfVanillaNames.Enemies),
      GetFilter(x => x.TextFilterBestiary, x => x.FilterUnusedBestiary), out _bestiary);

    _recipesDisposable = ObserveFlagsWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.Recipes, BfVanillaNames.Recipes),
      GetFilter(x => x.TextFilterRecipes, x => x.FilterUnusedRecipes), out _recipes);

    _recordsDisposable = ObserveFlagsWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.Records, BfVanillaNames.Records),
      GetFilter(x => x.TextFilterRecords, x => x.FilterUnusedRecords), out _records);

    _seenAreasDisposable = ObserveFlagsWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.SeenAreas, BfVanillaNames.Areas),
      GetFilter(x => x.TextFilterSeenAreas, x => x.FilterUnusedSeenAreas), out _seenAreas);
  }

  [RelayCommand]
  private void ToggleAllShown(ReadOnlyObservableCollection<FlagSaveDataModel> collection)
  {
    bool newState = collection.Any(x => !x.Enabled);
    foreach (FlagSaveDataModel flagVm in collection)
      flagVm.Enabled = newState;
  }

  private static IEnumerable<FlagSaveDataModel> WrapFlagsWithMetadata(Collection<FlagSaveData> data,
                                                                      IReadOnlyList<string> names)
  {
    return data.Select((x, i) => new FlagSaveDataModel(x)
    {
      Index = i,
      Description1 = i < names.Count ? names[i] : ""
    }).ToList();
  }

  private IObservable<Func<FlagSaveDataModel, bool>> GetFilter(Expression<Func<LibraryViewModel, string>> filterChange,
                                                               Expression<Func<LibraryViewModel, bool>> unusedChange)
  {
    return this.WhenChanged(filterChange, unusedChange,
        (_, text, keepUnused) => (text, keepUnused))
      .Throttle(TimeSpan.FromMilliseconds(250))
      .Select(FlagTextFilterWithUnused!);
  }

  public void Dispose()
  {
    _discoveriesDisposable.Dispose();
    _bestiaryDisposable.Dispose();
    _recipesDisposable.Dispose();
    _recordsDisposable.Dispose();
    _seenAreasDisposable.Dispose();
  }
}
