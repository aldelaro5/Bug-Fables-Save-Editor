using System;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Core.ViewModels;

public partial class QuestsViewModel : ObservableObject, IDisposable
{
  [ObservableProperty]
  private ViewModelCollection<BfQuest, BfNamedIdModel> _opened;

  [ObservableProperty]
  private ViewModelCollection<BfQuest, BfNamedIdModel> _taken;

  [ObservableProperty]
  private ViewModelCollection<BfQuest, BfNamedIdModel> _completed;

  public QuestsViewModel() : this(new()) { }

  public QuestsViewModel(BoardQuestsSaveData questsSaveData)
  {
    _opened = new(questsSaveData.Opened);
    _taken = new(questsSaveData.Taken);
    _completed = new(questsSaveData.Completed);
  }

  public void Dispose()
  {
    Opened.Dispose();
    Taken.Dispose();
    Completed.Dispose();
  }
}
