using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace BugFablesSaveEditor.ObservableModels;

public class ViewModelCollection<TModel, TVm> : ObservableCollection<TVm>
  where TVm : IModelWrapper
{
  private IList<TModel> _underCollection;

  public IList<TModel> UnderCollection
  {
    get => _underCollection;
    set
    {
      _underCollection = value;
      OnPropertyChanged(new PropertyChangedEventArgs(nameof(UnderCollection)));
    }
  }

  public ViewModelCollection(Collection<TModel> collection, Func<TModel, TVm> creator) :
    base(collection.Select(creator.Invoke)) =>
    _underCollection = collection;

  protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
  {
    var newList = e.NewItems?.Cast<TVm>().ToList();
    switch (e.Action)
    {
      case NotifyCollectionChangedAction.Add:
        UnderCollection.Insert(e.NewStartingIndex, (TModel)newList![0].Model);
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
