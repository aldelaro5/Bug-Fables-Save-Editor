using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System;
using System.Text;
using static BugFablesSaveEditor.BugFablesSave.Sections.Library;

namespace BugFablesSaveEditor.ViewModels
{
  public class LibraryViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private string[] discoveriesNames;
    private string[] enemiesNames;
    private string[] recipessNames;
    private string[] recordsNames;
    private string[] areasNames;

    private LibraryFlag[] _discoveries;
    public LibraryFlag[] Discoveries
    {
      get { return _discoveries; }
      set { _discoveries = value; this.RaisePropertyChanged(); }
    }
    private LibraryFlag[] _enemies;
    public LibraryFlag[] Enemies
    {
      get { return _enemies; }
      set { _enemies = value; this.RaisePropertyChanged(); }
    }
    private LibraryFlag[] _recipes;
    public LibraryFlag[] Recipes
    {
      get { return _recipes; }
      set { _recipes = value; this.RaisePropertyChanged(); }
    }
    private LibraryFlag[] _records;
    public LibraryFlag[] Records
    {
      get { return _records; }
      set { _records = value; this.RaisePropertyChanged(); }
    }
    private LibraryFlag[] _seenAreas;
    public LibraryFlag[] SeenAreas
    {
      get { return _seenAreas; }
      set { _seenAreas = value; this.RaisePropertyChanged(); }
    }

    private DataGridCollectionView _discoveriesFiltered;
    public DataGridCollectionView DiscoveriesFiltered
    {
      get { return _discoveriesFiltered; }
      set { _discoveriesFiltered = value; this.RaisePropertyChanged(); }
    }
    private DataGridCollectionView _enemiesFiltered;
    public DataGridCollectionView EnemiesFiltered
    {
      get { return _enemiesFiltered; }
      set { _enemiesFiltered = value; this.RaisePropertyChanged(); }
    }
    private DataGridCollectionView _recipesFiltered;
    public DataGridCollectionView RecipesFiltered
    {
      get { return _recipesFiltered; }
      set { _recipesFiltered = value; this.RaisePropertyChanged(); }
    }
    private DataGridCollectionView _recordsFiltered;
    public DataGridCollectionView RecordsFiltered
    {
      get { return _recordsFiltered; }
      set { _recordsFiltered = value; this.RaisePropertyChanged(); }
    }
    private DataGridCollectionView _seenAreasFiltered;
    public DataGridCollectionView SeenAreasFiltered
    {
      get { return _seenAreasFiltered; }
      set { _seenAreasFiltered = value; this.RaisePropertyChanged(); }
    }

    private string _textFilterDiscoveries;
    public string TextFilterDiscoveries
    {
      get { return _textFilterDiscoveries; }
      set
      {
        _textFilterDiscoveries = value;
        this.RaisePropertyChanged();
        DiscoveriesFiltered.Refresh();
      }
    }
    private string _textFilterEnemies;
    public string TextFilterEnemies
    {
      get { return _textFilterEnemies; }
      set
      {
        _textFilterEnemies = value;
        this.RaisePropertyChanged();
        EnemiesFiltered.Refresh();
      }
    }
    private string _textFilterRecipes;
    public string TextFilterRecipes
    {
      get { return _textFilterRecipes; }
      set
      {
        _textFilterRecipes = value;
        this.RaisePropertyChanged();
        RecipesFiltered.Refresh();
      }
    }
    private string _textFilterRecords;
    public string TextFilterRecords
    {
      get { return _textFilterRecords; }
      set
      {
        _textFilterRecords = value;
        this.RaisePropertyChanged();
        RecordsFiltered.Refresh();
      }
    }
    private string _textFilterSeenAreas;
    public string TextFilterSeenAreas
    {
      get { return _textFilterSeenAreas; }
      set
      {
        _textFilterSeenAreas = value;
        this.RaisePropertyChanged();
        SeenAreasFiltered.Refresh();
      }
    }

    private bool _filterUnusedDiscoveries;
    public bool FilterUnusedDiscoveries
    {
      get { return _filterUnusedDiscoveries; }
      set
      {
        _filterUnusedDiscoveries = value;
        this.RaisePropertyChanged();
        DiscoveriesFiltered.Refresh();
      }
    }
    private bool _filterUnusedEnemiess;
    public bool FilterUnusedEnemiess
    {
      get { return _filterUnusedEnemiess; }
      set
      {
        _filterUnusedEnemiess = value;
        this.RaisePropertyChanged();
        EnemiesFiltered.Refresh();
      }
    }
    private bool _filterUnusedRecipes;
    public bool FilterUnusedRecipes
    {
      get { return _filterUnusedRecipes; }
      set
      {
        _filterUnusedRecipes = value;
        this.RaisePropertyChanged();
        RecipesFiltered.Refresh();
      }
    }
    private bool _filterUnusedRecords;
    public bool FilterUnusedRecords
    {
      get { return _filterUnusedRecords; }
      set
      {
        _filterUnusedRecords = value;
        this.RaisePropertyChanged();
        RecordsFiltered.Refresh();
      }
    }
    private bool _filterUnusedSeenAreas;
    public bool FilterUnusedSeenAreas
    {
      get { return _filterUnusedSeenAreas; }
      set
      {
        _filterUnusedSeenAreas = value;
        this.RaisePropertyChanged();
        SeenAreasFiltered.Refresh();
      }
    }

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

    private void Initialise()
    {
      discoveriesNames = CommonUtils.GetEnumDescriptions<Discovery>();
      enemiesNames = CommonUtils.GetEnumDescriptions<Enemy>();
      recipessNames = CommonUtils.GetEnumDescriptions<Recipe>();
      recordsNames = CommonUtils.GetEnumDescriptions<Record>();
      areasNames = CommonUtils.GetEnumDescriptions<Area>();

      var wholeLibrary = (LibraryFlag[][])SaveData.Sections[SaveFileSection.Library].Data;
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
        return false;
      if (string.IsNullOrEmpty(textFilter))
        return true;

      StringBuilder sb = new StringBuilder();
      sb.Append(flag.Index).Append(' ');
      if (flag.Index >= enumValues.Length)
        sb.Append("UNUSED " + flag.Index);
      else
        sb.Append(enumValues[flag.Index]);
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

      foreach (var item in dg)
      {
        LibraryFlag flag = (LibraryFlag)item;
        if (flag.Enabled)
        {
          newEnabled = false;
          break;
        }
      }

      foreach (var item in dg)
      {
        LibraryFlag flag = (LibraryFlag)item;
        flag.Enabled = newEnabled;
      }
    }
  }
}
