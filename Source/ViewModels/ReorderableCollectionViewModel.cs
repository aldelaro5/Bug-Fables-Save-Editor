using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class ReorderableCollectionViewModel<T> : ObservableObject
  where T : INotifyPropertyChanged
{
  [ObservableProperty]
  private IList<T> _collection;

  [ObservableProperty]
  private DataGridCollectionView _collectionView;

  [ObservableProperty]
  private List<string>? _exposedProperties;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(ReorderSelectedElementUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(ReorderSelectedElementDownCommand))]
  private T? _selectedElement;

  public ReorderableCollectionViewModel(IList<T> collection, List<string>? exposedProperties = null)
  {
    _exposedProperties = exposedProperties;
    _collection = collection;
    _collectionView = new DataGridCollectionView(collection);
  }

  [RelayCommand(CanExecute = nameof(CanReorderSelectedElementUp))]
  private void ReorderSelectedElementUp() => ReorderSelected(Utils.ReorderDirection.Up);

  private bool CanReorderSelectedElementUp() => CollectionView.Count > 0 &&
                                                SelectedElement is not null &&
                                                !CollectionView[0].Equals(SelectedElement);

  [RelayCommand(CanExecute = nameof(CanReorderSelectedElementDown))]
  private void ReorderSelectedElementDown() => ReorderSelected(Utils.ReorderDirection.Down);

  private bool CanReorderSelectedElementDown() => CollectionView.Count > 0 &&
                                                  SelectedElement is not null &&
                                                  !CollectionView[^1].Equals(SelectedElement);

  [RelayCommand]
  private void RemoveElement(T element) => CollectionView.Remove(element);

  private void ReorderSelected(Utils.ReorderDirection direction)
  {
    if (SelectedElement is null)
      return;

    int index = Collection.IndexOf(SelectedElement);
    int newIndex = index;
    if (direction == Utils.ReorderDirection.Up)
      newIndex--;
    else if (direction == Utils.ReorderDirection.Down)
      newIndex++;

    T item = Collection[index];
    Collection.RemoveAt(index);
    Collection.Insert(newIndex, item);
    SelectedElement = Collection[newIndex];
    CollectionView.Refresh();
  }
}
