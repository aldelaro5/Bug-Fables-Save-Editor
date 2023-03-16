using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Collections;
using BugFablesDataLib;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesDataLib.Sections.Library;

namespace BugFablesSaveEditor.ViewModels;

public partial class LibraryViewModel : ObservableObject
{
  private readonly string[] _areasNames = Utils.GetEnumDescriptions<Area>();
  private readonly string[] _discoveriesNames = Utils.GetEnumDescriptions<Discovery>();
  private readonly string[] _enemiesNames = Utils.GetEnumDescriptions<Enemy>();
  private readonly string[] _recipesNames = Utils.GetEnumDescriptions<Recipe>();
  private readonly string[] _recordsNames = Utils.GetEnumDescriptions<Record>();

  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private IList<LibraryFlag> _discoveries;

  [ObservableProperty]
  private DataGridCollectionView _discoveriesFiltered;

  [ObservableProperty]
  private string _textFilterDiscoveries = "";

  partial void OnTextFilterDiscoveriesChanged(string value) => DiscoveriesFiltered.Refresh();

  [ObservableProperty]
  private bool _filterUnusedDiscoveries;

  partial void OnFilterUnusedDiscoveriesChanged(bool value) => DiscoveriesFiltered.Refresh();

  [ObservableProperty]
  private IList<LibraryFlag> _enemies;

  [ObservableProperty]
  private DataGridCollectionView _enemiesFiltered;

  [ObservableProperty]
  private string _textFilterEnemies = "";

  partial void OnTextFilterEnemiesChanged(string value) => EnemiesFiltered.Refresh();

  [ObservableProperty]
  private bool _filterUnusedEnemies;

  partial void OnFilterUnusedEnemiesChanged(bool value) => EnemiesFiltered.Refresh();

  [ObservableProperty]
  private IList<LibraryFlag> _recipes;

  [ObservableProperty]
  private DataGridCollectionView _recipesFiltered;

  [ObservableProperty]
  private string _textFilterRecipes = "";

  partial void OnTextFilterRecipesChanged(string value) => RecipesFiltered.Refresh();

  [ObservableProperty]
  private bool _filterUnusedRecipes;

  partial void OnFilterUnusedRecipesChanged(bool value) => RecipesFiltered.Refresh();

  [ObservableProperty]
  private IList<LibraryFlag> _records;

  [ObservableProperty]
  private DataGridCollectionView _recordsFiltered;

  [ObservableProperty]
  private string _textFilterRecords = "";

  partial void OnTextFilterRecordsChanged(string value) => RecordsFiltered.Refresh();

  [ObservableProperty]
  private bool _filterUnusedRecords;

  partial void OnFilterUnusedRecordsChanged(bool value) => RecordsFiltered.Refresh();

  [ObservableProperty]
  private IList<LibraryFlag> _seenAreas;

  [ObservableProperty]
  private DataGridCollectionView _seenAreasFiltered;

  [ObservableProperty]
  private string _textFilterSeenAreas = "";

  partial void OnTextFilterSeenAreasChanged(string value) => SeenAreasFiltered.Refresh();

  [ObservableProperty]
  private bool _filterUnusedSeenAreas;

  partial void OnFilterUnusedSeenAreasChanged(bool value) => SeenAreasFiltered.Refresh();

  public LibraryViewModel() : this(new SaveData())
  {
  }

  public LibraryViewModel(SaveData saveData)
  {
    _saveData = saveData;

    _discoveries = _saveData.Library.Discoveries;
    _enemies = _saveData.Library.Bestiary;
    _recipes = _saveData.Library.Recipes;
    _records = _saveData.Library.Records;
    _seenAreas = _saveData.Library.SeenAreas;

    _discoveriesFiltered = new(_discoveries);
    _discoveriesFiltered.Filter = arg => FilterLibrary(LibrarySection.Discovery, arg);
    _enemiesFiltered = new(_enemies);
    _enemiesFiltered.Filter = arg => FilterLibrary(LibrarySection.Bestiary, arg);
    _recipesFiltered = new(_recipes);
    _recipesFiltered.Filter = arg => FilterLibrary(LibrarySection.Recipe, arg);
    _recordsFiltered = new(_records);
    _recordsFiltered.Filter = arg => FilterLibrary(LibrarySection.Record, arg);
    _seenAreasFiltered = new(_seenAreas);
    _seenAreasFiltered.Filter = arg => FilterLibrary(LibrarySection.SeenAreas, arg);
  }

  private bool FilterLibrary(LibrarySection section, object arg)
  {
    LibraryFlag flag = (LibraryFlag)arg;
    string textFilter;
    string[] enumValues;
    bool filterUnused;
    switch (section)
    {
      case LibrarySection.Discovery:
        textFilter = TextFilterDiscoveries;
        enumValues = _discoveriesNames;
        filterUnused = FilterUnusedDiscoveries;
        break;
      case LibrarySection.Bestiary:
        textFilter = TextFilterEnemies;
        enumValues = _enemiesNames;
        filterUnused = FilterUnusedEnemies;
        break;
      case LibrarySection.Recipe:
        textFilter = TextFilterRecipes;
        enumValues = _recipesNames;
        filterUnused = FilterUnusedRecipes;
        break;
      case LibrarySection.Record:
        textFilter = TextFilterRecords;
        enumValues = _recordsNames;
        filterUnused = FilterUnusedRecords;
        break;
      case LibrarySection.SeenAreas:
        textFilter = TextFilterSeenAreas;
        enumValues = _areasNames;
        filterUnused = FilterUnusedSeenAreas;
        break;
      default:
        return false;
    }

    if (!filterUnused && flag.Index >= enumValues.Length)
      return false;

    if (string.IsNullOrEmpty(textFilter))
      return true;

    StringBuilder sb = new();
    sb.Append(flag.Index).Append(' ');
    if (flag.Index >= enumValues.Length)
      sb.Append("UNUSED " + flag.Index);
    else
      sb.Append(enumValues[flag.Index]);

    return sb.ToString().Contains(textFilter, StringComparison.OrdinalIgnoreCase);
  }

  [RelayCommand]
  private void ToggleAllFilteredDiscoveries() => ToggleAllShownLibrary(LibrarySection.Discovery);

  [RelayCommand]
  private void ToggleAllFilteredEnemies() => ToggleAllShownLibrary(LibrarySection.Bestiary);

  [RelayCommand]
  private void ToggleAllFilteredRecipes() => ToggleAllShownLibrary(LibrarySection.Recipe);

  [RelayCommand]
  private void ToggleAllFilteredRecords() => ToggleAllShownLibrary(LibrarySection.Record);

  [RelayCommand]
  private void ToggleAllFilteredSeenAreas() => ToggleAllShownLibrary(LibrarySection.SeenAreas);

  private void ToggleAllShownLibrary(LibrarySection section)
  {
    IList<LibraryFlag> list;
    switch (section)
    {
      case LibrarySection.Discovery:
        list = Discoveries;
        break;
      case LibrarySection.Bestiary:
        list = Enemies;
        break;
      case LibrarySection.Recipe:
        list = Recipes;
        break;
      case LibrarySection.Record:
        list = Records;
        break;
      case LibrarySection.SeenAreas:
        list = SeenAreas;
        break;
      default:
        return;
    }

    bool newEnabled = !list.Any(x => x.Enabled);
    foreach (LibraryFlag flag in list)
      flag.Enabled = newEnabled;
  }
}
