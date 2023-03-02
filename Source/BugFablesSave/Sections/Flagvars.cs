using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public class Flagvars : IBugFablesSaveSection
{
  private const int NbrSlots = 70;

  public Flagvars()
  {
    string[] lines = File.ReadAllLines("FlagsData/Flagvars.csv");

    string[][] data = new string[lines.Length][];
    for (int i = 0; i < lines.Length; i++)
    {
      data[i] = lines[i].Split(';');
    }

    FlagvarInfo[] array = (FlagvarInfo[])Data;
    for (int i = 0; i < array.Length; i++)
    {
      array[i] = new FlagvarInfo { Index = i, Description = data[i][1].Replace('~', '\n') };
    }
  }

  public object Data { get; set; } = new FlagvarInfo[NbrSlots];

  public string EncodeToSaveLine()
  {
    FlagvarInfo[] flagvars = (FlagvarInfo[])Data;
    StringBuilder sb = new();

    for (int i = 0; i < flagvars.Length; i++)
    {
      sb.Append(flagvars[i].Var);

      if (i != flagvars.Length - 1)
      {
        sb.Append(Common.FieldSeparator);
      }
    }

    return sb.ToString();
  }

  public void ParseFromSaveLine(string saveLine)
  {
    string[] flagsvarsData = saveLine.Split(Common.FieldSeparator);
    if (flagsvarsData.Length != NbrSlots)
    {
      throw new Exception(nameof(Flagvars) + " is in an invalid format");
    }

    FlagvarInfo[] flagsvars = (FlagvarInfo[])Data;

    for (int i = 0; i < flagsvarsData.Length; i++)
    {
      int intOut = 0;
      if (!int.TryParse(flagsvarsData[i], out intOut))
      {
        throw new Exception(nameof(Flagvars) + "[" + i + "] failed to parse");
      }

      flagsvars[i].Var = intOut;
    }
  }

  public void ResetToDefault()
  {
    FlagvarInfo[] flagvars = (FlagvarInfo[])Data;
    foreach (FlagvarInfo flagvar in flagvars)
    {
      flagvar.Var = 0;
    }
  }

  public class FlagvarInfo : INotifyPropertyChanged
  {
    private string _description = "";
    private int _index;

    private int _var;

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

    public int Var
    {
      get => _var;
      set
      {
        _var = value;
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
