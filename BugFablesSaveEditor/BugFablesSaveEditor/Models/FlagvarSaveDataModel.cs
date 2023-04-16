using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class FlagvarSaveDataModel : ObservableObject, IFlagModel, IModelWrapper<FlagvarSaveData>
{
  public FlagvarSaveData Model { get; }

  [ObservableProperty]
  private int _index;

  public int Var
  {
    get => Model.Var;
    set => SetProperty(Model.Var, value, Model, (data, s) => data.Var = s);
  }

  [ObservableProperty]
  private string _description1 = "";

  [ObservableProperty]
  private string _description2 = "";

  public static IModelWrapper<FlagvarSaveData> WrapModel(FlagvarSaveData model) => new FlagvarSaveDataModel(model);

  public static IModelWrapper<FlagvarSaveData> WrapNewModel(FlagvarSaveData model) =>
    new FlagvarSaveDataModel(new FlagvarSaveData { Var = model.Var });

  private FlagvarSaveDataModel(FlagvarSaveData flagvarSaveData) => Model = flagvarSaveData;
}
