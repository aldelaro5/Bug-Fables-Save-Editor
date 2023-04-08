using System.Collections.ObjectModel;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class SongsViewModel : ObservableObject
{
  [ObservableProperty]
  private ViewModelCollection<BfMusicSaveData, ObservableMusicSaveData> _musicSaveData;

  public SongsViewModel() : this(new()) { }

  public SongsViewModel(Collection<BfMusicSaveData> musicSaveData)
  {
    _musicSaveData = new(musicSaveData);
  }
}
