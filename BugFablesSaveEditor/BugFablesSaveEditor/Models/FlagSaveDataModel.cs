using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class FlagSaveDataModel : ObservableObject, IFlagViewModel, IModelWrapper<FlagSaveData>
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
  public FlagSaveDataModel(FlagSaveData flagSaveData) => Model = flagSaveData;
}
