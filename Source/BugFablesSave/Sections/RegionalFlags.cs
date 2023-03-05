using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.Utils;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public class RegionalFlags : IBugFablesSaveSection
{
  private const int NbrSlots = 100;

  private readonly string[][] Descriptions = new string[(int)Area.COUNT][];

  public RegionalFlags()
  {
    string[] areas = Enum.GetNames(typeof(Area));
    // Don't consider the last one which is the COUNT
    for (int i = 0; i < areas.Length - 1; i++)
    {
      Descriptions[i] = new string[NbrSlots];
      Array.Fill(Descriptions[i], "UNUSED");

      string[] lines = File.ReadAllLines("FlagsData/Regionals/" + areas[i] + ".csv");
      string[][] data = new string[lines.Length][];
      for (int j = 0; j < lines.Length; j++)
      {
        data[j] = lines[j].Split(';');
      }

      for (int j = 0; j < data.Length; j++)
      {
        Descriptions[i][int.Parse(data[j][0])] = data[j][1].Replace('~', '\n');
      }
    }

    RegionalInfo[] array = (RegionalInfo[])Data;
    for (int i = 0; i < array.Length; i++)
    {
      array[i] = new RegionalInfo { Index = i, Description = "" };
    }
  }

  public object Data { get; set; } = new RegionalInfo[NbrSlots];

  public string EncodeToSaveLine()
  {
    RegionalInfo[] regionals = (RegionalInfo[])Data;
    StringBuilder sb = new();

    for (int i = 0; i < regionals.Length; i++)
    {
      sb.Append(regionals[i].Enabled);

      if (i != regionals.Length - 1)
      {
        sb.Append(Common.FieldSeparator);
      }
    }

    return sb.ToString();
  }

  public void ParseFromSaveLine(string saveLine)
  {
    string[] regionalFlagsData = saveLine.Split(Common.FieldSeparator);
    if (regionalFlagsData.Length != NbrSlots)
    {
      throw new Exception(nameof(RegionalFlags) + " is in an invalid format");
    }

    RegionalInfo[] regionalFlags = (RegionalInfo[])Data;

    for (int i = 0; i < regionalFlagsData.Length; i++)
    {
      bool boolOut = false;
      if (!bool.TryParse(regionalFlagsData[i], out boolOut))
      {
        throw new Exception(nameof(RegionalFlags) + "[" + i + "] failed to parse");
      }

      regionalFlags[i].Enabled = boolOut;
    }
  }

  public void ResetToDefault()
  {
    RegionalInfo[] regionalFlags = (RegionalInfo[])Data;
    foreach (RegionalInfo flag in regionalFlags)
    {
      flag.Enabled = false;
    }
  }

  public void ChangeCurrentRegionalsArea(Area area)
  {
    RegionalInfo[] regionals = (RegionalInfo[])Data;

    for (int i = 0; i < NbrSlots; i++)
    {
      regionals[i].Description = Descriptions[(int)area][i];
    }
  }

  public class RegionalInfo : INotifyPropertyChanged
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
