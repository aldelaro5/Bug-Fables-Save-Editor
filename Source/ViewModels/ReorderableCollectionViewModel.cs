using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class ReorderableCollectionViewModel<T> : ObservableObject
  where T : INotifyPropertyChanged
{
  [ObservableProperty]
  private ObservableCollection<T> _collection = null!;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(ReorderSelectedElementUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(ReorderSelectedElementDownCommand))]
  private T? _selectedElement;

  public ReorderableCollectionViewModel(IEnumerable<T> collection)
  {
    Collection = new ObservableCollection<T>(collection);
  }

  [RelayCommand(CanExecute = nameof(CanReorderSelectedElementUp))]
  private void ReorderSelectedElementUp() => ReorderSelected(Utils.ReorderDirection.Up);

  private bool CanReorderSelectedElementUp() => Collection.Count > 0 &&
                                                SelectedElement is not null &&
                                                !Collection[0].Equals(SelectedElement);

  [RelayCommand(CanExecute = nameof(CanReorderSelectedElementDown))]
  private void ReorderSelectedElementDown() => ReorderSelected(Utils.ReorderDirection.Down);

  private bool CanReorderSelectedElementDown() => Collection.Count > 0 &&
                                                  SelectedElement is not null &&
                                                  !Collection[^1].Equals(SelectedElement);

  [RelayCommand]
  private void RemoveElement(T element) => Collection.Remove(element);

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
  }
}
