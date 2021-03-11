using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Header : IBugFablesSaveSection
  {
    public class HeaderInfo : INotifyPropertyChanged
    {
      private float _positionX;
      public float PositionX { get { return _positionX; } set { _positionX = value; NotifyPropertyChanged(); } }
      
      private float _positionY;
      public float PositionY { get { return _positionY; } set { _positionY = value; NotifyPropertyChanged(); } }
      
      private float _positionZ;
      public float PositionZ { get { return _positionZ; } set { _positionZ = value; NotifyPropertyChanged(); } }

      private bool _isRuigee;
      public bool IsRuigee { get { return _isRuigee; } set { _isRuigee = value; NotifyPropertyChanged(); } }
      
      private bool _isHardest;
      public bool IsHardest { get { return _isHardest; } set { _isHardest = value; NotifyPropertyChanged(); } }
      
      private bool _isFrameone; 
      public bool IsFrameone { get { return _isFrameone; } set { _isFrameone = value; NotifyPropertyChanged(); } }
      
      private bool _isPushrock;
      public bool IsPushrock { get { return _isPushrock; } set { _isPushrock = value; NotifyPropertyChanged(); } }
      
      private bool _isMorefarm;
      public bool IsMorefarm { get { return _isMorefarm; } set { _isMorefarm = value; NotifyPropertyChanged(); } }
      
      private bool _isMystery;
      public bool IsMystery { get { return _isMystery; } set { _isMystery = value; NotifyPropertyChanged(); } }

      private string _filename = "";
      public string Filename { get { return _filename; } set { _filename = value; NotifyPropertyChanged(); } }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
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
