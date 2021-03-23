using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Flags : IBugFablesSaveSection
  {
    public class FlagInfo : INotifyPropertyChanged
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

      private bool _enabled;
      public bool Enabled
      {
        get { return _enabled; }
        set { _enabled = value; NotifyPropertyChanged(); }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    private const int NbrSlots = 750;

    public object Data { get; set; } = new FlagInfo[NbrSlots];

    public Flags()
    {
      string[] lines = File.ReadAllLines("FlagsData/Flags.csv");

      string[][] data = new string[lines.Length][];
      for (int i = 0; i < lines.Length; i++)
        data[i] = lines[i].Split(';');

      var array = (FlagInfo[])Data;
      for (int i = 0; i < array.Length; i++)
      {
        array[i] = new FlagInfo { Index = i, Description = data[i][1].Replace('~', '\n') };
      }
    }

    public string EncodeToSaveLine()
    {
      FlagInfo[] flags = (FlagInfo[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < flags.Length; i++)
      {
        sb.Append(flags[i].Enabled);

        if (i != flags.Length - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] flagsData = saveLine.Split(Common.FieldSeparator);
      if (flagsData.Length != NbrSlots)
        throw new Exception(nameof(Flags) + " is in an invalid format");

      FlagInfo[] flags = (FlagInfo[])Data;

      for (int i = 0; i < flagsData.Length; i++)
      {
        bool boolOut = false;
        if (!bool.TryParse(flagsData[i], out boolOut))
          throw new Exception(nameof(Flags) + "[" + i + "] failed to parse");
        flags[i].Enabled = boolOut;
      }
    }

    public void ResetToDefault()
    {
      FlagInfo[] flags = (FlagInfo[])Data;
      foreach (var flag in flags)
        flag.Enabled = false;
    }
  }
}
