using System.Collections.Generic;
using System.Linq;
using BugFablesLib;
using BugFablesLib.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Core.Models;

public class BfNamedIdModel : ObservableObject, IModelWrapper<BfQuest>, IModelWrapper<BfAnimId>,
  IModelWrapper<BfItem>, IModelWrapper<BfMedal>
{
  private BfSerializableNamedId Model { get; }

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

  public string Name
  {
    get
    {
      if (Model is BfAnimId)
      {
        return Model.Name switch
        {
          "Bee" => "Vi",
          "Beetle" => "Kabbu",
          "Moth" => "Leif",
          _ => Model.Name
        };
      }

      return Model.Name;
    }
  }

  public IReadOnlyList<string> AllResourceNames
  {
    get
    {
      var names = BugFablesLib.Utils.GetAllBfNames(Model).ToList();
      if (Model is BfAnimId)
      {
        names[0] = "Vi";
        names[1] = "Kabbu";
        names[2] = "Leif";
      }

      return names;
    }
  }

  public static IModelWrapper<BfAnimId> WrapModel(BfAnimId model) => Create(model);
  public static IModelWrapper<BfMedal> WrapModel(BfMedal model) => Create(model);
  public static IModelWrapper<BfItem> WrapModel(BfItem model) => Create(model);
  public static IModelWrapper<BfQuest> WrapModel(BfQuest model) => Create(model);
  public static IModelWrapper<BfAnimId> WrapNewModel(BfAnimId model) => Create(new BfAnimId { Id = model.Id });
  public static IModelWrapper<BfMedal> WrapNewModel(BfMedal model) => Create(new BfMedal { Id = model.Id });
  public static IModelWrapper<BfItem> WrapNewModel(BfItem model) => Create(new BfItem { Id = model.Id });
  public static IModelWrapper<BfQuest> WrapNewModel(BfQuest model) => Create(new BfQuest { Id = model.Id });

  public BfNamedIdModel(BfSerializableNamedId namedId) => Model = namedId;

  private static BfNamedIdModel Create(BfSerializableNamedId model) => new(model);
}
