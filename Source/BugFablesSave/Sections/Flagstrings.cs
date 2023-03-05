using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public class Flagstrings : IBugFablesSaveSection
{
  private const int NbrSlots = 15;
  private const string separator = "|SPLIT|";

  public Flagstrings()
  {
    string[] lines = File.ReadAllLines("FlagsData/Flagstrings.csv");

    string[][] data = new string[lines.Length][];
    for (int i = 0; i < lines.Length; i++)
    {
      data[i] = lines[i].Split(';');
    }

    FlagstringInfo[] array = (FlagstringInfo[])Data;
    for (int i = 0; i < array.Length; i++)
    {
      array[i] = new FlagstringInfo { Index = i, Description = data[i][1].Replace('~', '\n') };
    }
  }

  public object Data { get; set; } = new FlagstringInfo[NbrSlots];

  public string EncodeToSaveLine()
  {
    FlagstringInfo[] flagstrings = (FlagstringInfo[])Data;
    StringBuilder sb = new();

    for (int i = 0; i < flagstrings.Length; i++)
    {
      sb.Append(flagstrings[i].Str);

      if (i != flagstrings.Length - 1)
      {
        sb.Append(separator);
      }
    }

    return sb.ToString();
  }

  public void ParseFromSaveLine(string saveLine)
  {
    string[] flagstringData = saveLine.Split(separator);
    if (flagstringData.Length != NbrSlots)
    {
      throw new Exception(nameof(Flagstrings) + " is in an invalid format");
    }

    FlagstringInfo[] flagstrings = (FlagstringInfo[])Data;

    for (int i = 0; i < flagstringData.Length; i++)
    {
      flagstrings[i].Str = flagstringData[i];
    }
  }

  public void ResetToDefault()
  {
    FlagstringInfo[] flagstrings = (FlagstringInfo[])Data;
    foreach (FlagstringInfo str in flagstrings)
    {
      str.Str = "";
    }
  }

  public class FlagstringInfo : INotifyPropertyChanged
  {
    private string _description = "";
    private int _index;

    private string _str = "";

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

    public string Str
    {
      get => _str;
      set
      {
        _str = value;
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
