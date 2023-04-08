using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableBoardQuestsSaveData : ObservableObject
{
  public BoardQuestsSaveData Model { get; }

  [ObservableProperty]
  private ViewModelCollection<BfQuest, ObservableBfNamedId> _opened;

  [ObservableProperty]
  private ViewModelCollection<BfQuest, ObservableBfNamedId> _taken;

  [ObservableProperty]
  private ViewModelCollection<BfQuest, ObservableBfNamedId> _completed;

  public ObservableBoardQuestsSaveData(BoardQuestsSaveData boardQuestsSaveData)
  {
    Model = boardQuestsSaveData;
    _opened = new(Model.Opened);
    _taken = new(Model.Taken);
    _completed = new(Model.Completed);
  }
}
