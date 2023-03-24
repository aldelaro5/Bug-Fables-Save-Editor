using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

[ObservableObject]
public partial class ObservableMusicSaveData : ObservableModel
{
  public override MusicSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfResource _music;

  public ReactiveProperty<bool> Bought { get; }

  public ObservableMusicSaveData(MusicSaveData musicSaveData) :
    base(musicSaveData)
  {
    UnderlyingData = musicSaveData;
    _music = new ObservableBfResource(musicSaveData.Song);
    Bought = ReactiveProperty.FromObject(UnderlyingData, data => data.Bought);
  }
}
