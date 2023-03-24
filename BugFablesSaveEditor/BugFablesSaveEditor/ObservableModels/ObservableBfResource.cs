using System.Collections.Generic;
using BugFablesLib;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableBfResource : ObservableModel
{
  public sealed override BfSerializableResource UnderlyingData { get; }
  public ReactiveProperty<int> Id { get; }
  public IReadOnlyList<string> AllResourceNames => UnderlyingData.AllNames;

  public ObservableBfResource(BfSerializableResource resource) : base(resource)
  {
    UnderlyingData = resource;
    Id = ReactiveProperty.FromObject(UnderlyingData, data => data.Id);
  }
}
