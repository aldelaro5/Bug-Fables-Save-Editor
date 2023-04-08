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
    _discoveries = new(Model.Discoveries);
    _bestiary = new(Model.Bestiary);
    _recipes = new(Model.Recipes);
    _records = new(Model.Records);
    _seenAreas = new(Model.SeenAreas);
  }
}
