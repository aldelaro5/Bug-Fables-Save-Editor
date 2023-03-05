using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.Utils;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public class Global : IBugFablesSaveSection
{
  public object Data { get; set; } = new GlobalInfo();

  public string EncodeToSaveLine()
  {
    GlobalInfo globalInfo = (GlobalInfo)Data;
    StringBuilder sb = new();

    sb.Append(globalInfo.Rank);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.Exp);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.NeededExp);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.MaxTP);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.TP);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.BerryCount);
    sb.Append(Common.FieldSeparator);
    sb.Append((int)globalInfo.CurrentMap);
    sb.Append(Common.FieldSeparator);
    sb.Append((int)globalInfo.CurrentArea);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.MP);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.MaxMP);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.NbrMaxItemsInventory);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.NbrMaxItemsStorage);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.PlayTimeHours);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.PlayTimeMinutes);
    sb.Append(Common.FieldSeparator);
    sb.Append(globalInfo.PlayTimeSeconds);
    sb.Append(Common.FieldSeparator);
    sb.Append((int)globalInfo.SaveProgressIcons);

    return sb.ToString();
  }

  public void ParseFromSaveLine(string saveLine)
  {
    string[] data = saveLine.Split(Common.FieldSeparator);

    if (data.Length != 16)
    {
      throw new Exception(nameof(GlobalInfo) + " is in an invalid format");
    }

    GlobalInfo globalInfo = (GlobalInfo)Data;

    int intOut = 0;
    if (!int.TryParse(data[0], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.Rank) + " failed to parse");
    }

    globalInfo.Rank = intOut;
    if (!int.TryParse(data[1], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.Exp) + " failed to parse");
    }

    globalInfo.Exp = intOut;
    if (!int.TryParse(data[2], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.NeededExp) + " failed to parse");
    }

    globalInfo.NeededExp = intOut;
    if (!int.TryParse(data[3], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.MaxTP) + " failed to parse");
    }

    globalInfo.MaxTP = intOut;
    if (!int.TryParse(data[4], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.TP) + " failed to parse");
    }

    globalInfo.TP = intOut;
    if (!int.TryParse(data[5], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.BerryCount) + " failed to parse");
    }

    globalInfo.BerryCount = intOut;

    if (!int.TryParse(data[6], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.CurrentMap) + " failed to parse");
    }

    if (intOut < 0 || intOut >= (int)Map.COUNT)
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.CurrentMap) + ": " +
                          intOut + " is not a valid map ID");
    }

    globalInfo.CurrentMap = (Map)intOut;

    if (!int.TryParse(data[7], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.CurrentArea) + " failed to parse");
    }

    if (intOut < 0 || intOut >= (int)Area.COUNT)
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.CurrentArea) + ": " +
                          intOut + " is not a valid area ID");
    }

    globalInfo.CurrentArea = (Area)intOut;

    if (!int.TryParse(data[8], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.MP) + " failed to parse");
    }

    globalInfo.MP = intOut;
    if (!int.TryParse(data[9], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.MaxMP) + " failed to parse");
    }

    globalInfo.MaxMP = intOut;
    if (!int.TryParse(data[10], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.NbrMaxItemsInventory) + " failed to parse");
    }

    globalInfo.NbrMaxItemsInventory = intOut;
    if (!int.TryParse(data[11], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.NbrMaxItemsStorage) + " failed to parse");
    }

    globalInfo.NbrMaxItemsStorage = intOut;
    if (!int.TryParse(data[12], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.PlayTimeHours) + " failed to parse");
    }

    globalInfo.PlayTimeHours = intOut;
    if (!int.TryParse(data[13], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.PlayTimeMinutes) + " failed to parse");
    }

    globalInfo.PlayTimeMinutes = intOut;
    if (!int.TryParse(data[14], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.PlayTimeSeconds) + " failed to parse");
    }

    globalInfo.PlayTimeSeconds = intOut;

    if (!int.TryParse(data[15], out intOut))
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.SaveProgressIcons) + " failed to parse");
    }

    if (intOut < 0 || intOut >= (int)SaveProgressIcon.COUNT)
    {
      throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.SaveProgressIcons) + ": " +
                          intOut + " is not a valid save progress icon value");
    }

    globalInfo.SaveProgressIcons = (SaveProgressIcon)intOut;
  }

  public void ResetToDefault()
  {
    GlobalInfo globalInfo = (GlobalInfo)Data;

    globalInfo.Rank = 0;
    globalInfo.Exp = 0;
    globalInfo.NeededExp = 0;
    globalInfo.MaxTP = 0;
    globalInfo.TP = 0;
    globalInfo.BerryCount = 0;
    globalInfo.CurrentMap = 0;
    globalInfo.CurrentArea = 0;
    globalInfo.MP = 0;
    globalInfo.MaxMP = 0;
    globalInfo.NbrMaxItemsInventory = 0;
    globalInfo.NbrMaxItemsStorage = 0;
    globalInfo.PlayTimeHours = 0;
    globalInfo.PlayTimeMinutes = 0;
    globalInfo.PlayTimeSeconds = 0;
    globalInfo.SaveProgressIcons = 0;
  }

  public class GlobalInfo : INotifyPropertyChanged
  {
    private int _berryCount;
    private Area _currentArea;

    private Map _currentMap;
    private int _exp;
    private int _maxMp;

    private int _maxTp;

    private int _mp;

    private int _nbrMaxItemsInventory;
    private int _nbrMaxItemsStoreage;
    private int _neededExp;

    private int _playTimeHours;
    private int _playTimeMinutes;
    private int _playTimeSeconds;
    private int _rank;

    private SaveProgressIcon _saveProgressIcon;
    private int _tp;

    public int Rank
    {
      get => _rank;
      set
      {
        _rank = value;
        NotifyPropertyChanged();
      }
    }

    public int Exp
    {
      get => _exp;
      set
      {
        _exp = value;
        NotifyPropertyChanged();
      }
    }

    public int NeededExp
    {
      get => _neededExp;
      set
      {
        _neededExp = value;
        NotifyPropertyChanged();
      }
    }

    public int MaxTP
    {
      get => _maxTp;
      set
      {
        _maxTp = value;
        NotifyPropertyChanged();
      }
    }

    public int TP
    {
      get => _tp;
      set
      {
        _tp = value;
        NotifyPropertyChanged();
      }
    }

    public int BerryCount
    {
      get => _berryCount;
      set
      {
        _berryCount = value;
        NotifyPropertyChanged();
      }
    }

    public Map CurrentMap
    {
      get => _currentMap;
      set
      {
        _currentMap = value;
        NotifyPropertyChanged();
      }
    }

    public Area CurrentArea
    {
      get => _currentArea;
      set
      {
        _currentArea = value;
        NotifyPropertyChanged();
      }
    }

    public int MP
    {
      get => _mp;
      set
      {
        _mp = value;
        NotifyPropertyChanged();
      }
    }

    public int MaxMP
    {
      get => _maxMp;
      set
      {
        _maxMp = value;
        NotifyPropertyChanged();
      }
    }

    public int NbrMaxItemsInventory
    {
      get => _nbrMaxItemsInventory;
      set
      {
        _nbrMaxItemsInventory = value;
        NotifyPropertyChanged();
      }
    }

    public int NbrMaxItemsStorage
    {
      get => _nbrMaxItemsStoreage;
      set
      {
        _nbrMaxItemsStoreage = value;
        NotifyPropertyChanged();
      }
    }

    public int PlayTimeHours
    {
      get => _playTimeHours;
      set
      {
        _playTimeHours = value;
        NotifyPropertyChanged();
      }
    }

    public int PlayTimeMinutes
    {
      get => _playTimeMinutes;
      set
      {
        _playTimeMinutes = value;
        NotifyPropertyChanged();
      }
    }

    public int PlayTimeSeconds
    {
      get => _playTimeSeconds;
      set
      {
        _playTimeSeconds = value;
        NotifyPropertyChanged();
      }
    }

    public SaveProgressIcon SaveProgressIcons
    {
      get => _saveProgressIcon;
      set
      {
        _saveProgressIcon = value;
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
