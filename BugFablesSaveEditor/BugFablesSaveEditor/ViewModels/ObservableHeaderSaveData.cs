using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableHeaderSaveData : BfObservable
{
  private readonly HeaderSaveData _headerSaveData;

  public string Filename
  {
    get => _headerSaveData.Filename;
    set => SetProperty(_headerSaveData.Filename, value, _headerSaveData,
      (x, y) => x.Filename = y);
  }

  public bool IsFrameone
  {
    get => _headerSaveData.IsFrameone;
    set => SetProperty(_headerSaveData.IsFrameone, value, _headerSaveData,
      (x, y) => x.IsFrameone = y);
  }

  public bool IsHardest
  {
    get => _headerSaveData.IsHardest;
    set => SetProperty(_headerSaveData.IsHardest, value, _headerSaveData,
      (x, y) => x.IsHardest = y);
  }

  public bool IsMorefarm
  {
    get => _headerSaveData.IsMorefarm;
    set => SetProperty(_headerSaveData.IsMorefarm, value, _headerSaveData,
      (x, y) => x.IsMorefarm = y);
  }

  public bool IsMystery
  {
    get => _headerSaveData.IsMystery;
    set => SetProperty(_headerSaveData.IsMystery, value, _headerSaveData,
      (x, y) => x.IsMystery = y);
  }

  public bool IsPushrock
  {
    get => _headerSaveData.IsPushrock;
    set => SetProperty(_headerSaveData.IsPushrock, value, _headerSaveData,
      (x, y) => x.IsPushrock = y);
  }

  public bool IsRuigee
  {
    get => _headerSaveData.IsRuigee;
    set => SetProperty(_headerSaveData.IsRuigee, value, _headerSaveData,
      (x, y) => x.IsRuigee = y);
  }

  public float PositionX
  {
    get => _headerSaveData.PositionX;
    set => SetProperty(_headerSaveData.PositionX, value, _headerSaveData,
      (x, y) => x.PositionX = y);
  }

  public float PositionY
  {
    get => _headerSaveData.PositionY;
    set => SetProperty(_headerSaveData.PositionY, value, _headerSaveData,
      (x, y) => x.PositionY = y);
  }

  public float PositionZ
  {
    get => _headerSaveData.PositionZ;
    set => SetProperty(_headerSaveData.PositionZ, value, _headerSaveData,
      (x, y) => x.PositionZ = y);
  }

  public ObservableHeaderSaveData(HeaderSaveData headerSaveData) :
    base(headerSaveData)
  {
    _headerSaveData = headerSaveData;
  }
}
