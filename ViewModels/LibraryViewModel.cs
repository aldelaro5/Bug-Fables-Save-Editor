using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
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
      var wholeLibrary = (LibraryFlag[][])SaveData.Sections[SaveFileSection.Library].Data;
      Discoveries = wholeLibrary[(int)LibrarySection.Discovery];
      Enemies = wholeLibrary[(int)LibrarySection.Bestiary];
      Recipes = wholeLibrary[(int)LibrarySection.Recipe];
      Records = wholeLibrary[(int)LibrarySection.Record];
      SeenAreas = wholeLibrary[(int)LibrarySection.SeenMapLocation];
    }
  }
}
