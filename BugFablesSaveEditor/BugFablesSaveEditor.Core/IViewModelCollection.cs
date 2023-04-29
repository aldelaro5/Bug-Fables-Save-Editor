using System.Collections;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.Core;

public interface IViewModelCollection
{
  public ICollection Collection { get; }
  public object NewViewModel { get; set; }

  /// <summary>
  /// Adds a view model to the collection which will be propagated to the model collection
  /// </summary>
  public IRelayCommand<object> AddViewModelCommand { get; }

  /// <summary>
  /// Deletes a view model to the collection which will be propagated to the model collection
  /// </summary>
  public IRelayCommand<object> RemoveViewModelCommand { get; }
}
