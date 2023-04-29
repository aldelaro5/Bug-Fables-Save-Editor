using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Core.Models;

public partial class FlagSaveDataModel : ObservableObject, IFlagModel, IModelWrapper<FlagSaveData>
{
  public FlagSaveData Model { get; }

  [ObservableProperty]
  private int _index;

  public bool Enabled
  {
    get => Model.Enabled;
    set => SetProperty(Model.Enabled, value, Model, (data, b) => data.Enabled = b);
  }

  [ObservableProperty]
  private string _description1 = "";

  [ObservableProperty]
  private string _description2 = "";

  public static IModelWrapper<FlagSaveData> WrapModel(FlagSaveData model) => new FlagSaveDataModel(model);

  public static IModelWrapper<FlagSaveData> WrapNewModel(FlagSaveData model) =>
    new FlagSaveDataModel(new FlagSaveData { Enabled = model.Enabled });

  public FlagSaveDataModel(FlagSaveData flagSaveData) => Model = flagSaveData;
}
