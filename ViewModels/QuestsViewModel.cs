using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using static BugFablesSaveEditor.BugFablesSave.Sections.Quests;

namespace BugFablesSaveEditor.ViewModels
{
  public class QuestsViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private string[] _questNames;
    public string[] QuestsNames
    {
      get { return _questNames; }
      set { _questNames = value; this.RaisePropertyChanged(); }
    }

    private Quest _selectedOpenQuestForAdd;
    public Quest SelectedOpenQuestForAdd
    {
      get { return _selectedOpenQuestForAdd; }
      set { _selectedOpenQuestForAdd = value; this.RaisePropertyChanged(); }
    }
    private Quest _selectedTakenQuestForAdd;
    public Quest SelectedTakenQuestForAdd
    {
      get { return _selectedTakenQuestForAdd; }
      set { _selectedTakenQuestForAdd = value; this.RaisePropertyChanged(); }
    }
    private Quest _selectedCompletedQuestForAdd;
    public Quest SelectedCompletedQuestForAdd
    {
      get { return _selectedCompletedQuestForAdd; }
      set { _selectedCompletedQuestForAdd = value; this.RaisePropertyChanged(); }
    }

    private QuestInfo _selectedOpenQuest;
    public QuestInfo SelectedOpenQuest
    {
      get { return _selectedOpenQuest; }
      set { _selectedOpenQuest = value; this.RaisePropertyChanged(); }
    }

    private QuestInfo _selectedTakenQuest;
    public QuestInfo SelectedTakenQuest
    {
      get { return _selectedTakenQuest; }
      set { _selectedTakenQuest = value; this.RaisePropertyChanged(); }
    }

    private QuestInfo _selectedCompletedQuest;
    public QuestInfo SelectedCompletedQuest
    {
      get { return _selectedCompletedQuest; }
      set { _selectedCompletedQuest = value; this.RaisePropertyChanged(); }
    }

    private ObservableCollection<QuestInfo> _openQuests;
    public ObservableCollection<QuestInfo> OpenQuests
    {
      get { return _openQuests; }
      set { _openQuests = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<QuestInfo> _takenQuests;
    public ObservableCollection<QuestInfo> TakenQuests
    {
      get { return _takenQuests; }
      set { _takenQuests = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<QuestInfo> _completedQuests;
    public ObservableCollection<QuestInfo> CompletedQuests
    {
      get { return _completedQuests; }
      set { _completedQuests = value; this.RaisePropertyChanged(); }
    }

    public ReactiveCommand<Unit, Unit> CmdReorderOpenQuestUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderOpenQuestDown { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderTakenQuestUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderTakenQuestDown { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderCompletedQuestUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderCompletedQuestDown { get; set; }

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

    private void Initialize()
    {
      QuestsNames = Common.GetEnumDescriptions<Quest>();
      var questsArray = (ObservableCollection<QuestInfo>[])SaveData.Sections[SaveFileSection.Quests].Data;
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
      }, this.WhenAnyValue(x => x.SelectedCompletedQuest, x => x != null && CompletedQuests[CompletedQuests.Count - 1] != x));
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
        newIndex--;
      else if (direction == ReorderDirection.Down)
        newIndex++;
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
}
