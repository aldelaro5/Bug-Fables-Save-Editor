using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class FlagstringSaveDataModel : ObservableObject, IFlagViewModel, IModelWrapper<FlagstringSaveData>
{
  public FlagstringSaveData Model { get; }

  [ObservableProperty]
  private int _index;

  public string Str
  {
    get => Model.Str;
    set => SetProperty(Model.Str, value, Model, (data, s) => data.Str = s);
  }

  [ObservableProperty]
  private string _description1 = "";
  [ObservableProperty]
  private string _description2 = "";

  public static IModelWrapper<FlagstringSaveData> WrapModel(FlagstringSaveData model) =>
    new FlagstringSaveDataModel(model);

  private FlagstringSaveDataModel(FlagstringSaveData flagstringSaveData) => Model = flagstringSaveData;
}
