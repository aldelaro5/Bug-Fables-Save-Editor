using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Header : BugFablesData
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

  public override void Parse(string str)
  {
    string[] data = str.Split(Utils.PrimarySeparator);
    if (data.Length != 10)
      throw new Exception(nameof(Header) + " is in an invalid format");

    PositionX = ParseField<float>(data[0], nameof(PositionX));
    PositionY = ParseField<float>(data[1], nameof(PositionY));
    PositionZ = ParseField<float>(data[2], nameof(PositionZ));
    IsRuigee = ParseField<bool>(data[3], nameof(IsRuigee));
    IsHardest = ParseField<bool>(data[4], nameof(IsHardest));;
    IsFrameone = ParseField<bool>(data[5], nameof(IsFrameone));;
    IsPushrock = ParseField<bool>(data[6], nameof(IsPushrock));;
    IsMorefarm = ParseField<bool>(data[7], nameof(IsMorefarm));;
    IsMystery = ParseField<bool>(data[8], nameof(IsMystery));;
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

  public event PropertyChangedEventHandler? PropertyChanged;

  private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
