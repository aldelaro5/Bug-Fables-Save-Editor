using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using BugFablesLib;

namespace BugFablesSaveEditor.ViewModels;

public class ObservableBfCollection<TSource, TObservable> :
  ObservableCollection<TObservable>, IDisposable
  where TSource : IBfSerializable
  where TObservable : BfObservable
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
    base(creator(collection))
  {
    _underCollection = collection;
    base.CollectionChanged += OnCollectionChanged;
  }

  private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
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
  }

  public void Dispose()
  {
    base.CollectionChanged -= OnCollectionChanged;
  }
}
