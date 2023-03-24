using System.Reactive.Linq;
using BugFablesLib.SaveData;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableHeaderSaveData : ObservableModel
{
  public sealed override HeaderSaveData UnderlyingData { get; }

  public ReactiveProperty<string> Filename { get; }
  public ReactiveProperty<bool> IsFrameone { get; }
  public ReactiveProperty<bool> IsHardest { get; }
  public ReactiveProperty<bool> IsMorefarm { get; }
  public ReactiveProperty<bool> IsMystery { get; }
  public ReactiveProperty<bool> IsPushrock { get; }
  public ReactiveProperty<bool> IsRuigee { get; }
  public ReactiveProperty<float> PositionX { get; }
  public ReactiveProperty<float> PositionY { get; }
  public ReactiveProperty<float> PositionZ { get; }

  public ObservableHeaderSaveData(HeaderSaveData headerSaveData) :
    base(headerSaveData)
  {
    UnderlyingData = headerSaveData;
    PositionX = ReactiveProperty.FromObject(UnderlyingData, data => data.PositionX);
    PositionY = ReactiveProperty.FromObject(UnderlyingData, data => data.PositionY);
    PositionZ = ReactiveProperty.FromObject(UnderlyingData, data => data.PositionZ);
    IsRuigee = ReactiveProperty.FromObject(UnderlyingData, data => data.IsRuigee);
    IsHardest = ReactiveProperty.FromObject(UnderlyingData, data => data.IsHardest);
    IsFrameone = ReactiveProperty.FromObject(UnderlyingData, data => data.IsFrameone);
    IsPushrock = ReactiveProperty.FromObject(UnderlyingData, data => data.IsPushrock);
    IsMorefarm = ReactiveProperty.FromObject(UnderlyingData, data => data.IsMorefarm);
    IsMystery = ReactiveProperty.FromObject(UnderlyingData, data => data.IsMystery);
    Filename = ReactiveProperty.FromObject(UnderlyingData, data => data.Filename);
  }
}
