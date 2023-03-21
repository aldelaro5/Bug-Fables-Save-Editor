using System.Linq;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableLibrarySaveData : BfObservable
{
  public sealed override LibrarySaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> _discoveries;
  [ObservableProperty]
  private ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> _bestiary;
  [ObservableProperty]
  private ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> _recipes;
  [ObservableProperty]
  private ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> _records;
  [ObservableProperty]
  private ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> _seenAreas;

  public ObservableLibrarySaveData(LibrarySaveData librarySaveData) :
    base(librarySaveData)
  {
    UnderlyingData = librarySaveData;
    _discoveries = new(UnderlyingData.Discoveries,
      flags => flags.Select(x => new ObservableFlagSaveData(x)).ToList());
    _bestiary = new(UnderlyingData.Bestiary,
      flags => flags.Select(x => new ObservableFlagSaveData(x)).ToList());
    _recipes = new(UnderlyingData.Recipes,
      flags => flags.Select(x => new ObservableFlagSaveData(x)).ToList());
    _records = new(UnderlyingData.Records,
      flags => flags.Select(x => new ObservableFlagSaveData(x)).ToList());
    _seenAreas = new(UnderlyingData.SeenAreas,
      flags => flags.Select(x => new ObservableFlagSaveData(x)).ToList());
  }
}
