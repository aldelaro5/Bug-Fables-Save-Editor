using System.Collections.Generic;
using BugFablesLib;
using BugFablesLib.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public class BfNamedIdModel : ObservableObject, IModelWrapper<BfQuest>, IModelWrapper<BfAnimId>,
  IModelWrapper<BfItem>, IModelWrapper<BfMedal>
{
  private BfSerializableNamedId Model { get; }
  private static BfNamedIdModel Create(BfSerializableNamedId model) => new(model);

  BfMedal IModelWrapper<BfMedal>.Model { get => (BfMedal)Model; }
  BfItem IModelWrapper<BfItem>.Model { get => (BfItem)Model; }
  BfAnimId IModelWrapper<BfAnimId>.Model { get => (BfAnimId)Model; }
  BfQuest IModelWrapper<BfQuest>.Model { get => (BfQuest)Model; }
  public static IModelWrapper<BfAnimId> WrapModel(BfAnimId model) => Create(model);
  public static IModelWrapper<BfMedal> WrapModel(BfMedal model) => Create(model);
  public static IModelWrapper<BfItem> WrapModel(BfItem model) => Create(model);
  public static IModelWrapper<BfQuest> WrapModel(BfQuest model) => Create(model);

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

  public BfNamedIdModel(BfSerializableNamedId namedId) => Model = namedId;
}
