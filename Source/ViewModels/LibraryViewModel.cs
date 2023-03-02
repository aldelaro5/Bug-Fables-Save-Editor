using System;
using System.Text;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using static BugFablesSaveEditor.BugFablesSave.Sections.Library;

namespace BugFablesSaveEditor.ViewModels;

public class LibraryViewModel : ViewModelBase
{
  private LibraryFlag[] _discoveries;

  private DataGridCollectionView _discoveriesFiltered;
  private LibraryFlag[] _enemies;
  private DataGridCollectionView _enemiesFiltered;

  private bool _filterUnusedDiscoveries;
  private bool _filterUnusedEnemiess;
  private bool _filterUnusedRecipes;
  private bool _filterUnusedRecords;
  private bool _filterUnusedSeenAreas;
  private LibraryFlag[] _recipes;
  private DataGridCollectionView _recipesFiltered;
  private LibraryFlag[] _records;
  private DataGridCollectionView _recordsFiltered;
  private SaveData _saveData;
  private LibraryFlag[] _seenAreas;
  private DataGridCollectionView _seenAreasFiltered;

  private string _textFilterDiscoveries;
  private string _textFilterEnemies;
  private string _textFilterRecipes;
  private string _textFilterRecords;
  private string _textFilterSeenAreas;
  private string[] areasNames;

  private string[] discoveriesNames;
  private string[] enemiesNames;
  private string[] recipessNames;
  private string[] recordsNames;

  public LibraryViewModel()
  {
    SaveData = new SaveData();
    Initialise();
  }

  public LibraryViewModel(SaveData saveData)
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

  public LibraryFlag[] Discoveries
  {
    get => _discoveries;
    set
    {
      _discoveries = value;
      this.RaisePropertyChanged();
    }
  }

  public LibraryFlag[] Enemies
  {
    get => _enemies;
    set
    {
      _enemies = value;
      this.RaisePropertyChanged();
    }
  }

  public LibraryFlag[] Recipes
  {
    get => _recipes;
    set
    {
      _recipes = value;
      this.RaisePropertyChanged();
    }
  }

  public LibraryFlag[] Records
  {
    get => _records;
    set
    {
      _records = value;
      this.RaisePropertyChanged();
    }
  }

  public LibraryFlag[] SeenAreas
  {
    get => _seenAreas;
    set
    {
      _seenAreas = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView DiscoveriesFiltered
  {
    get => _discoveriesFiltered;
    set
    {
      _discoveriesFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView EnemiesFiltered
  {
    get => _enemiesFiltered;
    set
    {
      _enemiesFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView RecipesFiltered
  {
    get => _recipesFiltered;
    set
    {
      _recipesFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView RecordsFiltered
  {
    get => _recordsFiltered;
    set
    {
      _recordsFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView SeenAreasFiltered
  {
    get => _seenAreasFiltered;
    set
    {
      _seenAreasFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public string TextFilterDiscoveries
  {
    get => _textFilterDiscoveries;
    set
    {
      _textFilterDiscoveries = value;
      this.RaisePropertyChanged();
      DiscoveriesFiltered.Refresh();
    }
  }

  public string TextFilterEnemies
  {
    get => _textFilterEnemies;
    set
    {
      _textFilterEnemies = value;
      this.RaisePropertyChanged();
      EnemiesFiltered.Refresh();
    }
  }

  public string TextFilterRecipes
  {
    get => _textFilterRecipes;
    set
    {
      _textFilterRecipes = value;
      this.RaisePropertyChanged();
      RecipesFiltered.Refresh();
    }
  }

  public string TextFilterRecords
  {
    get => _textFilterRecords;
    set
    {
      _textFilterRecords = value;
      this.RaisePropertyChanged();
      RecordsFiltered.Refresh();
    }
  }

  public string TextFilterSeenAreas
  {
    get => _textFilterSeenAreas;
    set
    {
      _textFilterSeenAreas = value;
      this.RaisePropertyChanged();
      SeenAreasFiltered.Refresh();
    }
  }

  public bool FilterUnusedDiscoveries
  {
    get => _filterUnusedDiscoveries;
    set
    {
      _filterUnusedDiscoveries = value;
      this.RaisePropertyChanged();
      DiscoveriesFiltered.Refresh();
    }
  }

  public bool FilterUnusedEnemiess
  {
    get => _filterUnusedEnemiess;
    set
    {
      _filterUnusedEnemiess = value;
      this.RaisePropertyChanged();
      EnemiesFiltered.Refresh();
    }
  }

  public bool FilterUnusedRecipes
  {
    get => _filterUnusedRecipes;
    set
    {
      _filterUnusedRecipes = value;
      this.RaisePropertyChanged();
      RecipesFiltered.Refresh();
    }
  }

  public bool FilterUnusedRecords
  {
    get => _filterUnusedRecords;
    set
    {
      _filterUnusedRecords = value;
      this.RaisePropertyChanged();
      RecordsFiltered.Refresh();
    }
  }

  public bool FilterUnusedSeenAreas
  {
    get => _filterUnusedSeenAreas;
    set
    {
      _filterUnusedSeenAreas = value;
      this.RaisePropertyChanged();
      SeenAreasFiltered.Refresh();
    }
  }

  private void Initialise()
  {
    discoveriesNames = Common.GetEnumDescriptions<Discovery>();
    enemiesNames = Common.GetEnumDescriptions<Enemy>();
    recipessNames = Common.GetEnumDescriptions<Recipe>();
    recordsNames = Common.GetEnumDescriptions<Record>();
    areasNames = Common.GetEnumDescriptions<Area>();

    LibraryFlag[][] wholeLibrary = (LibraryFlag[][])SaveData.Sections[SaveFileSection.Library].Data;
    Discoveries = wholeLibrary[(int)LibrarySection.Discovery];
    Enemies = wholeLibrary[(int)LibrarySection.Bestiary];
    Recipes = wholeLibrary[(int)LibrarySection.Recipe];
    Records = wholeLibrary[(int)LibrarySection.Record];
    SeenAreas = wholeLibrary[(int)LibrarySection.SeenMapLocation];

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
        filterUnused = FilterUnusedEnemiess;
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
    {
      return false;
    }

    if (string.IsNullOrEmpty(textFilter))
    {
      return true;
    }

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

  public void ToggleAllFilteredDiscoveries()
  {
    ToggleAllShownLibrary(LibrarySection.Discovery);
  }

  public void ToggleAllFilteredEnemies()
  {
    ToggleAllShownLibrary(LibrarySection.Bestiary);
  }

  public void ToggleAllFilteredRecipes()
  {
    ToggleAllShownLibrary(LibrarySection.Recipe);
  }

  public void ToggleAllFilteredRecords()
  {
    ToggleAllShownLibrary(LibrarySection.Record);
  }

  public void ToggleAllFilteredSeenAreas()
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
