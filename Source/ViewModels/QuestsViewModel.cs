using BugFablesDataLib;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesDataLib.Sections.Quests;

namespace BugFablesSaveEditor.ViewModels;

public partial class QuestsViewModel : ObservableObject
{
  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private ReorderableCollectionViewModel<QuestInfo> _openQuestsVm;

  [ObservableProperty]
  private ReorderableCollectionViewModel<QuestInfo> _takenQuestsVm;

  [ObservableProperty]
  private ReorderableCollectionViewModel<QuestInfo> _completedQuestsVm;

  [ObservableProperty]
  private string[] _questsNames = Utils.GetEnumDescriptions<Quest>();

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
    _saveData = saveData;

    _openQuestsVm = new(_saveData.Quests.Opened);
    _takenQuestsVm = new(_saveData.Quests.Taken);
    _completedQuestsVm = new(_saveData.Quests.Completed);
  }

  [RelayCommand]
  private void AddOpenQuest()
  {
    QuestInfo info = new();
    info.Quest = SelectedOpenQuestForAdd;
    OpenQuestsVm.Collection.Add(info);
    OpenQuestsVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddTakenQuest()
  {
    QuestInfo info = new();
    info.Quest = SelectedTakenQuestForAdd;
    TakenQuestsVm.Collection.Add(info);
    TakenQuestsVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddCompletedQuest()
  {
    QuestInfo info = new();
    info.Quest = SelectedCompletedQuestForAdd;
    CompletedQuestsVm.Collection.Add(info);
    CompletedQuestsVm.CollectionView.Refresh();
  }
}
