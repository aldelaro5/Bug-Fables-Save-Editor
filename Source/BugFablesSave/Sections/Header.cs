using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public class Header : IBugFablesSaveSection
{
  public object Data { get; set; } = new HeaderInfo();

  public string EncodeToSaveLine()
  {
    HeaderInfo headerInfo = (HeaderInfo)Data;
    StringBuilder sb = new();

    sb.Append(headerInfo.PositionX.ToString(NumberFormatInfo.InvariantInfo));
    sb.Append(Utils.Common.FieldSeparator);
    sb.Append(headerInfo.PositionY.ToString(NumberFormatInfo.InvariantInfo));
    sb.Append(Utils.Common.FieldSeparator);
    sb.Append(headerInfo.PositionZ.ToString(NumberFormatInfo.InvariantInfo));
    sb.Append(Utils.Common.FieldSeparator);
    sb.Append(headerInfo.IsRuigee);
    sb.Append(Utils.Common.FieldSeparator);
    sb.Append(headerInfo.IsHardest);
    sb.Append(Utils.Common.FieldSeparator);
    sb.Append(headerInfo.IsFrameone);
    sb.Append(Utils.Common.FieldSeparator);
    sb.Append(headerInfo.IsPushrock);
    sb.Append(Utils.Common.FieldSeparator);
    sb.Append(headerInfo.IsMorefarm);
    sb.Append(Utils.Common.FieldSeparator);
    sb.Append(headerInfo.IsMystery);
    sb.Append(Utils.Common.FieldSeparator);
    sb.Append(headerInfo.Filename);

    return sb.ToString();
  }

  public void ParseFromSaveLine(string saveLine)
  {
    string[] data = saveLine.Split(Utils.Common.FieldSeparator);

    if (data.Length != 10)
    {
      throw new Exception(nameof(Header) + " is in an invalid format");
    }

    HeaderInfo headerInfo = (HeaderInfo)Data;

    float floatOut = 0;
    if (!float.TryParse(data[0], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out floatOut))
    {
      throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.PositionX) + " failed to parse");
    }

    headerInfo.PositionX = floatOut;
    if (!float.TryParse(data[1], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out floatOut))
    {
      throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.PositionY) + " failed to parse");
    }

    headerInfo.PositionY = floatOut;
    if (!float.TryParse(data[2], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out floatOut))
    {
      throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.PositionZ) + " failed to parse");
    }

    headerInfo.PositionZ = floatOut;

    bool boolOut = false;
    if (!bool.TryParse(data[3], out boolOut))
    {
      throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsRuigee) + " failed to parse");
    }

    headerInfo.IsRuigee = boolOut;
    if (!bool.TryParse(data[4], out boolOut))
    {
      throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsHardest) + " failed to parse");
    }

    headerInfo.IsHardest = boolOut;
    if (!bool.TryParse(data[5], out boolOut))
    {
      throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsFrameone) +
                          " failed to parse");
    }

    headerInfo.IsFrameone = boolOut;
    if (!bool.TryParse(data[6], out boolOut))
    {
      throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsPushrock) +
                          " failed to parse");
    }

    headerInfo.IsPushrock = boolOut;
    if (!bool.TryParse(data[7], out boolOut))
    {
      throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsMorefarm) +
                          " failed to parse");
    }

    headerInfo.IsMorefarm = boolOut;
    if (!bool.TryParse(data[8], out boolOut))
    {
      throw new Exception(nameof(Header) + "." + nameof(HeaderInfo.IsMystery) + " failed to parse");
    }

    headerInfo.IsMystery = boolOut;

    headerInfo.Filename = data[9];
  }

  public void ResetToDefault()
  {
    HeaderInfo headerInfo = (HeaderInfo)Data;

    headerInfo.PositionX = 0f;
    headerInfo.PositionY = 0f;
    headerInfo.PositionZ = 0f;
    headerInfo.IsRuigee = false;
    headerInfo.IsHardest = false;
    headerInfo.IsFrameone = false;
    headerInfo.IsPushrock = false;
    headerInfo.IsMorefarm = false;
    headerInfo.IsMystery = false;
    headerInfo.Filename = "";
  }

  public class HeaderInfo : INotifyPropertyChanged
  {
    private string _filename = "";

    private bool _isFrameone;

    private bool _isHardest;

    private bool _isMorefarm;

    private bool _isMystery;

    private bool _isPushrock;

    private bool _isRuigee;
    private float _positionX;

    private float _positionY;

    private float _positionZ;

    public float PositionX
    {
      get => _positionX;
      set
      {
        _positionX = value;
        NotifyPropertyChanged();
      }
    }

    public float PositionY
    {
      get => _positionY;
      set
      {
        _positionY = value;
        NotifyPropertyChanged();
      }
    }

    public float PositionZ
    {
      get => _positionZ;
      set
      {
        _positionZ = value;
        NotifyPropertyChanged();
      }
    }

    public bool IsRuigee
    {
      get => _isRuigee;
      set
      {
        _isRuigee = value;
        NotifyPropertyChanged();
      }
    }

    public bool IsHardest
    {
      get => _isHardest;
      set
      {
        _isHardest = value;
        NotifyPropertyChanged();
      }
    }

    public bool IsFrameone
    {
      get => _isFrameone;
      set
      {
        _isFrameone = value;
        NotifyPropertyChanged();
      }
    }

    public bool IsPushrock
    {
      get => _isPushrock;
      set
      {
        _isPushrock = value;
        NotifyPropertyChanged();
      }
    }

    public bool IsMorefarm
    {
      get => _isMorefarm;
      set
      {
        _isMorefarm = value;
        NotifyPropertyChanged();
      }
    }

    public bool IsMystery
    {
      get => _isMystery;
      set
      {
        _isMystery = value;
        NotifyPropertyChanged();
      }
    }

    public string Filename
    {
      get => _filename;
      set
      {
        _filename = value;
        NotifyPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
