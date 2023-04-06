using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableLibrarySaveData : ObservableObject
{
  public LibrarySaveData Model { get; }

  [ObservableProperty]
  private ViewModelCollection<FlagSaveData, ObservableFlagSaveData> _discoveries;

  [ObservableProperty]
  private ViewModelCollection<FlagSaveData, ObservableFlagSaveData> _bestiary;

  [ObservableProperty]
  private ViewModelCollection<FlagSaveData, ObservableFlagSaveData> _recipes;

  [ObservableProperty]
  private ViewModelCollection<FlagSaveData, ObservableFlagSaveData> _records;

  [ObservableProperty]
  private ViewModelCollection<FlagSaveData, ObservableFlagSaveData> _seenAreas;

  public ObservableLibrarySaveData(LibrarySaveData librarySaveData)
  {
    Model = librarySaveData;
    _discoveries = new(Model.Discoveries, x => new ObservableFlagSaveData(x));
    _bestiary = new(Model.Bestiary, x => new ObservableFlagSaveData(x));
    _recipes = new(Model.Recipes, x => new ObservableFlagSaveData(x));
    _records = new(Model.Records, x => new ObservableFlagSaveData(x));
    _seenAreas = new(Model.SeenAreas, x => new ObservableFlagSaveData(x));
  }
}
