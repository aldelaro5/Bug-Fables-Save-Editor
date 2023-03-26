using System;
using System.Collections.Generic;
using BugFablesLib;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableBfNamedId : ObservableModel
{
  public sealed override BfSerializableNamedId UnderlyingData { get; }
  public ReactiveProperty<int> Id { get; }
  public string Name => UnderlyingData.Name;
  public IReadOnlyList<string> AllResourceNames => BugFablesLib.Utils.GetAllBfNames(UnderlyingData);

  public ObservableBfNamedId(BfSerializableNamedId namedId) : base(namedId)
  {
    UnderlyingData = namedId;
    Id = ReactiveProperty.FromObject(UnderlyingData, data => data.Id);
    Id.Subscribe(_ => OnPropertyChanged(nameof(Name)));
  }
}
