using System;
using System.Collections.Specialized;
using System.Linq;
using BugFablesLib;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class ObservableBoardQuestsSaveData : ObservableObject, IDisposable
{
  private readonly BoardQuestsSaveData _boardQuestsSaveData;

  [ObservableProperty]
  private TrackedObservableCollection<ObservableBfResource> _opened;
  [ObservableProperty]
  private TrackedObservableCollection<ObservableBfResource> _taken;
  [ObservableProperty]
  private TrackedObservableCollection<ObservableBfResource> _completed;

  public ObservableBoardQuestsSaveData(BoardQuestsSaveData boardQuestsSaveData)
  {
    _boardQuestsSaveData = boardQuestsSaveData;
    _opened = new TrackedObservableCollection<ObservableBfResource>(_boardQuestsSaveData.Opened
      .Select(x => new ObservableBfResource(x)).ToList());
    _opened.CollectionChanged += OpenedOnCollectionChanged;

    _taken = new TrackedObservableCollection<ObservableBfResource>(_boardQuestsSaveData.Taken
      .Select(x => new ObservableBfResource(x)).ToList());
    _taken.CollectionChanged += TakenOnCollectionChanged;

    _completed = new TrackedObservableCollection<ObservableBfResource>(_boardQuestsSaveData.Completed
      .Select(x => new ObservableBfResource(x)).ToList());
    _completed.CollectionChanged += CompletedOnCollectionChanged;
  }

  private void OpenedOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _boardQuestsSaveData.Opened);

  private void TakenOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _boardQuestsSaveData.Taken);

  private void CompletedOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _boardQuestsSaveData.Completed);

  private void CollectionChanged(NotifyCollectionChangedEventArgs e,
                                 BfSerializableCollection<BfQuest> collection)
  {
    var newList = e.NewItems?.Cast<ObservableBfResource>().ToList();
    switch (e.Action)
    {
      case NotifyCollectionChangedAction.Add:
        collection.Insert(e.NewStartingIndex, new BfQuest { Id = newList![0].Id });
        break;
      case NotifyCollectionChangedAction.Remove:
        collection.RemoveAt(e.OldStartingIndex);
        break;
      case NotifyCollectionChangedAction.Replace:
        break;
      case NotifyCollectionChangedAction.Move:
        break;
      case NotifyCollectionChangedAction.Reset:
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  public void Dispose()
  {
    Opened.CollectionChanged -= OpenedOnCollectionChanged;
    Taken.CollectionChanged -= TakenOnCollectionChanged;
    Completed.CollectionChanged -= CompletedOnCollectionChanged;
  }
}
