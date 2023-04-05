using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableMusicSaveData : ObservableModel
{
  public sealed override BfMusicSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfNamedId _music;

  public ReactiveProperty<bool> Bought { get; }

  public ObservableMusicSaveData(BfMusicSaveData musicSaveData) :
    base(musicSaveData)
  {
    UnderlyingData = musicSaveData;
    _music = new ObservableBfNamedId(musicSaveData.Song);
    Bought = ReactiveProperty.FromObject(UnderlyingData, data => data.Bought);
  }
}
