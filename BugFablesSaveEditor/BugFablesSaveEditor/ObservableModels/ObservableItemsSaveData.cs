using System.Linq;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

[ObservableObject]
public partial class ObservableItemsSaveData : ObservableModel
{
  public sealed override ItemsSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfCollection<BfItem, ObservableBfResource> _inventory;

  [ObservableProperty]
  private ObservableBfCollection<BfItem, ObservableBfResource> _key;

  [ObservableProperty]
  private ObservableBfCollection<BfItem, ObservableBfResource> _stored;

  public ObservableItemsSaveData(ItemsSaveData itemsSaveData) : base(itemsSaveData)
  {
    UnderlyingData = itemsSaveData;
    _inventory = new(UnderlyingData.Inventory,
      x => x.Select(bfItems => new ObservableBfResource(bfItems)).ToList());
    _key = new(UnderlyingData.KeyItems,
      x => x.Select(bfItems => new ObservableBfResource(bfItems)).ToList());
    _stored = new(UnderlyingData.Stored,
      x => x.Select(bfItems => new ObservableBfResource(bfItems)).ToList());
  }
}
