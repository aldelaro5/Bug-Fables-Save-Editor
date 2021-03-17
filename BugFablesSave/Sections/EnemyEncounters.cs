using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class EnemyEncounters : IBugFablesSaveSection
  {
    private const int nbrSlots = 256;

    public class EnemyEncounterInfo : INotifyPropertyChanged
    {
      private int _nbrSeen;
      public int NbrSeen { get { return _nbrSeen; } set { _nbrSeen = value; NotifyPropertyChanged(); } }

      private int _nbrDefeated;
      public int NbrDefeated { get { return _nbrDefeated; } set { _nbrDefeated = value; NotifyPropertyChanged(); } }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public object Data { get; set; } = new EnemyEncounterInfo[nbrSlots];

    public EnemyEncounters()
    {
      EnemyEncounterInfo[] encounterList = (EnemyEncounterInfo[])Data;

      for (int i = 0; i < encounterList.Length; i++)
        encounterList[i] = new EnemyEncounterInfo();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] enemyEncountersData = saveLine.Split(Common.ElementSeparator);
      if (enemyEncountersData.Length != nbrSlots)
        throw new Exception(nameof(EnemyEncounters) + " is in an invalid format");

      EnemyEncounterInfo[] enemyEncounters = (EnemyEncounterInfo[])Data;

      for (int i = 0; i < enemyEncountersData.Length; i++)
      {
        string[] data = enemyEncountersData[i].Split(Common.FieldSeparator);

        EnemyEncounterInfo newEnemyEncounterInfo = new EnemyEncounterInfo();

        int intOut = 0;
        if (!int.TryParse(data[0], out intOut))
          throw new Exception(nameof(EnemyEncounters) + "[" + i + "]." + nameof(EnemyEncounterInfo.NbrSeen) + " failed to parse");
        newEnemyEncounterInfo.NbrSeen = intOut;
        if (!int.TryParse(data[1], out intOut))
          throw new Exception(nameof(EnemyEncounters) + "[" + i + "]." + nameof(EnemyEncounterInfo.NbrDefeated) + " failed to parse");
        newEnemyEncounterInfo.NbrDefeated = intOut;

        enemyEncounters[i] = newEnemyEncounterInfo;
      }
    }

    public string EncodeToSaveLine()
    {
      EnemyEncounterInfo[] enemyEncounters = (EnemyEncounterInfo[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < enemyEncounters.Length; i++)
      {
        sb.Append(enemyEncounters[i].NbrSeen);
        sb.Append(Common.FieldSeparator);
        sb.Append(enemyEncounters[i].NbrDefeated);

        if (i != enemyEncounters.Length - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }
  }
}
