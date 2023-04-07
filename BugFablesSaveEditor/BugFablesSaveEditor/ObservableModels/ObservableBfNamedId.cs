using System.Collections.Generic;
using BugFablesLib;
using BugFablesLib.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableBfNamedId : ObservableObject, IModelWrapper<BfSerializableNamedId>,
  IModelWrapper<BfQuest>, IModelWrapper<BfAnimId>, IModelWrapper<BfItem>, IModelWrapper<BfMedal>
{
  public BfSerializableNamedId Model { get; }
  BfMedal IModelWrapper<BfMedal>.Model { get => (BfMedal)Model; }
  BfItem IModelWrapper<BfItem>.Model { get => (BfItem)Model; }
  BfAnimId IModelWrapper<BfAnimId>.Model { get => (BfAnimId)Model; }
  BfQuest IModelWrapper<BfQuest>.Model { get => (BfQuest)Model; }

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
}
