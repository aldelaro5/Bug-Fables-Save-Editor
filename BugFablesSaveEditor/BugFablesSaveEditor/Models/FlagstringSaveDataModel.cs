using BugFablesLib.SaveData;
using BugFablesSaveEditor.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class FlagstringSaveDataModel : ObservableObject, IFlagViewModel
{
  private readonly FlagstringSaveData _model;

  [ObservableProperty]
  private int _index;

  public string Description1 { get; set; } = "";
  public string Str
  {
    get => _model.Str;
    set => SetProperty(_model.Str, value, _model, (data, s) => data.Str = s);
  }

  public FlagstringSaveDataModel(FlagstringSaveData flagstringSaveData) => _model = flagstringSaveData;
}
