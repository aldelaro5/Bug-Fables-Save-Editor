using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class QuestsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableBoardQuestsSaveData _questsSaveData;

  public QuestsViewModel() : this(new(new())) { }

  public QuestsViewModel(ObservableBoardQuestsSaveData questsSaveData)
  {
    _questsSaveData = questsSaveData;
  }
}
