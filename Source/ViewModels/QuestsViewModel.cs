using System.Collections.ObjectModel;
using System.Reactive;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using static BugFablesSaveEditor.BugFablesSave.Sections.Quests;

namespace BugFablesSaveEditor.ViewModels;

public class QuestsViewModel : ViewModelBase
{
  private ObservableCollection<QuestInfo> _completedQuests;

  private ObservableCollection<QuestInfo> _openQuests;

  private string[] _questNames;
  private SaveData _saveData;

  private QuestInfo _selectedCompletedQuest;
  private Quest _selectedCompletedQuestForAdd;

  private QuestInfo _selectedOpenQuest;

  private Quest _selectedOpenQuestForAdd;

  private QuestInfo _selectedTakenQuest;
  private Quest _selectedTakenQuestForAdd;
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

  public SaveData SaveData
  {
    get => _saveData;
    set
    {
      _saveData = value;
      this.RaisePropertyChanged();
    }
  }

  public string[] QuestsNames
  {
    get => _questNames;
    set
    {
      _questNames = value;
      this.RaisePropertyChanged();
    }
  }

  public Quest SelectedOpenQuestForAdd
  {
    get => _selectedOpenQuestForAdd;
    set
    {
      _selectedOpenQuestForAdd = value;
      this.RaisePropertyChanged();
    }
  }

  public Quest SelectedTakenQuestForAdd
  {
    get => _selectedTakenQuestForAdd;
    set
    {
      _selectedTakenQuestForAdd = value;
      this.RaisePropertyChanged();
    }
  }

  public Quest SelectedCompletedQuestForAdd
  {
    get => _selectedCompletedQuestForAdd;
    set
    {
      _selectedCompletedQuestForAdd = value;
      this.RaisePropertyChanged();
    }
  }

  public QuestInfo SelectedOpenQuest
  {
    get => _selectedOpenQuest;
    set
    {
      _selectedOpenQuest = value;
      this.RaisePropertyChanged();
    }
  }

  public QuestInfo SelectedTakenQuest
  {
    get => _selectedTakenQuest;
    set
    {
      _selectedTakenQuest = value;
      this.RaisePropertyChanged();
    }
  }

  public QuestInfo SelectedCompletedQuest
  {
    get => _selectedCompletedQuest;
    set
    {
      _selectedCompletedQuest = value;
      this.RaisePropertyChanged();
    }
  }

  public ObservableCollection<QuestInfo> OpenQuests
  {
    get => _openQuests;
    set
    {
      _openQuests = value;
      this.RaisePropertyChanged();
    }
  }

  public ObservableCollection<QuestInfo> TakenQuests
  {
    get => _takenQuests;
    set
    {
      _takenQuests = value;
      this.RaisePropertyChanged();
    }
  }

  public ObservableCollection<QuestInfo> CompletedQuests
  {
    get => _completedQuests;
    set
    {
      _completedQuests = value;
      this.RaisePropertyChanged();
    }
  }

  public ReactiveCommand<Unit, Unit> CmdReorderOpenQuestUp { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderOpenQuestDown { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderTakenQuestUp { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderTakenQuestDown { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderCompletedQuestUp { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderCompletedQuestDown { get; set; }

  private void Initialize()
  {
    QuestsNames = Common.GetEnumDescriptions<Quest>();
    ObservableCollection<QuestInfo>[] questsArray =
      (ObservableCollection<QuestInfo>[])SaveData.Sections[SaveFileSection.Quests].Data;
    OpenQuests = questsArray[(int)QuestState.Open];
    TakenQuests = questsArray[(int)QuestState.Taken];
    CompletedQuests = questsArray[(int)QuestState.Completed];

    CmdReorderOpenQuestUp = ReactiveCommand.Create(() =>
    {
      ReorderQuest(QuestState.Open, ReorderDirection.Up);
    }, this.WhenAnyValue(x => x.SelectedOpenQuest, x => x != null && OpenQuests[0] != x));
    CmdReorderOpenQuestDown = ReactiveCommand.Create(() =>
    {
      ReorderQuest(QuestState.Open, ReorderDirection.Down);
    }, this.WhenAnyValue(x => x.SelectedOpenQuest, x => x != null && OpenQuests[OpenQuests.Count - 1] != x));

    CmdReorderTakenQuestUp = ReactiveCommand.Create(() =>
    {
      ReorderQuest(QuestState.Taken, ReorderDirection.Up);
    }, this.WhenAnyValue(x => x.SelectedTakenQuest, x => x != null && TakenQuests[0] != x));
    CmdReorderTakenQuestDown = ReactiveCommand.Create(() =>
    {
      ReorderQuest(QuestState.Taken, ReorderDirection.Down);
    }, this.WhenAnyValue(x => x.SelectedTakenQuest, x => x != null && TakenQuests[TakenQuests.Count - 1] != x));

    CmdReorderCompletedQuestUp = ReactiveCommand.Create(() =>
    {
      ReorderQuest(QuestState.Completed, ReorderDirection.Up);
    }, this.WhenAnyValue(x => x.SelectedCompletedQuest, x => x != null && CompletedQuests[0] != x));
    CmdReorderCompletedQuestDown = ReactiveCommand.Create(() =>
      {
        ReorderQuest(QuestState.Completed, ReorderDirection.Down);
      },
      this.WhenAnyValue(x => x.SelectedCompletedQuest,
        x => x != null && CompletedQuests[CompletedQuests.Count - 1] != x));
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

  public void RemoveOpenQuest(QuestInfo questInfo)
  {
    OpenQuests.Remove(questInfo);
  }

  public void RemoveTakenQuest(QuestInfo questInfo)
  {
    TakenQuests.Remove(questInfo);
  }

  public void RemoveCompletedQuest(QuestInfo questInfo)
  {
    CompletedQuests.Remove(questInfo);
  }

  public void AddOpenQuest()
  {
    OpenQuests.Add(new QuestInfo { Quest = SelectedOpenQuestForAdd });
  }

  public void AddTakenQuest()
  {
    TakenQuests.Add(new QuestInfo { Quest = SelectedTakenQuestForAdd });
  }

  public void AddCompletedQuest()
  {
    CompletedQuests.Add(new QuestInfo { Quest = SelectedCompletedQuestForAdd });
  }
}
