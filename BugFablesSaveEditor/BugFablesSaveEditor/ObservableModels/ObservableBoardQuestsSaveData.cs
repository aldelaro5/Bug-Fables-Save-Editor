using System.Linq;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

[ObservableObject]
public partial class ObservableBoardQuestsSaveData : ObservableModel
{
  public sealed override BoardQuestsSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfCollection<BfQuest, ObservableBfResource> _opened;

  [ObservableProperty]
  private ObservableBfCollection<BfQuest, ObservableBfResource> _taken;

  [ObservableProperty]
  private ObservableBfCollection<BfQuest, ObservableBfResource> _completed;

  public ObservableBoardQuestsSaveData(BoardQuestsSaveData boardQuestsSaveData) :
    base(boardQuestsSaveData)
  {
    UnderlyingData = boardQuestsSaveData;
    _opened = new(UnderlyingData.Opened,
      list => list.Select(x => new ObservableBfResource(x)).ToList());
    _taken = new(UnderlyingData.Taken,
      list => list.Select(x => new ObservableBfResource(x)).ToList());
    _completed = new(UnderlyingData.Completed,
      list => list.Select(x => new ObservableBfResource(x)).ToList());
  }
}
