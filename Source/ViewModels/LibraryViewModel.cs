using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Library;

namespace BugFablesSaveEditor.ViewModels;

public partial class LibraryViewModel : ObservableObject
{
  [ObservableProperty]
  private IList<LibraryFlag> _discoveries = null!;

  [ObservableProperty]
  private DataGridCollectionView _discoveriesFiltered = null!;

  [ObservableProperty]
  private IList<LibraryFlag> _enemies = null!;

  [ObservableProperty]
  private DataGridCollectionView _enemiesFiltered = null!;

  [ObservableProperty]
  private bool _filterUnusedDiscoveries;

  partial void OnFilterUnusedDiscoveriesChanged(bool value)
  {
    DiscoveriesFiltered.Refresh();
  }

  [ObservableProperty]
  private bool _filterUnusedEnemies;

  partial void OnFilterUnusedEnemiesChanged(bool value)
  {
    EnemiesFiltered.Refresh();
  }

  [ObservableProperty]
  private bool _filterUnusedRecipes;

  partial void OnFilterUnusedRecipesChanged(bool value)
  {
    RecipesFiltered.Refresh();
  }

  [ObservableProperty]
  private bool _filterUnusedRecords;

  partial void OnFilterUnusedRecordsChanged(bool value)
  {
    RecordsFiltered.Refresh();
  }

  [ObservableProperty]
  private bool _filterUnusedSeenAreas;

  partial void OnFilterUnusedSeenAreasChanged(bool value)
  {
    SeenAreasFiltered.Refresh();
  }

  [ObservableProperty]
  private IList<LibraryFlag> _recipes = null!;

  [ObservableProperty]
  private DataGridCollectionView _recipesFiltered = null!;

  [ObservableProperty]
  private IList<LibraryFlag> _records = null!;

  [ObservableProperty]
  private DataGridCollectionView _recordsFiltered = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  private IList<LibraryFlag> _seenAreas = null!;

  [ObservableProperty]
  private DataGridCollectionView _seenAreasFiltered = null!;

  [ObservableProperty]
  private string _textFilterDiscoveries = null!;

