using System.Collections.Generic;
using BugFablesLib;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableBfResource : BfObservable
{
  public override BfSerializableResource UnderlyingData { get; }

  public int Id
  {
    get => UnderlyingData.Id;
    set => SetProperty(UnderlyingData.Id, value, UnderlyingData, (resource, n) => resource.Id = n);
  }

  public IReadOnlyList<string> AllResourceNames => UnderlyingData.AllNames;

  public ObservableBfResource(BfSerializableResource resource) :
    base(resource) => UnderlyingData = resource;
}
