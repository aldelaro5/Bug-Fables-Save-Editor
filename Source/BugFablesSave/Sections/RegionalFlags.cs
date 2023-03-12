using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class RegionalFlags : BugFablesDataList<RegionalFlags.RegionalInfo>
{
  private readonly string[][] _descriptions = new string[(int)Area.COUNT][];

  public RegionalFlags()
  {
    string[] areas = Enum.GetNames(typeof(Area));
    // Don't consider the last one which is the COUNT
    for (int i = 0; i < areas.Length - 1; i++)
    {
      string[][] lines = LoadAdditionalData($"FlagsData/Regionals/{areas[i]}.csv");
      _descriptions[i] = new string[lines.Length];
      Array.Fill(_descriptions[i], "UNUSED");

      for (int j = 0; j < lines.Length; j++)
        _descriptions[i][j] = lines[j][1].Replace('~', '\n');
    }
    List.Add(new RegionalInfo());
  }

  public void ChangeCurrentRegionalsArea(Area area)
  {
    for (int i = 0; i < _descriptions[1].Length; i++)
    {
      if (i >= List.Count)
        break;

      List[i].Description = _descriptions[(int)area][i];
    }
  }

  public sealed class RegionalInfo : BugFablesData, INotifyPropertyChanged
  {
    private string _description = "";

    private bool _enabled;
    private int _index;

    public int Index
    {
      get => _index;
      set
      {
        _index = value;
        NotifyPropertyChanged();
      }
    }

    public string Description
    {
      get => _description;
      set
      {
        _description = value;
        NotifyPropertyChanged();
      }
    }

    public bool Enabled
    {
      get => _enabled;
      set
      {
        _enabled = value;
        NotifyPropertyChanged();
      }
    }

    public override void ResetToDefault()
    {
      Enabled = false;
    }

    public override void Parse(string str)
    {
      Enabled = ParseField<bool>(str, nameof(Enabled));
    }

    public override string ToString()
    {
      return Enabled.ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
