using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class MainViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableBoardQuestsSaveData _test;

  [ObservableProperty]
  private int _index;

  [RelayCommand]
  private void Remove(int i)
  {
    Test.Opened.Insert(i, new ObservableBfResource(new BfQuest() {Id = 55}));
  }

  public MainViewModel()
  {
    BoardQuestsSaveData? data = new();
    data[0] = new() {new BfQuest {Id = 95}, new BfQuest() { Id = 20}};
    data[1] = new() {new BfQuest {Id = 20}, new BfQuest() { Id = 10}, new BfQuest() { Id = 5}};
    data[2] = new() {new BfQuest {Id = 62}};
    _test = new ObservableBoardQuestsSaveData(data);
  }
}
