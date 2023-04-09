using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor;

/// <summary>
/// A wrapper around an ObservableCollection that wraps a model collection into their view model wrapper.
/// It allows collections without change notifications to gain them via this wrapper which will automatically
/// pass any addition or deletion to the underlying collection. It is itself an IModelWrapper which allows
/// recursions.
/// <para />
/// It includes the AddViewModel and RemoveViewModel command for the UI to easily bind to as well as a
/// NewViewModel property as a working object to use for potential addition.
/// </summary>
/// <typeparam name="TModel">The model type to wrap, must have a default constructor</typeparam>
/// <typeparam name="TViewModel">The view model type that wraps TModel</typeparam>
public partial class ViewModelCollection<TModel, TViewModel> : ObservableObject,
  IModelWrapper<Collection<TModel>>,
  IDisposable
  where TViewModel : IModelWrapper<TModel>
  where TModel : new()
{
  /// <summary>
  /// The Collection of the ViewModel, this will propagate all changes to the underlying model collection
  /// </summary>
  public ObservableCollection<TViewModel> Collection { get; }

  public Collection<TModel> Model { get; }

  /// <summary>
  /// A working object the UI can bind to for potential addition
  /// </summary>
  [ObservableProperty]
  private TViewModel _newViewModel;

  public static IModelWrapper<Collection<TModel>> WrapModel(Collection<TModel> model) =>
    new ViewModelCollection<TModel, TViewModel>(model);

  public ViewModelCollection(Collection<TModel> collection)
  {
    Collection = new(collection.Select<TModel, TViewModel>(x => (TViewModel)TViewModel.WrapModel(x)));
    Collection.CollectionChanged += OnCollectionChanged;
    Model = collection;
    _newViewModel = (TViewModel)TViewModel.WrapModel(new());
  }

  /// <summary>
  /// Adds a view model to the collection which will be propagated to the model collection
  /// </summary>
  /// <param name="item">The view model to add</param>
  [RelayCommand]
  private void AddViewModel(TViewModel item) => Collection.Add((TViewModel)TViewModel.WrapModel(item.Model));

  /// <summary>
  /// Deletes a view model to the collection which will be propagated to the model collection
  /// </summary>
  /// <param name="item">The view model to delete</param>
  [RelayCommand]
  private void RemoveViewModel(TViewModel item) => Collection.Remove(item);

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

  public void Dispose()
  {
    Collection.CollectionChanged -= OnCollectionChanged;
  }
}
