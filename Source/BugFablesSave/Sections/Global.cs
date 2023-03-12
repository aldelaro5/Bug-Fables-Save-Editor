using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Global : BugFablesData
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

  public override string ToString()
  {
    StringBuilder sb = new();

    sb.Append(Rank);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(Exp);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(NeededExp);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(MaxTP);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(TP);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(BerryCount);
    sb.Append(Utils.PrimarySeparator);
    sb.Append((int)CurrentMap);
    sb.Append(Utils.PrimarySeparator);
    sb.Append((int)CurrentArea);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(MP);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(MaxMP);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(NbrMaxItemsInventory);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(NbrMaxItemsStorage);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(PlayTimeHours);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(PlayTimeMinutes);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(PlayTimeSeconds);
    sb.Append(Utils.PrimarySeparator);
    sb.Append((int)SaveProgressIcons);

    return sb.ToString();
  }

  public override void Parse(string saveLine)
  {
    string[] data = saveLine.Split(Utils.PrimarySeparator);
    if (data.Length != 16)
      throw new Exception(nameof(Global) + " is in an invalid format");

    Rank = ParseField<int>(data[0], nameof(Rank));
    Exp = ParseField<int>(data[1], nameof(Exp));
    NeededExp = ParseField<int>(data[2], nameof(NeededExp));
    MaxTP = ParseField<int>(data[3], nameof(MaxTP));
    TP = ParseField<int>(data[4], nameof(TP));
    BerryCount = ParseField<int>(data[5], nameof(BerryCount));
    CurrentMap = (Map)ParseField<int>(data[6], nameof(CurrentMap));
    CurrentArea = (Area)ParseField<int>(data[7], nameof(CurrentArea));
    MP = ParseField<int>(data[8], nameof(MP));
    MaxMP = ParseField<int>(data[9], nameof(MaxMP));
    NbrMaxItemsInventory = ParseField<int>(data[10], nameof(NbrMaxItemsInventory));
    NbrMaxItemsStorage = ParseField<int>(data[11], nameof(NbrMaxItemsStorage));
    PlayTimeHours = ParseField<int>(data[12], nameof(PlayTimeHours));
    PlayTimeMinutes = ParseField<int>(data[13], nameof(PlayTimeMinutes));
    PlayTimeSeconds = ParseField<int>(data[14], nameof(PlayTimeSeconds));
    SaveProgressIcons = (SaveProgressIcon)ParseField<int>(data[15], nameof(SaveProgressIcons));
  }

  public override void ResetToDefault()
  {
    Rank = 0;
    Exp = 0;
    NeededExp = 0;
    MaxTP = 0;
    TP = 0;
    BerryCount = 0;
    CurrentMap = 0;
    CurrentArea = 0;
    MP = 0;
    MaxMP = 0;
    NbrMaxItemsInventory = 0;
    NbrMaxItemsStorage = 0;
    PlayTimeHours = 0;
    PlayTimeMinutes = 0;
    PlayTimeSeconds = 0;
    SaveProgressIcons = 0;
  }

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
