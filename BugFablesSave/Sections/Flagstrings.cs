using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Flagstrings : IBugFablesSaveSection
  {
    public class FlagstringInfo : INotifyPropertyChanged
    {
      private int _index;
      public int Index
      {
        get { return _index; }
        set { _index = value; NotifyPropertyChanged(); }
      }

      private string _description = "";
      public string Description
      {
        get { return _description; }
        set { _description = value; NotifyPropertyChanged(); }
      }

      private string _str;
      public string Str
      {
        get { return _str; }
        set { _str = value; NotifyPropertyChanged(); }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    private const int NbrSlots = 15;
    private const string separator = "|SPLIT|";

    public object Data { get; set; } = new FlagstringInfo[NbrSlots];

    public Flagstrings()
    {
      string[] lines = File.ReadAllLines("Data/Flagstrings.csv");

      string[][] data = new string[lines.Length][];
      for (int i = 0; i < lines.Length; i++)
        data[i] = lines[i].Split(';');

      var array = (FlagstringInfo[])Data;
      for (int i = 0; i < array.Length; i++)
      {
        array[i] = new FlagstringInfo { Index = i, Description = data[i][1].Replace('~', '\n') };
      }
    }

    public string EncodeToSaveLine()
    {
      FlagstringInfo[] flagstrings = (FlagstringInfo[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < flagstrings.Length; i++)
      {
        sb.Append(flagstrings[i].Str);

        if (i != flagstrings.Length - 1)
          sb.Append(separator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] flagstringData = saveLine.Split(separator);
      if (flagstringData.Length != NbrSlots)
        throw new Exception(nameof(Flagstrings) + " is in an invalid format");

      FlagstringInfo[] flagstrings = (FlagstringInfo[])Data;

      for (int i = 0; i < flagstringData.Length; i++)
        flagstrings[i].Str = flagstringData[i];
    }
  }
}
