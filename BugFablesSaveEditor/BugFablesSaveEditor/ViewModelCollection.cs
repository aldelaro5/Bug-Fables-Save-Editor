using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor;

public partial class ViewModelCollection<TModel, TViewModel> : ObservableObject, IModelWrapper<Collection<TModel>>
  where TViewModel : IModelWrapper<TModel>
  where TModel : new()
{
  public ObservableCollection<TViewModel> Collection { get; }

  public Collection<TModel> Model { get; }

  public static IModelWrapper<Collection<TModel>> WrapModel(Collection<TModel> model) =>
    new ViewModelCollection<TModel, TViewModel>(model);

  [ObservableProperty]
  private TViewModel _newViewModel;

  [RelayCommand]
  private void AddViewModel(TViewModel item) => Collection.Add((TViewModel)TViewModel.WrapModel(item.Model));

  [RelayCommand]
  private void RemoveViewModel(TViewModel item) => Collection.Remove(item);

  public ViewModelCollection(Collection<TModel> collection)
  {
    Collection = new(collection.Select<TModel, TViewModel>(x => (TViewModel)TViewModel.WrapModel(x)));
    Collection.CollectionChanged += OnCollectionChanged;
    Model = collection;
    _newViewModel = (TViewModel)TViewModel.WrapModel(new());
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
