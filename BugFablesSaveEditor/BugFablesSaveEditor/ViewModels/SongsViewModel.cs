using System.Collections.Generic;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class SongsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableBfCollection<BfMusicSaveData, ObservableMusicSaveData> _musicSaveData;

  [ObservableProperty]
  private ObservableMusicSaveData _newMusic = new(new BfMusicSaveData());

  public SongsViewModel()
  {
    _musicSaveData = new(new(), _ => new List<ObservableMusicSaveData>());
  }

  public SongsViewModel(ObservableBfCollection<BfMusicSaveData, ObservableMusicSaveData> musicSaveData)
  {
    _musicSaveData = musicSaveData;
  }

  [RelayCommand]
  private void AddMusic(ObservableMusicSaveData music) =>
    MusicSaveData.Add(new(music.UnderlyingData));

  [RelayCommand]
  private void DeleteMusic(ObservableMusicSaveData music) =>
    MusicSaveData.Remove(music);
}

