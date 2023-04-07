using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ViewModelCollection<TModel, TViewModel> : ObservableObject, IModelWrapper<IList<TModel>>
  where TViewModel : IModelWrapper<TModel>
  where TModel : new()
{
  private readonly Func<TModel, TViewModel> _viewModelFactory;
  private readonly ObservableCollection<TViewModel> _collection;

  public IList<TModel> Model { get; }
  public ReadOnlyObservableCollection<TViewModel> CollectionView { get; }

  [ObservableProperty]
  private TViewModel _newViewModel;

  [RelayCommand]
  private void AddViewModel(TViewModel item) => _collection.Add(_viewModelFactory.Invoke(item.Model));

  [RelayCommand]
  private void RemoveViewModel(TViewModel item) => _collection.Remove(item);

  public ViewModelCollection(Collection<TModel> collection, Func<TModel, TViewModel> creator)
  {
    _collection = new(collection.Select(creator.Invoke));
    _collection.CollectionChanged += OnCollectionChanged;
    CollectionView = new(_collection);
    _viewModelFactory = creator;
    Model = collection;
    _newViewModel = creator.Invoke(new());
  }

  private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    var newList = e.NewItems?.Cast<TViewModel>().ToList();
    switch (e.Action)
    {
      case NotifyCollectionChangedAction.Add:
        Model.Insert(e.NewStartingIndex, newList![0].Model);
        break;
      case NotifyCollectionChangedAction.Remove:
        Model.RemoveAt(e.OldStartingIndex);
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }
}
