using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class ObservableMusicSaveData : ObservableObject
{
  private readonly MusicSaveData _musicSaveData;

  [ObservableProperty]
  private ObservableBfResource _music;

  public bool Bought
  {
    get => _musicSaveData.Bought;
    set => SetProperty(_musicSaveData.Bought, value, _musicSaveData,
      (music, n) => music.Bought = n);
  }

  public ObservableMusicSaveData(MusicSaveData musicSaveData)
  {
    _musicSaveData = musicSaveData;
    _music = new ObservableBfResource(musicSaveData.Song);
  }
}
