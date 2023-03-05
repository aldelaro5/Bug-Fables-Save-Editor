using System.Collections.ObjectModel;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Quests;

namespace BugFablesSaveEditor.ViewModels;

public partial class QuestsViewModel : ViewModelBase
{
  [ObservableProperty]
  private ObservableCollection<QuestInfo> _completedQuests;

  [ObservableProperty]
  private ObservableCollection<QuestInfo> _openQuests;

  [ObservableProperty]
  private string[] _questsNames;
  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderCompletedQuestUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderCompletedQuestDownCommand))]
  private QuestInfo? _selectedCompletedQuest;
  [ObservableProperty]
  private Quest _selectedCompletedQuestForAdd;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderOpenQuestUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderOpenQuestDownCommand))]
  private QuestInfo? _selectedOpenQuest;

  [ObservableProperty]
  private Quest _selectedOpenQuestForAdd;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderTakenQuestUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderTakenQuestDownCommand))]
  private QuestInfo? _selectedTakenQuest;
  [ObservableProperty]
  private Quest _selectedTakenQuestForAdd;
  [ObservableProperty]
  private ObservableCollection<QuestInfo> _takenQuests;

  public QuestsViewModel()
  {
    SaveData = new SaveData();
    Initialize();

    OpenQuests.Add(new QuestInfo { Quest = (Quest)51 });
    OpenQuests.Add(new QuestInfo { Quest = (Quest)27 });
    OpenQuests.Add(new QuestInfo { Quest = (Quest)16 });

    TakenQuests.Add(new QuestInfo { Quest = (Quest)62 });
    TakenQuests.Add(new QuestInfo { Quest = (Quest)7 });
    TakenQuests.Add(new QuestInfo { Quest = (Quest)28 });

    CompletedQuests.Add(new QuestInfo { Quest = (Quest)12 });
    CompletedQuests.Add(new QuestInfo { Quest = (Quest)25 });
    CompletedQuests.Add(new QuestInfo { Quest = (Quest)14 });
  }

  public QuestsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Initialize();
  }

  [RelayCommand(CanExecute = nameof(CanReorderOpenQuestUp))]
  private void CmdReorderOpenQuestUp()
  {
    ReorderQuest(QuestState.Open, ReorderDirection.Up);
  }
  private bool CanReorderOpenQuestUp()
  {
    return OpenQuests.Count > 0 && SelectedOpenQuest is not null && OpenQuests[0] != SelectedOpenQuest;
  }

  [RelayCommand(CanExecute = nameof(CanReorderOpenQuestDown))]
  private void CmdReorderOpenQuestDown()
  {
    ReorderQuest(QuestState.Open, ReorderDirection.Down);
  }
  private bool CanReorderOpenQuestDown()
  {
    return OpenQuests.Count > 0 && SelectedOpenQuest is not null && OpenQuests[^1] != SelectedOpenQuest;
  }

  [RelayCommand(CanExecute = nameof(CanReorderTakenQuestUp))]
  private void CmdReorderTakenQuestUp()
  {
    ReorderQuest(QuestState.Taken, ReorderDirection.Up);
  }
  private bool CanReorderTakenQuestUp()
  {
    return TakenQuests.Count > 0 && SelectedTakenQuest is not null && TakenQuests[0] != SelectedTakenQuest;
  }

  [RelayCommand(CanExecute = nameof(CanReorderTakenQuestDown))]
  private void CmdReorderTakenQuestDown()
  {
    ReorderQuest(QuestState.Taken, ReorderDirection.Down);
  }
  private bool CanReorderTakenQuestDown()
  {
    return TakenQuests.Count > 0 && SelectedTakenQuest is not null && TakenQuests[^1] != SelectedTakenQuest;
  }

  [RelayCommand(CanExecute = nameof(CanReorderCompletedQuestUp))]
  private void CmdReorderCompletedQuestUp()
  {
    ReorderQuest(QuestState.Completed, ReorderDirection.Up);
  }
  private bool CanReorderCompletedQuestUp()
  {
    return CompletedQuests.Count > 0 && SelectedCompletedQuest is not null && CompletedQuests[0] != SelectedCompletedQuest;
  }

  [RelayCommand(CanExecute = nameof(CanReorderCompletedQuestDown))]
  private void CmdReorderCompletedQuestDown()
  {
    ReorderQuest(QuestState.Completed, ReorderDirection.Down);
  }
  private bool CanReorderCompletedQuestDown()
  {
    return CompletedQuests.Count > 0 && SelectedCompletedQuest is not null && CompletedQuests[^1] != SelectedCompletedQuest;
  }

  private void Initialize()
  {
    QuestsNames = Common.GetEnumDescriptions<Quest>();
    ObservableCollection<QuestInfo>[] questsArray =
      (ObservableCollection<QuestInfo>[])SaveData.Sections[SaveFileSection.Quests].Data;
    OpenQuests = questsArray[(int)QuestState.Open];
    TakenQuests = questsArray[(int)QuestState.Taken];
    CompletedQuests = questsArray[(int)QuestState.Completed];
  }

  private void ReorderQuest(QuestState questState, ReorderDirection direction)
  {
    QuestInfo selectedQuest;
    ObservableCollection<QuestInfo> questsCollection;
    switch (questState)
    {
      case QuestState.Open:
        selectedQuest = SelectedOpenQuest;
        questsCollection = OpenQuests;
        break;
      case QuestState.Taken:
        selectedQuest = SelectedTakenQuest;
        questsCollection = TakenQuests;
        break;
      case QuestState.Completed:
        selectedQuest = SelectedCompletedQuest;
        questsCollection = CompletedQuests;
        break;
      default:
        return;
    }

    int index = questsCollection.IndexOf(selectedQuest);
    int newIndex = index;
    if (direction == ReorderDirection.Up)
    {
      newIndex--;
    }
    else if (direction == ReorderDirection.Down)
    {
      newIndex++;
    }

    Quest quest = selectedQuest.Quest;
    questsCollection.Remove(selectedQuest);
    questsCollection.Insert(newIndex, new QuestInfo { Quest = quest });

    switch (questState)
    {
      case QuestState.Open:
        SelectedOpenQuest = OpenQuests[newIndex];
        break;
      case QuestState.Taken:
        SelectedTakenQuest = TakenQuests[newIndex];
        break;
      case QuestState.Completed:
        SelectedCompletedQuest = CompletedQuests[newIndex];
        break;
    }
  }

  [RelayCommand]
  private void RemoveOpenQuest(QuestInfo questInfo)
  {
    OpenQuests.Remove(questInfo);
  }

  [RelayCommand]
  private void RemoveTakenQuest(QuestInfo questInfo)
  {
    TakenQuests.Remove(questInfo);
  }

  [RelayCommand]
  private void RemoveCompletedQuest(QuestInfo questInfo)
  {
    CompletedQuests.Remove(questInfo);
  }

  [RelayCommand]
  private void AddOpenQuest()
  {
    OpenQuests.Add(new QuestInfo { Quest = SelectedOpenQuestForAdd });
  }

  [RelayCommand]
  private void AddTakenQuest()
  {
    TakenQuests.Add(new QuestInfo { Quest = SelectedTakenQuestForAdd });
  }

  [RelayCommand]
  private void AddCompletedQuest()
  {
    CompletedQuests.Add(new QuestInfo { Quest = SelectedCompletedQuestForAdd });
  }
}
