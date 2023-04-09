using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class FlagSaveDataModel : ObservableObject, IFlagViewModel
{
  private readonly FlagSaveData _model;

  [ObservableProperty]
  private int _index;

  public bool Enabled
  {
    get => _model.Enabled;
    set => SetProperty(_model.Enabled, value, _model, (data, b) => data.Enabled = b);
  }

  [ObservableProperty]
  private string _description1 = "";

  [ObservableProperty]
  private string _description2 = "";

  public FlagSaveDataModel(FlagSaveData flagSaveData) => _model = flagSaveData;
}
