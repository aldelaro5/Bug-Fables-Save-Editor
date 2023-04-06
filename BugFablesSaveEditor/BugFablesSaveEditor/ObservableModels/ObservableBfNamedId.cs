using System.Collections.Generic;
using BugFablesLib;
using BugFablesLib.Data;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableBfNamedId : ObservableModel
{
  public sealed override BfSerializableNamedId UnderlyingData { get; }

  public int Id
  {
    get => UnderlyingData.Id;
    set
    {
      // Workaround Avalonia bug:
      if (value >= 0)
        SetProperty(UnderlyingData.Id, value, UnderlyingData, (namedId, i) => namedId.Id = i);
    }
  }

  public string Name => UnderlyingData.Name;
  public IReadOnlyList<string> AllResourceNames => BugFablesLib.Utils.GetAllBfNames(UnderlyingData);

  public ObservableBfNamedId(BfSerializableNamedId namedId) : base(namedId)
  {
    UnderlyingData = namedId;
  }

  public BfQuest ToQuest() => new() { Id = Id };
  public BfMedal ToMedal() => new() { Id = Id };
  public BfItem ToItem() => new() { Id = Id };
}
