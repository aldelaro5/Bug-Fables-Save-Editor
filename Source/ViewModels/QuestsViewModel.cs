using System.Collections.Generic;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Quests;

namespace BugFablesSaveEditor.ViewModels;

public partial class QuestsViewModel : ObservableObject
{
  [ObservableProperty]
  private ReorderableCollectionViewModel<QuestInfo> _openQuestsVm = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<QuestInfo> _takenQuestsVm = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<QuestInfo> _completedQuestsVm = null!;

  [ObservableProperty]
  private string[] _questsNames = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  private Quest _selectedCompletedQuestForAdd;

  [ObservableProperty]
  private Quest _selectedOpenQuestForAdd;

  [ObservableProperty]
  private Quest _selectedTakenQuestForAdd;

  public QuestsViewModel() : this(new SaveData())
  {
    OpenQuestsVm.Collection.Add(new QuestInfo { Quest = (Quest)51 });
    OpenQuestsVm.Collection.Add(new QuestInfo { Quest = (Quest)27 });
    OpenQuestsVm.Collection.Add(new QuestInfo { Quest = (Quest)16 });

    TakenQuestsVm.Collection.Add(new QuestInfo { Quest = (Quest)62 });
    TakenQuestsVm.Collection.Add(new QuestInfo { Quest = (Quest)7 });
    TakenQuestsVm.Collection.Add(new QuestInfo { Quest = (Quest)28 });

    CompletedQuestsVm.Collection.Add(new QuestInfo { Quest = (Quest)12 });
    CompletedQuestsVm.Collection.Add(new QuestInfo { Quest = (Quest)25 });
    CompletedQuestsVm.Collection.Add(new QuestInfo { Quest = (Quest)14 });
  }

  public QuestsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    QuestsNames = Utils.GetEnumDescriptions<Quest>();

    OpenQuestsVm = new ReorderableCollectionViewModel<QuestInfo>(SaveData.Quests.Opened);
    TakenQuestsVm = new ReorderableCollectionViewModel<QuestInfo>(SaveData.Quests.Taken);
    CompletedQuestsVm = new ReorderableCollectionViewModel<QuestInfo>(SaveData.Quests.Completed);
  }

  [RelayCommand]
  private void AddOpenQuest()
  {
    OpenQuestsVm.Collection.Add(new QuestInfo { Quest = SelectedOpenQuestForAdd });
    OpenQuestsVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddTakenQuest()
  {
    TakenQuestsVm.Collection.Add(new QuestInfo { Quest = SelectedTakenQuestForAdd });
    TakenQuestsVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddCompletedQuest()
  {
    CompletedQuestsVm.Collection.Add(new QuestInfo { Quest = SelectedCompletedQuestForAdd });
    CompletedQuestsVm.CollectionView.Refresh();
  }
}
