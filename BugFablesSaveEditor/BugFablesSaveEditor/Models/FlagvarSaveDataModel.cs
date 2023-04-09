using BugFablesLib.SaveData;
using BugFablesSaveEditor.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class FlagvarSaveDataModel : ObservableObject, IFlagViewModel
{
  private readonly FlagvarSaveData _model;

  [ObservableProperty]
  private int _index;

  [ObservableProperty]
  private FlagvarSaveDataModel _flag = null!;

  public string Description1 { get; set; } = "";

  public int Var
  {
    get => _model.Var;
    set => SetProperty(_model.Var, value, _model, (data, s) => data.Var = s);
  }

  public FlagvarSaveDataModel(FlagvarSaveData flagvarSaveData) => _model = flagvarSaveData;
}
