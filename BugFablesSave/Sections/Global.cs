using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Global : IBugFablesSaveSection
  {
    public class GlobalInfo : INotifyPropertyChanged
    {
      private int _rank;
      public int Rank { get { return _rank; } set { _rank = value; NotifyPropertyChanged(); } }
      private int _exp;
      public int Exp { get { return _exp; } set { _exp = value; NotifyPropertyChanged(); } }
      private int _neededExp;
      public int NeededExp { get { return _neededExp; } set { _neededExp = value; NotifyPropertyChanged(); } }

      private int _maxTp;
      public int MaxTP { get { return _maxTp; } set { _maxTp = value; NotifyPropertyChanged(); } }
      private int _tp;
      public int TP { get { return _tp; } set { _tp = value; NotifyPropertyChanged(); } }

      private int _berryCount;
      public int BerryCount { get { return _berryCount; } set { _berryCount = value; NotifyPropertyChanged(); } }

      private Map _currentMap;
      public Map CurrentMap { get { return _currentMap; } set { _currentMap = value; NotifyPropertyChanged(); } }
      private Area _currentArea;
      public Area CurrentArea { get { return _currentArea; } set { _currentArea = value; NotifyPropertyChanged(); } }

      private int _mp;
      public int MP { get { return _mp; } set { _mp = value; NotifyPropertyChanged(); } }
      private int _maxMp;
      public int MaxMP { get { return _maxMp; } set { _maxMp = value; NotifyPropertyChanged(); } }

      private int _nbrMaxItemsInventory;
      public int NbrMaxItemsInventory { get { return _nbrMaxItemsInventory; } set { _nbrMaxItemsInventory = value; NotifyPropertyChanged(); } }
      private int _nbrMaxItemsStoreage;
      public int NbrMaxItemsStorage { get { return _nbrMaxItemsStoreage; } set { _nbrMaxItemsStoreage = value; NotifyPropertyChanged(); } }

      private int _playTimeHours;
      public int PlayTimeHours { get { return _playTimeHours; } set { _playTimeHours = value; NotifyPropertyChanged(); } }
      private int _playTimeMinutes;
      public int PlayTimeMinutes { get { return _playTimeMinutes; } set { _playTimeMinutes = value; NotifyPropertyChanged(); } }
      private int _playTimeSeconds;
      public int PlayTimeSeconds { get { return _playTimeSeconds; } set { _playTimeSeconds = value; NotifyPropertyChanged(); } }

      private SaveProgressIcon _saveProgressIcon;
      public SaveProgressIcon SaveProgressIcons { get { return _saveProgressIcon; } set { _saveProgressIcon = value; NotifyPropertyChanged(); } }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public object Data { get; set; } = new GlobalInfo();

    public string EncodeToSaveLine()
    {
      GlobalInfo globalInfo = (GlobalInfo)Data;
      StringBuilder sb = new StringBuilder();

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
        throw new Exception(nameof(GlobalInfo) + " is in an invalid format");

      GlobalInfo globalInfo = (GlobalInfo)Data;

      int intOut = 0;
      if (!int.TryParse(data[0], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.Rank) + " failed to parse");
      globalInfo.Rank = intOut;
      if (!int.TryParse(data[1], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.Exp) + " failed to parse");
      globalInfo.Exp = intOut;
      if (!int.TryParse(data[2], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.NeededExp) + " failed to parse");
      globalInfo.NeededExp = intOut;
      if (!int.TryParse(data[3], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.MaxTP) + " failed to parse");
      globalInfo.MaxTP = intOut;
      if (!int.TryParse(data[4], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.TP) + " failed to parse");
      globalInfo.TP = intOut;
      if (!int.TryParse(data[5], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.BerryCount) + " failed to parse");
      globalInfo.BerryCount = intOut;

      if (!int.TryParse(data[6], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.CurrentMap) + " failed to parse");
      if (intOut < 0 || intOut >= (int)Map.COUNT)
      {
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.CurrentMap) + ": " +
                            intOut + " is not a valid map ID");
      }
      globalInfo.CurrentMap = (Map)intOut;

      if (!int.TryParse(data[7], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.CurrentArea) + " failed to parse");
      if (intOut < 0 || intOut >= (int)Area.COUNT)
      {
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.CurrentArea) + ": " +
                            intOut + " is not a valid area ID");
      }
      globalInfo.CurrentArea = (Area)intOut;

      if (!int.TryParse(data[8], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.MP) + " failed to parse");
      globalInfo.MP = intOut;
      if (!int.TryParse(data[9], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.MaxMP) + " failed to parse");
      globalInfo.MaxMP = intOut;
      if (!int.TryParse(data[10], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.NbrMaxItemsInventory) + " failed to parse");
      globalInfo.NbrMaxItemsInventory = intOut;
      if (!int.TryParse(data[11], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.NbrMaxItemsStorage) + " failed to parse");
      globalInfo.NbrMaxItemsStorage = intOut;
      if (!int.TryParse(data[12], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.PlayTimeHours) + " failed to parse");
      globalInfo.PlayTimeHours = intOut;
      if (!int.TryParse(data[13], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.PlayTimeMinutes) + " failed to parse");
      globalInfo.PlayTimeMinutes = intOut;
      if (!int.TryParse(data[14], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.PlayTimeSeconds) + " failed to parse");
      globalInfo.PlayTimeSeconds = intOut;

      if (!int.TryParse(data[15], out intOut))
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.SaveProgressIcons) + " failed to parse");
      if (intOut < 0 || intOut >= (int)SaveProgressIcon.COUNT)
      {
        throw new Exception(nameof(GlobalInfo) + "." + nameof(GlobalInfo.SaveProgressIcons) + ": " +
                            intOut + " is not a valid save progress icon value");
      }
      globalInfo.SaveProgressIcons = (SaveProgressIcon)intOut;
    }
  }
}
