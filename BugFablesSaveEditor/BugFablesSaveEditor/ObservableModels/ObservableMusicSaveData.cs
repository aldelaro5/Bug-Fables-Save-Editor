using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableMusicSaveData : ObservableModel
{
  public sealed override BfMusicSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfNamedId _music;

  public bool Bought
  {
    get => UnderlyingData.Bought;
    set => SetProperty(UnderlyingData.Bought, value, UnderlyingData, (data, b) => data.Bought = b);
  }

  public ObservableMusicSaveData(BfMusicSaveData musicSaveData) :
    base(musicSaveData)
  {
    UnderlyingData = musicSaveData;
    _music = new ObservableBfNamedId(musicSaveData.Song);
  }
}
