using BugFablesLib.Data;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class QuestsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableBoardQuestsSaveData _questsSaveData;

  public QuestsViewModel()
  {
    _questsSaveData = new(new());
  }

  public QuestsViewModel(ObservableBoardQuestsSaveData questsSaveData)
  {
    _questsSaveData = questsSaveData;
  }
}
