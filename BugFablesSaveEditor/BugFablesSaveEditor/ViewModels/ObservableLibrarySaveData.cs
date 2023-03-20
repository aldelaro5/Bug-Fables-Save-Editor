using System;
using System.Collections.Specialized;
using System.Linq;
using BugFablesLib;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class ObservableLibrarySaveData : ObservableObject
{

  private readonly LibrarySaveData _libraryQuestsSaveData;

  [ObservableProperty]
  private TrackedObservableCollection<ObservableFlagSaveData> _discoveries;
  [ObservableProperty]
  private TrackedObservableCollection<ObservableFlagSaveData> _bestiary;
  [ObservableProperty]
  private TrackedObservableCollection<ObservableFlagSaveData> _recipes;
  [ObservableProperty]
  private TrackedObservableCollection<ObservableFlagSaveData> _records;
  [ObservableProperty]
  private TrackedObservableCollection<ObservableFlagSaveData> _seenAreas;

  public ObservableLibrarySaveData(LibrarySaveData librarySaveData)
  {
    _libraryQuestsSaveData = librarySaveData;
    _discoveries = new(_libraryQuestsSaveData.Discoveries
      .Select(x => new ObservableFlagSaveData(x)).ToList());
    _discoveries.CollectionChanged += DiscoveriesOnCollectionChanged;

    _bestiary = new(_libraryQuestsSaveData.Bestiary
      .Select(x => new ObservableFlagSaveData(x)).ToList());
    _bestiary.CollectionChanged += BestiaryOnCollectionChanged;

    _recipes = new(_libraryQuestsSaveData.Recipes
      .Select(x => new ObservableFlagSaveData(x)).ToList());
    _recipes.CollectionChanged += RecipesOnCollectionChanged;

    _records = new(_libraryQuestsSaveData.Records
      .Select(x => new ObservableFlagSaveData(x)).ToList());
    _records.CollectionChanged += RecordsOnCollectionChanged;

    _seenAreas = new(_libraryQuestsSaveData.SeenAreas
      .Select(x => new ObservableFlagSaveData(x)).ToList());
    _seenAreas.CollectionChanged += SeenAreasOnCollectionChanged;
  }

  private void DiscoveriesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _libraryQuestsSaveData.Discoveries);

  private void BestiaryOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _libraryQuestsSaveData.Bestiary);

  private void RecipesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _libraryQuestsSaveData.Recipes);

  private void RecordsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _libraryQuestsSaveData.Records);

  private void SeenAreasOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _libraryQuestsSaveData.SeenAreas);

  private void CollectionChanged(NotifyCollectionChangedEventArgs e,
                                 BfSerializableCollection<FlagSaveData> collection)
  {
    var newList = e.NewItems?.Cast<FlagSaveData>().ToList();
    switch (e.Action)
    {
      case NotifyCollectionChangedAction.Add:
        collection.Insert(e.NewStartingIndex, new FlagSaveData { Enabled = newList![0].Enabled });
        break;
      case NotifyCollectionChangedAction.Remove:
        collection.RemoveAt(e.OldStartingIndex);
        break;
      case NotifyCollectionChangedAction.Replace:
        break;
      case NotifyCollectionChangedAction.Move:
        break;
      case NotifyCollectionChangedAction.Reset:
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  public void Dispose()
  {
    Discoveries.CollectionChanged -= DiscoveriesOnCollectionChanged;
    Bestiary.CollectionChanged -= BestiaryOnCollectionChanged;
    Recipes.CollectionChanged -= RecipesOnCollectionChanged;
    Records.CollectionChanged -= RecordsOnCollectionChanged;
    Records.CollectionChanged -= SeenAreasOnCollectionChanged;
  }
}
