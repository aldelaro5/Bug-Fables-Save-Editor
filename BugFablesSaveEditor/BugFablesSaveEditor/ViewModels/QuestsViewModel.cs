using BugFablesLib.Data;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class QuestsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableBoardQuestsSaveData _questsSaveData;

  [ObservableProperty]
  private ObservableBfNamedId _newOpenQuest = new(new BfQuest());

  [ObservableProperty]
  private ObservableBfNamedId _newTakenQuest = new(new BfQuest());

  [ObservableProperty]
  private ObservableBfNamedId _newCompletedQuest = new(new BfQuest());

  public QuestsViewModel()
  {
    _questsSaveData = new(new());
  }

  public QuestsViewModel(ObservableBoardQuestsSaveData questsSaveData)
  {
    _questsSaveData = questsSaveData;
  }

  [RelayCommand]
  private void AddOpenQuest(ObservableBfNamedId quest) =>
    QuestsSaveData.Opened.Add(new(quest.ToQuest()));

  [RelayCommand]
  private void DeleteOpenQuest(ObservableBfNamedId quest) => QuestsSaveData.Opened.Remove(quest);

  [RelayCommand]
  private void AddTakenQuest(ObservableBfNamedId quest) =>
    QuestsSaveData.Taken.Add(new(quest.ToQuest()));

  [RelayCommand]
  private void DeleteTakenQuest(ObservableBfNamedId quest) => QuestsSaveData.Taken.Remove(quest);

  [RelayCommand]
  private void AddCompletedQuest(ObservableBfNamedId quest) =>
    QuestsSaveData.Completed.Add(new(quest.ToQuest()));

  [RelayCommand]
  private void DeleteCompletedQuest(ObservableBfNamedId quest) =>
    QuestsSaveData.Completed.Remove(quest);
}
