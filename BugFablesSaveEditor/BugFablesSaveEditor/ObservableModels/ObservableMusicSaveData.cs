using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableMusicSaveData : ObservableObject, IModelWrapper<BfMusicSaveData>
{
  public BfMusicSaveData Model { get; }
  public static IModelWrapper<BfMusicSaveData> Factory(BfMusicSaveData model) => new ObservableMusicSaveData(model);

  [ObservableProperty]
  private ObservableBfNamedId _music;

  public bool Bought
  {
    get => Model.Bought;
    set => SetProperty(Model.Bought, value, Model, (data, b) => data.Bought = b);
  }

  public ObservableMusicSaveData(BfMusicSaveData musicSaveData)
  {
    Model = musicSaveData;
    _music = new ObservableBfNamedId(musicSaveData.Song);
  }
}
