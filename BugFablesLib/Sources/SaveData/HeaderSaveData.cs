using System;
using System.Globalization;
using System.Text;
using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class HeaderSaveData : IBfData
{
  public string Filename { get; set; } = "";
  public bool IsFrameone { get; set; }
  public bool IsHardest { get; set; }
  public bool IsMorefarm { get; set; }
  public bool IsMystery { get; set; }
  public bool IsPushrock { get; set; }
  public bool IsRuigee { get; set; }
  public float PositionX { get; set; }
  public float PositionY { get; set; }
  public float PositionZ { get; set; }

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { CommaSeparator }, StringSplitOptions.None);
    if (data.Length != 10)
      throw new Exception(nameof(HeaderSaveData) + " is in an invalid format");

    PositionX = ParseValueType<float>(data[0], nameof(PositionX));
    PositionY = ParseValueType<float>(data[1], nameof(PositionY));
    PositionZ = ParseValueType<float>(data[2], nameof(PositionZ));
    IsRuigee = ParseValueType<bool>(data[3], nameof(IsRuigee));
    IsHardest = ParseValueType<bool>(data[4], nameof(IsHardest));
    IsFrameone = ParseValueType<bool>(data[5], nameof(IsFrameone));
    IsPushrock = ParseValueType<bool>(data[6], nameof(IsPushrock));
    IsMorefarm = ParseValueType<bool>(data[7], nameof(IsMorefarm));
    IsMystery = ParseValueType<bool>(data[8], nameof(IsMystery));
    Filename = data[9];
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(PositionX.ToString(NumberFormatInfo.InvariantInfo));
    sb.Append(CommaSeparator);
    sb.Append(PositionY.ToString(NumberFormatInfo.InvariantInfo));
    sb.Append(CommaSeparator);
    sb.Append(PositionZ.ToString(NumberFormatInfo.InvariantInfo));
    sb.Append(CommaSeparator);
    sb.Append(IsRuigee);
    sb.Append(CommaSeparator);
    sb.Append(IsHardest);
    sb.Append(CommaSeparator);
    sb.Append(IsFrameone);
    sb.Append(CommaSeparator);
    sb.Append(IsPushrock);
    sb.Append(CommaSeparator);
    sb.Append(IsMorefarm);
    sb.Append(CommaSeparator);
    sb.Append(IsMystery);
    sb.Append(CommaSeparator);
    sb.Append(Filename);

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    PositionX = 0f;
    PositionY = 0f;
    PositionZ = 0f;
    IsRuigee = false;
    IsHardest = false;
    IsFrameone = false;
    IsPushrock = false;
    IsMorefarm = false;
    IsMystery = false;
    Filename = "";
  }
}
