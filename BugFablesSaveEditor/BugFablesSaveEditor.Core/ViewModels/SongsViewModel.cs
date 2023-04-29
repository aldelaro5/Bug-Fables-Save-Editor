using System;
using System.Collections.ObjectModel;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Core.ViewModels;

public partial class SongsViewModel : ObservableObject, IDisposable
{
  [ObservableProperty]
  private ViewModelCollection<BfMusicSaveData, MusicSaveDataModel> _musicSaveData;

  public SongsViewModel() : this(new()) { }

  public SongsViewModel(Collection<BfMusicSaveData> musicSaveData) => _musicSaveData = new(musicSaveData);

  public void Dispose()
  {
    MusicSaveData.Dispose();
  }
}