  partial void OnTextFilterDiscoveriesChanged(string value)
  {
    DiscoveriesFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterEnemies = null!;

  partial void OnTextFilterEnemiesChanged(string value)
  {
    EnemiesFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterRecipes = null!;

  partial void OnTextFilterRecipesChanged(string value)
  {
    RecipesFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterRecords = null!;

  partial void OnTextFilterRecordsChanged(string value)
  {
    RecordsFiltered.Refresh();
  }

  [ObservableProperty]
  private string _textFilterSeenAreas = null!;

  partial void OnTextFilterSeenAreasChanged(string value)
  {
    SeenAreasFiltered.Refresh();
  }

  private string[] areasNames;
  private string[] discoveriesNames;
  private string[] enemiesNames;
  private string[] recipessNames;
  private string[] recordsNames;

  public LibraryViewModel() : this(new SaveData())
  {
  }

  public LibraryViewModel(SaveData saveData)
  {
    SaveData = saveData;
    discoveriesNames = Utils.GetEnumDescriptions<Discovery>();
    enemiesNames = Utils.GetEnumDescriptions<Enemy>();
    recipessNames = Utils.GetEnumDescriptions<Recipe>();
    recordsNames = Utils.GetEnumDescriptions<Record>();
    areasNames = Utils.GetEnumDescriptions<Area>();

    var wholeLibrary = SaveData.Library.List;
    Discoveries = wholeLibrary[(int)LibrarySection.Discovery].List;
    Enemies = wholeLibrary[(int)LibrarySection.Bestiary].List;
    Recipes = wholeLibrary[(int)LibrarySection.Recipe].List;
    Records = wholeLibrary[(int)LibrarySection.Record].List;
    SeenAreas = wholeLibrary[(int)LibrarySection.SeenMapLocation].List;

    DiscoveriesFiltered = new DataGridCollectionView(Discoveries);
    DiscoveriesFiltered.Filter = FilterDiscoveries;
    EnemiesFiltered = new DataGridCollectionView(Enemies);
    EnemiesFiltered.Filter = FilterEnemies;
    RecipesFiltered = new DataGridCollectionView(Recipes);
    RecipesFiltered.Filter = FilterRecipes;
    RecordsFiltered = new DataGridCollectionView(Records);
    RecordsFiltered.Filter = FilterRecords;
    SeenAreasFiltered = new DataGridCollectionView(SeenAreas);
    SeenAreasFiltered.Filter = FilterSeenAreas;
  }

  private bool FilterDiscoveries(object arg)
  {
    return FilterLibrary(LibrarySection.Discovery, arg);
  }

  private bool FilterEnemies(object arg)
  {
    return FilterLibrary(LibrarySection.Bestiary, arg);
  }

  private bool FilterRecipes(object arg)
  {
    return FilterLibrary(LibrarySection.Recipe, arg);
  }

  private bool FilterRecords(object arg)
  {
    return FilterLibrary(LibrarySection.Record, arg);
  }

  private bool FilterSeenAreas(object arg)
  {
    return FilterLibrary(LibrarySection.SeenMapLocation, arg);
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
        enumValues = discoveriesNames;
        filterUnused = FilterUnusedDiscoveries;
        break;
      case LibrarySection.Bestiary:
        textFilter = TextFilterEnemies;
        enumValues = enemiesNames;
        filterUnused = FilterUnusedEnemies;
        break;
      case LibrarySection.Recipe:
        textFilter = TextFilterRecipes;
        enumValues = recipessNames;
        filterUnused = FilterUnusedRecipes;
        break;
      case LibrarySection.Record:
        textFilter = TextFilterRecords;
        enumValues = recordsNames;
        filterUnused = FilterUnusedRecords;
        break;
      case LibrarySection.SeenMapLocation:
        textFilter = TextFilterSeenAreas;
        enumValues = areasNames;
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
    {
      sb.Append("UNUSED " + flag.Index);
    }
    else
    {
      sb.Append(enumValues[flag.Index]);
    }

    return sb.ToString().Contains(textFilter, StringComparison.OrdinalIgnoreCase);
  }

  [RelayCommand]
  private void ToggleAllFilteredDiscoveries()
  {
    ToggleAllShownLibrary(LibrarySection.Discovery);
  }

  [RelayCommand]
  private void ToggleAllFilteredEnemies()
  {
    ToggleAllShownLibrary(LibrarySection.Bestiary);
  }

  [RelayCommand]
  private void ToggleAllFilteredRecipes()
  {
    ToggleAllShownLibrary(LibrarySection.Recipe);
  }

  [RelayCommand]
  private void ToggleAllFilteredRecords()
  {
    ToggleAllShownLibrary(LibrarySection.Record);
  }

  [RelayCommand]
  private void ToggleAllFilteredSeenAreas()
  {
    ToggleAllShownLibrary(LibrarySection.SeenMapLocation);
  }

  private void ToggleAllShownLibrary(LibrarySection section)
  {
    bool newEnabled = true;
    DataGridCollectionView dg;
    switch (section)
    {
      case LibrarySection.Discovery:
        dg = DiscoveriesFiltered;
        break;
      case LibrarySection.Bestiary:
        dg = EnemiesFiltered;
        break;
      case LibrarySection.Recipe:
        dg = RecipesFiltered;
        break;
      case LibrarySection.Record:
        dg = RecordsFiltered;
        break;
      case LibrarySection.SeenMapLocation:
        dg = SeenAreasFiltered;
        break;
      default:
        return;
    }

    foreach (object? item in dg)
    {
      LibraryFlag flag = (LibraryFlag)item;
      if (flag.Enabled)
      {
        newEnabled = false;
        break;
      }
    }

    foreach (object? item in dg)
    {
      LibraryFlag flag = (LibraryFlag)item;
      flag.Enabled = newEnabled;
    }
  }
}
