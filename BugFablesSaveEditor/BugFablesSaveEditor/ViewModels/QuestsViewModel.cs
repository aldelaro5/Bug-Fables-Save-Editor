using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class QuestsViewModel : ObservableObject
{
  [ObservableProperty]
  private ViewModelCollection<BfQuest, ObservableBfNamedId> _opened;

  [ObservableProperty]
  private ViewModelCollection<BfQuest, ObservableBfNamedId> _taken;

  [ObservableProperty]
  private ViewModelCollection<BfQuest, ObservableBfNamedId> _completed;


  public QuestsViewModel() : this(new()) { }

  public QuestsViewModel(BoardQuestsSaveData questsSaveData)
  {
    _opened = new(questsSaveData.Opened);
    _taken = new(questsSaveData.Taken);
    _completed = new(questsSaveData.Completed);
  }
}
