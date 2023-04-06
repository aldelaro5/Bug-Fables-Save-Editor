using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableHeaderSaveData : ObservableObject
{
  public HeaderSaveData Model { get; }

  public string Filename
  {
    get => Model.Filename;
    set => SetProperty(Model.Filename, value, Model, (data, s) => data.Filename = s);
  }

  public bool IsFrameone
  {
    get => Model.IsFrameone;
    set => SetProperty(Model.IsFrameone, value, Model, (data, s) => data.IsFrameone = s);
  }

  public bool IsHardest
  {
    get => Model.IsHardest;
    set => SetProperty(Model.IsHardest, value, Model, (data, s) => data.IsHardest = s);
  }

  public bool IsMorefarm
  {
    get => Model.IsMorefarm;
    set => SetProperty(Model.IsMorefarm, value, Model, (data, s) => data.IsMorefarm = s);
  }

  public bool IsMystery
  {
    get => Model.IsMystery;
    set => SetProperty(Model.IsMystery, value, Model, (data, s) => data.IsMystery = s);
  }

  public bool IsPushrock
  {
    get => Model.IsPushrock;
    set => SetProperty(Model.IsPushrock, value, Model, (data, s) => data.IsPushrock = s);
  }

  public bool IsRuigee
  {
    get => Model.IsRuigee;
    set => SetProperty(Model.IsRuigee, value, Model, (data, s) => data.IsRuigee = s);
  }

  public float PositionX
  {
    get => Model.PositionX;
    set => SetProperty(Model.PositionX, value, Model, (data, s) => data.PositionX = s);
  }

  public float PositionY
  {
    get => Model.PositionY;
    set => SetProperty(Model.PositionY, value, Model, (data, s) => data.PositionY = s);
  }

  public float PositionZ
  {
    get => Model.PositionZ;
    set => SetProperty(Model.PositionZ, value, Model, (data, s) => data.PositionZ = s);
  }

  public ObservableHeaderSaveData(HeaderSaveData headerSaveData) => Model = headerSaveData;
}
