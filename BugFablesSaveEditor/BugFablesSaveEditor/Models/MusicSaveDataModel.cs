using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class MusicSaveDataModel : ObservableObject, IModelWrapper<BfMusicSaveData>
{
  public BfMusicSaveData Model { get; }
  public static IModelWrapper<BfMusicSaveData> WrapModel(BfMusicSaveData model) => new MusicSaveDataModel(model);

  [ObservableProperty]
  private BfNamedIdModel _music;

  public bool Bought
  {
    get => Model.Bought;
    set => SetProperty(Model.Bought, value, Model, (data, b) => data.Bought = b);
  }

  public MusicSaveDataModel(BfMusicSaveData musicSaveData)
  {
    Model = musicSaveData;
    _music = new BfNamedIdModel(musicSaveData.Song);
  }
}
