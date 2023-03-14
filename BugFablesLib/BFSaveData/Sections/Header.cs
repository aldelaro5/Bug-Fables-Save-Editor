using System;
using System.Globalization;
using System.Text;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class Header : BfData
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

  public override void Parse(string str)
  {
    string[] data = str.Split(new[] {Utils.PrimarySeparator}, StringSplitOptions.None);
    if (data.Length != 10)
      throw new Exception(nameof(Header) + " is in an invalid format");

    PositionX = ParseField<float>(data[0], nameof(PositionX));
    PositionY = ParseField<float>(data[1], nameof(PositionY));
    PositionZ = ParseField<float>(data[2], nameof(PositionZ));
    IsRuigee = ParseField<bool>(data[3], nameof(IsRuigee));
    IsHardest = ParseField<bool>(data[4], nameof(IsHardest));
    IsFrameone = ParseField<bool>(data[5], nameof(IsFrameone));
    IsPushrock = ParseField<bool>(data[6], nameof(IsPushrock));
    IsMorefarm = ParseField<bool>(data[7], nameof(IsMorefarm));
    IsMystery = ParseField<bool>(data[8], nameof(IsMystery));
    Filename = data[9];
  }

  public override string ToString()
  {
    StringBuilder sb = new();

    sb.Append(PositionX.ToString(NumberFormatInfo.InvariantInfo));
    sb.Append(Utils.PrimarySeparator);
    sb.Append(PositionY.ToString(NumberFormatInfo.InvariantInfo));
    sb.Append(Utils.PrimarySeparator);
    sb.Append(PositionZ.ToString(NumberFormatInfo.InvariantInfo));
    sb.Append(Utils.PrimarySeparator);
    sb.Append(IsRuigee);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(IsHardest);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(IsFrameone);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(IsPushrock);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(IsMorefarm);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(IsMystery);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(Filename);

    return sb.ToString();
  }

  public override void ResetToDefault()
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
