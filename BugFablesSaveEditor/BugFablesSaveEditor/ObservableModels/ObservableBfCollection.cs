using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using BugFablesLib;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableBfCollection<TSource, TObservable> : ObservableCollection<TObservable>
  where TSource : IBfSerializable
  where TObservable : ObservableModel
{
  private IList<TSource> _underCollection;

  public IList<TSource> UnderCollection
  {
    get => _underCollection;
    set
    {
      _underCollection = value;
      OnPropertyChanged(new PropertyChangedEventArgs(nameof(UnderCollection)));
    }
  }

  public ObservableBfCollection(Collection<TSource> collection,
                                Func<Collection<TSource>, IList<TObservable>> creator) :
    base(creator(collection)) =>
    _underCollection = collection;

  protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
  {
    var newList = e.NewItems?.Cast<TObservable>().ToList();
    switch (e.Action)
    {
      case NotifyCollectionChangedAction.Add:
        UnderCollection.Insert(e.NewStartingIndex, (TSource)newList![0].UnderlyingData);
        break;
      case NotifyCollectionChangedAction.Remove:
        UnderCollection.RemoveAt(e.OldStartingIndex);
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
    base.OnCollectionChanged(e);
  }
}
