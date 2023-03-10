using System.Collections.Generic;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Utils;
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
    QuestsNames = Common.GetEnumDescriptions<Quest>();
    var quests =
      (IEnumerable<QuestInfo>[])SaveData.Sections[SaveFileSection.Quests].Data;

    OpenQuestsVm = new ReorderableCollectionViewModel<QuestInfo>(quests[(int)QuestState.Open]);
    TakenQuestsVm = new ReorderableCollectionViewModel<QuestInfo>(quests[(int)QuestState.Taken]);
    CompletedQuestsVm =
      new ReorderableCollectionViewModel<QuestInfo>(quests[(int)QuestState.Completed]);
  }

  [RelayCommand]
  private void AddOpenQuest()
  {
    OpenQuestsVm.Collection.Add(new QuestInfo { Quest = SelectedOpenQuestForAdd });
  }

  [RelayCommand]
  private void AddTakenQuest()
  {
    TakenQuestsVm.Collection.Add(new QuestInfo { Quest = SelectedTakenQuestForAdd });
  }

  [RelayCommand]
  private void AddCompletedQuest()
  {
    CompletedQuestsVm.Collection.Add(new QuestInfo { Quest = SelectedCompletedQuestForAdd });
  }
}
