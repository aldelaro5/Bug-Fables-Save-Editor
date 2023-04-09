using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Threading;
using BugFablesLib;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;

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
    _discoveriesDisposable = ObserveDataWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.Discoveries, BfVanillaNames.Discoveries), x => TextFilterDiscoveries,
      x => x.FilterUnusedDiscoveries, out _discoveries);

    _bestiaryDisposable = ObserveDataWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.Bestiary, BfVanillaNames.Enemies), x => TextFilterBestiary,
      x => x.FilterUnusedBestiary, out _bestiary);

    _recipesDisposable = ObserveDataWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.Recipes, BfVanillaNames.Recipes), x => TextFilterRecipes,
      x => x.FilterUnusedRecipes, out _recipes);

    _recordsDisposable = ObserveDataWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.Records, BfVanillaNames.Records), x => TextFilterRecords,
      x => x.FilterUnusedRecords, out _records);

    _seenAreasDisposable = ObserveDataWithFilterAndSort(
      WrapFlagsWithMetadata(librarySaveData.SeenAreas, BfVanillaNames.Areas), x => TextFilterSeenAreas,
      x => x.FilterUnusedSeenAreas, out _seenAreas);
  }

  [RelayCommand]
  private void ToggleAllShown(ReadOnlyObservableCollection<FlagSaveDataModel> collection)
  {
    bool newState = collection.Any(x => !x.Enabled);
    foreach (FlagSaveDataModel flagVm in collection)
      flagVm.Enabled = newState;
  }

  private List<FlagSaveDataModel> WrapFlagsWithMetadata(Collection<FlagSaveData> data, IReadOnlyList<string> names)
  {
    return data.Select((x, i) => new FlagSaveDataModel(x) { Index = i, Description1 = i < names.Count ? names[i] : "" })
      .ToList();
  }

  private IDisposable ObserveDataWithFilterAndSort(List<FlagSaveDataModel> data,
                                                   Expression<Func<LibraryViewModel, string>> filterChange,
                                                   Expression<Func<LibraryViewModel, bool>> unusedChange,
                                                   out ReadOnlyObservableCollection<FlagSaveDataModel> result)
  {
    return data
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(filterChange, unusedChange,
          (_, text, keepUnused) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagFilter!))
      .Sort(SortExpressionComparer<FlagSaveDataModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out result)
      .Subscribe();
  }

  private Func<FlagSaveDataModel, bool> FlagFilter((string text, bool keepUnused) filter)
  {
    return vm => (filter.keepUnused || vm.Description1 != string.Empty) &&
                 (filter.text == string.Empty ||
                  (vm.Description1 == string.Empty && filter.keepUnused) ||
                  vm.Index.ToString().Contains(filter.text, StringComparison.OrdinalIgnoreCase) ||
                  vm.Description1.Contains(filter.text, StringComparison.OrdinalIgnoreCase));
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
