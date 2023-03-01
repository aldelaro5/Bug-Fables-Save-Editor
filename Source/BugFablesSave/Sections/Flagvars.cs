using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Flagvars : IBugFablesSaveSection
  {
    public class FlagvarInfo : INotifyPropertyChanged
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

      private int _var;
      public int Var
      {
        get { return _var; }
        set { _var = value; NotifyPropertyChanged(); }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    private const int NbrSlots = 70;

    public object Data { get; set; } = new FlagvarInfo[NbrSlots];

    public Flagvars()
    {
      string[] lines = File.ReadAllLines("FlagsData/Flagvars.csv");

      string[][] data = new string[lines.Length][];
      for (int i = 0; i < lines.Length; i++)
        data[i] = lines[i].Split(';');

      var array = (FlagvarInfo[])Data;
      for (int i = 0; i < array.Length; i++)
      {
        array[i] = new FlagvarInfo { Index = i, Description = data[i][1].Replace('~', '\n') };
      }
    }

    public string EncodeToSaveLine()
    {
      FlagvarInfo[] flagvars = (FlagvarInfo[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < flagvars.Length; i++)
      {
        sb.Append(flagvars[i].Var);

        if (i != flagvars.Length - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] flagsvarsData = saveLine.Split(Common.FieldSeparator);
      if (flagsvarsData.Length != NbrSlots)
        throw new Exception(nameof(Flagvars) + " is in an invalid format");

      FlagvarInfo[] flagsvars = (FlagvarInfo[])Data;

      for (int i = 0; i < flagsvarsData.Length; i++)
      {
        int intOut = 0;
        if (!int.TryParse(flagsvarsData[i], out intOut))
          throw new Exception(nameof(Flagvars) + "[" + i + "] failed to parse");
        flagsvars[i].Var = intOut;
      }
    }

    public void ResetToDefault()
    {
      FlagvarInfo[] flagvars = (FlagvarInfo[])Data;
      foreach (var flagvar in flagvars)
        flagvar.Var = 0;
    }
  }
}
