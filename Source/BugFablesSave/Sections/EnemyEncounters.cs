using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class EnemyEncounters : BugFablesDataList<EnemyEncounters.EnemyEncounterInfo>
{
  public EnemyEncounters()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  public sealed class EnemyEncounterInfo : BugFablesData, INotifyPropertyChanged
  {
    private int _nbrDefeated;
    private int _nbrSeen;

    public int NbrSeen
    {
      get => _nbrSeen;
      set
      {
        _nbrSeen = value;
        NotifyPropertyChanged();
      }
    }

    public int NbrDefeated
    {
      get => _nbrDefeated;
      set
      {
        _nbrDefeated = value;
        NotifyPropertyChanged();
      }
    }

    public override void ResetToDefault()
    {
      NbrDefeated = 0;
      NbrSeen = 0;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(Utils.PrimarySeparator);
      if (data.Length != 2)
        throw new Exception($"Expected 2 fields, but got {data.Length}");

      NbrSeen = ParseField<int>(data[0], nameof(NbrSeen));
      NbrDefeated = ParseField<int>(data[1], nameof(NbrDefeated));
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append(NbrSeen);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(NbrDefeated);

      return sb.ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
