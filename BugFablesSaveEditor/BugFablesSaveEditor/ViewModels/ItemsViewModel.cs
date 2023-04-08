using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class ItemsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableItemsSaveData _itemsSaveData;

  public ItemsViewModel() : this(new(new())) { }

  public ItemsViewModel(ObservableItemsSaveData itemsSaveData)
  {
    _itemsSaveData = itemsSaveData;
  }
}
