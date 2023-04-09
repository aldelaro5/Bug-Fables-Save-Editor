using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class FlagvarSaveDataModel : ObservableObject, IFlagViewModel
{
  private readonly FlagvarSaveData _model;

  [ObservableProperty]
  private int _index;

  public int Var
  {
    get => _model.Var;
    set => SetProperty(_model.Var, value, _model, (data, s) => data.Var = s);
  }

  [ObservableProperty]
  private string _description1 = "";

  public FlagvarSaveDataModel(FlagvarSaveData flagvarSaveData) => _model = flagvarSaveData;
}
