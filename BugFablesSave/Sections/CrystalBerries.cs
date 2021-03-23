using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class CrystalBerries : IBugFablesSaveSection
  {
    public class CrystalBerry : INotifyPropertyChanged
    {
      private int _index;
      public int Index
      {
        get { return _index; }
        set { _index = value; NotifyPropertyChanged(); }
      }

      private Area _area;
      public Area Area
      {
        get { return _area; }
        set { _area = value; NotifyPropertyChanged(); }
      }

      private string _description = "";
      public string Description
      {
        get { return _description; }
        set { _description = value; NotifyPropertyChanged(); }
      }

      private bool _obtained;
      public bool Obtained
      {
        get { return _obtained; }
        set { _obtained = value; NotifyPropertyChanged(); }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    private const int NbrCrystalBerries = 50;

    public object Data { get; set; } = new CrystalBerry[NbrCrystalBerries];

    public CrystalBerries()
    {
      string[] lines = File.ReadAllLines("FlagsData/CrystalBerries.csv");

      string[][] data = new string[lines.Length][];
      for (int i = 0; i < lines.Length; i++)
        data[i] = lines[i].Split(';');

      var array = (CrystalBerry[])Data;
      for (int i = 0; i < array.Length; i++)
      {
        CrystalBerry crystalBerry = new CrystalBerry();
        crystalBerry.Index = i;
        crystalBerry.Area = (Area)Enum.Parse(typeof(Area), data[i][1]);
        crystalBerry.Description = data[i][2];
        array[i] = crystalBerry;
      }
    }

    public string EncodeToSaveLine()
    {
      CrystalBerry[] crystalBerries = (CrystalBerry[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < crystalBerries.Length; i++)
      {
        sb.Append(crystalBerries[i].Obtained);

        if (i != crystalBerries.Length - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] crystalBerriesData = saveLine.Split(Common.FieldSeparator);
      if (crystalBerriesData.Length != NbrCrystalBerries)
        throw new Exception(nameof(CrystalBerries) + " is in an invalid format");

      CrystalBerry[] crystalBerries = (CrystalBerry[])Data;

      for (int i = 0; i < crystalBerriesData.Length; i++)
      {
        bool boolOut = false;
        if (!bool.TryParse(crystalBerriesData[i], out boolOut))
          throw new Exception(nameof(CrystalBerries) + "[" + i + "] failed to parse");
        crystalBerries[i].Obtained = boolOut;
      }
    }

    public void ResetToDefault()
    {
      CrystalBerry[] crystalBerries = (CrystalBerry[])Data;
      foreach (var cb in crystalBerries)
        cb.Obtained = false;
    }
  }
}
