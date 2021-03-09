using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Header : IBugFablesSaveSection
  {
    public class HeaderInfo
    {
      public float PositionX { get; set; }
      public float PositionY { get; set; }
      public float PositionZ { get; set; }
      public bool IsRuigee { get; set; }
      public bool IsHardest { get; set; }
      public bool IsFrameone { get; set; }
      public bool IsPushrock { get; set; }
      public bool IsMorefarm { get; set; }
      public bool IsMystery { get; set; }
      public string Filename { get; set; } = "";
    }

    public object Data { get; set; } = new HeaderInfo();

    public string EncodeToSaveLine()
    {
      HeaderInfo headerInfo = (HeaderInfo)Data;
      StringBuilder sb = new StringBuilder();

      sb.Append(headerInfo.PositionX);
      sb.Append(Common.FieldSeparator);
      sb.Append(headerInfo.PositionY);
      sb.Append(Common.FieldSeparator);
      sb.Append(headerInfo.PositionZ);
      sb.Append(Common.FieldSeparator);
      sb.Append(headerInfo.IsRuigee);
      sb.Append(Common.FieldSeparator);
      sb.Append(headerInfo.IsHardest);
      sb.Append(Common.FieldSeparator);
      sb.Append(headerInfo.IsFrameone);
      sb.Append(Common.FieldSeparator);
      sb.Append(headerInfo.IsPushrock);
      sb.Append(Common.FieldSeparator);
      sb.Append(headerInfo.IsMorefarm);
      sb.Append(Common.FieldSeparator);
      sb.Append(headerInfo.IsMystery);
      sb.Append(Common.FieldSeparator);
      sb.Append(headerInfo.Filename);

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] data = saveLine.Split(Common.FieldSeparator);

      if (data.Length != 10)
        throw new Exception(nameof(Header) + " is in an invalid format");

      HeaderInfo headerInfo = (HeaderInfo)Data;

      float floatOut = 0;
      if (!float.TryParse(data[0], out floatOut))
        throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.PositionX) + " failed to parse");
      headerInfo.PositionX = floatOut;
      if (!float.TryParse(data[1], out floatOut))
        throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.PositionY) + " failed to parse");
      headerInfo.PositionY = floatOut;
      if (!float.TryParse(data[2], out floatOut))
        throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.PositionZ) + " failed to parse");
      headerInfo.PositionZ = floatOut;

      bool boolOut = false;
      if (!bool.TryParse(data[3], out boolOut))
        throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsRuigee) + " failed to parse");
      headerInfo.IsRuigee = boolOut;
      if (!bool.TryParse(data[4], out boolOut))
        throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsHardest) + " failed to parse");
      headerInfo.IsHardest = boolOut;
      if (!bool.TryParse(data[5], out boolOut))
        throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsFrameone) + " failed to parse");
      headerInfo.IsFrameone = boolOut;
      if (!bool.TryParse(data[6], out boolOut))
        throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsPushrock) + " failed to parse");
      headerInfo.IsPushrock = boolOut;
      if (!bool.TryParse(data[7], out boolOut))
        throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsMorefarm) + " failed to parse");
      headerInfo.IsMorefarm = boolOut;
      if (!bool.TryParse(data[8], out boolOut))
        throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsMystery) + " failed to parse");
      headerInfo.IsMystery = boolOut;

      headerInfo.Filename = data[9];
    }
  }
}
