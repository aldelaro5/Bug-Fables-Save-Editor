using BugFablesLib.SaveData;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableHeaderSaveData : ObservableModel
{
  public sealed override HeaderSaveData UnderlyingData { get; }

  public string Filename
  {
    get => UnderlyingData.Filename;
    set => SetProperty(UnderlyingData.Filename, value, UnderlyingData, (data, s) => data.Filename = s);
  }

  public bool IsFrameone
  {
    get => UnderlyingData.IsFrameone;
    set => SetProperty(UnderlyingData.IsFrameone, value, UnderlyingData, (data, s) => data.IsFrameone = s);
  }

  public bool IsHardest
  {
    get => UnderlyingData.IsHardest;
    set => SetProperty(UnderlyingData.IsHardest, value, UnderlyingData, (data, s) => data.IsHardest = s);
  }

  public bool IsMorefarm
  {
    get => UnderlyingData.IsMorefarm;
    set => SetProperty(UnderlyingData.IsMorefarm, value, UnderlyingData, (data, s) => data.IsMorefarm = s);
  }

  public bool IsMystery
  {
    get => UnderlyingData.IsMystery;
    set => SetProperty(UnderlyingData.IsMystery, value, UnderlyingData, (data, s) => data.IsMystery = s);
  }

  public bool IsPushrock
  {
    get => UnderlyingData.IsPushrock;
    set => SetProperty(UnderlyingData.IsPushrock, value, UnderlyingData, (data, s) => data.IsPushrock = s);
  }

  public bool IsRuigee
  {
    get => UnderlyingData.IsRuigee;
    set => SetProperty(UnderlyingData.IsRuigee, value, UnderlyingData, (data, s) => data.IsRuigee = s);
  }

  public float PositionX
  {
    get => UnderlyingData.PositionX;
    set => SetProperty(UnderlyingData.PositionX, value, UnderlyingData, (data, s) => data.PositionX = s);
  }

  public float PositionY
  {
    get => UnderlyingData.PositionY;
    set => SetProperty(UnderlyingData.PositionY, value, UnderlyingData, (data, s) => data.PositionY = s);
  }

  public float PositionZ
  {
    get => UnderlyingData.PositionZ;
    set => SetProperty(UnderlyingData.PositionZ, value, UnderlyingData, (data, s) => data.PositionZ = s);
  }

  public ObservableHeaderSaveData(HeaderSaveData headerSaveData) :
    base(headerSaveData)
  {
    UnderlyingData = headerSaveData;
  }
}
