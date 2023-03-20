using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace BugFablesSaveEditor.ViewModels;

public class TrackedObservableCollection<T> : ObservableCollection<T>, IDisposable
{
  private IList<T> _underCollection;

  public IList<T> UnderCollection
  {
    get => _underCollection;
    set
    {
      _underCollection = value;
      OnPropertyChanged(new PropertyChangedEventArgs(nameof(UnderCollection)));
    }
  }

  public TrackedObservableCollection(IList<T> collection) : base(collection)
  {
    _underCollection = collection;
    base.CollectionChanged += OnCollectionChanged;
  }

  private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    var newList = e.NewItems?.Cast<T>().ToList();
    switch (e.Action)
    {
      case NotifyCollectionChangedAction.Add:
        UnderCollection.Insert(e.NewStartingIndex, newList![0]);
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

  public void Dispose() => base.CollectionChanged -= OnCollectionChanged;
}
