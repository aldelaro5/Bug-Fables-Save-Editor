using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using BugFablesLib;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;

namespace BugFablesSaveEditor.ViewModels;

public partial class LibraryViewModel : ObservableObject
{
  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagViewModel> _discoveries;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagViewModel> _bestiary;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagViewModel> _recipes;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagViewModel> _records;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagViewModel> _seenAreas;

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
    librarySaveData.Discoveries
      .Select((x, i) => new FlagViewModel
      {
        Index = i, Flag = new(x), Description = i < BfVanillaNames.Discoveries.Count ? BfVanillaNames.Discoveries[i] : ""
      }).ToList()
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(x => x.TextFilterDiscoveries, x => x.FilterUnusedDiscoveries,
          (_, text, keepUnused) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagFilter!))
      .Sort(SortExpressionComparer<FlagViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _discoveries)
      .Subscribe();

    librarySaveData.Bestiary
      .Select((x, i) => new FlagViewModel
      {
        Index = i, Flag = new(x), Description = i < BfVanillaNames.Enemies.Count ? BfVanillaNames.Enemies[i] : ""
      }).ToList()
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(x => x.TextFilterBestiary, x => x.FilterUnusedBestiary,
          (_, text, keepUnused) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagFilter!))
      .Sort(SortExpressionComparer<FlagViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _bestiary)
      .Subscribe();

    librarySaveData.Recipes
      .Select((x, i) => new FlagViewModel
      {
        Index = i, Flag = new(x), Description = i < BfVanillaNames.Recipes.Count ? BfVanillaNames.Recipes[i] : ""
      }).ToList()
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(x => x.TextFilterRecipes, x => x.FilterUnusedRecipes,
          (_, text, keepUnused) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagFilter!))
      .Sort(SortExpressionComparer<FlagViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _recipes)
      .Subscribe();

    librarySaveData.Records
      .Select((x, i) => new FlagViewModel
      {
        Index = i, Flag = new(x), Description = i < BfVanillaNames.Records.Count ? BfVanillaNames.Records[i] : ""
      }).ToList()
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(x => x.TextFilterRecords, x => x.FilterUnusedRecords,
          (_, text, keepUnused) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagFilter!))
      .Sort(SortExpressionComparer<FlagViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _records)
      .Subscribe();

    librarySaveData.SeenAreas
      .Select((x, i) => new FlagViewModel
      {
        Index = i, Flag = new(x), Description = i < BfVanillaNames.Areas.Count ? BfVanillaNames.Areas[i] : ""
      }).ToList()
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(x => x.TextFilterSeenAreas, x => x.FilterUnusedSeenAreas,
          (_, text, keepUnused) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagFilter!))
      .Sort(SortExpressionComparer<FlagViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _seenAreas)
      .Subscribe();
  }

  [RelayCommand]
  private void ToggleAllShownDiscoveries() => ToggleAllShown(Discoveries);

  [RelayCommand]
  private void ToggleAllShownBestiary() => ToggleAllShown(Bestiary);

  [RelayCommand]
  private void ToggleAllShownRecipes() => ToggleAllShown(Recipes);

  [RelayCommand]
  private void ToggleAllShownRecords() => ToggleAllShown(Records);

  [RelayCommand]
  private void ToggleAllShownSeenAreas() => ToggleAllShown(SeenAreas);

  private void ToggleAllShown(ReadOnlyObservableCollection<FlagViewModel> collection)
  {
    bool newState = collection.Any(x => !x.Flag.Enabled);
    foreach (FlagViewModel flagVm in collection)
      flagVm.Flag.Enabled = newState;
  }

  private Func<FlagViewModel, bool> FlagFilter((string text, bool keepUnused) filter)
  {
    return vm => (filter.keepUnused || vm.Description != string.Empty) &&
                 (filter.text == string.Empty ||
                  (vm.Description == string.Empty && filter.keepUnused) ||
                  vm.Index.ToString().Contains(filter.text, StringComparison.OrdinalIgnoreCase) ||
                  vm.Description.Contains(filter.text, StringComparison.OrdinalIgnoreCase));
  }
}
