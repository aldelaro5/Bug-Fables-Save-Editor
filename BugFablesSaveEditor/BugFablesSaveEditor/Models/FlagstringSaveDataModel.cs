using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class FlagstringSaveDataModel : ObservableObject, IFlagViewModel
{
  private readonly FlagstringSaveData _model;

  [ObservableProperty]
  private int _index;

  public string Str
  {
    get => _model.Str;
    set => SetProperty(_model.Str, value, _model, (data, s) => data.Str = s);
  }

  [ObservableProperty]
  private string _description1 = "";

  public FlagstringSaveDataModel(FlagstringSaveData flagstringSaveData) => _model = flagstringSaveData;
}
