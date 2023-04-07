using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class SongsViewModel : ObservableObject
{
  [ObservableProperty]
  private ViewModelCollection<BfMusicSaveData, ObservableMusicSaveData> _musicSaveData;

  public SongsViewModel()
  {
    _musicSaveData = new(new(), x => new ObservableMusicSaveData(x));
  }

  public SongsViewModel(
    ViewModelCollection<BfMusicSaveData, ObservableMusicSaveData> musicSaveData)
  {
    _musicSaveData = musicSaveData;
  }
}
