using System.Collections.Generic;
using BugFablesLib;
using BugFablesLib.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableBfNamedId : ObservableObject, IModelWrapper
{
  object IModelWrapper.Model { get => Model; }
  public BfSerializableNamedId Model { get; }

  public int Id
  {
    get => Model.Id;
    set
    {
      // Workaround Avalonia bug https://github.com/AvaloniaUI/Avalonia/issues/10846
      if (value >= 0)
        SetProperty(Model.Id, value, Model, (namedId, i) => namedId.Id = i);
    }
  }

  public string Name => Model.Name;
  public IReadOnlyList<string> AllResourceNames => BugFablesLib.Utils.GetAllBfNames(Model);

  public ObservableBfNamedId(BfSerializableNamedId namedId) => Model = namedId;

  public BfQuest ToQuest() => new() { Id = Id };
  public BfMedal ToMedal() => new() { Id = Id };
  public BfItem ToItem() => new() { Id = Id };
}
