using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.BugFablesEnums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public class CrystalBerries : IBugFablesSaveSection
{
  private const int NbrCrystalBerries = 50;

  public CrystalBerries()
  {
    string[] lines = File.ReadAllLines("FlagsData/CrystalBerries.csv");

    string[][] data = new string[lines.Length][];
    for (int i = 0; i < lines.Length; i++)
    {
      data[i] = lines[i].Split(';');
    }

    CrystalBerry[] array = (CrystalBerry[])Data;
    for (int i = 0; i < array.Length; i++)
    {
      CrystalBerry crystalBerry = new();
      crystalBerry.Index = i;
      crystalBerry.Area = (Area)Enum.Parse(typeof(Area), data[i][1]);
      crystalBerry.Description = data[i][2];
      array[i] = crystalBerry;
    }
  }

  public object Data { get; set; } = new CrystalBerry[NbrCrystalBerries];

  public string EncodeToSaveLine()
  {
    CrystalBerry[] crystalBerries = (CrystalBerry[])Data;
    StringBuilder sb = new();

    for (int i = 0; i < crystalBerries.Length; i++)
    {
      sb.Append(crystalBerries[i].Obtained);

      if (i != crystalBerries.Length - 1)
      {
        sb.Append(Common.FieldSeparator);
      }
    }

    return sb.ToString();
  }

  public void ParseFromSaveLine(string saveLine)
  {
    string[] crystalBerriesData = saveLine.Split(Common.FieldSeparator);
    if (crystalBerriesData.Length != NbrCrystalBerries)
    {
      throw new Exception(nameof(CrystalBerries) + " is in an invalid format");
    }

    CrystalBerry[] crystalBerries = (CrystalBerry[])Data;

    for (int i = 0; i < crystalBerriesData.Length; i++)
    {
      bool boolOut = false;
      if (!bool.TryParse(crystalBerriesData[i], out boolOut))
      {
        throw new Exception(nameof(CrystalBerries) + "[" + i + "] failed to parse");
      }

      crystalBerries[i].Obtained = boolOut;
    }
  }

  public void ResetToDefault()
  {
    CrystalBerry[] crystalBerries = (CrystalBerry[])Data;
    foreach (CrystalBerry cb in crystalBerries)
    {
      cb.Obtained = false;
    }
  }

  public class CrystalBerry : INotifyPropertyChanged
  {
    private Area _area;

    private string _description = "";
    private int _index;

    private bool _obtained;

    public int Index
    {
      get => _index;
      set
      {
        _index = value;
        NotifyPropertyChanged();
      }
    }

    public Area Area
    {
      get => _area;
      set
      {
        _area = value;
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

    public bool Obtained
    {
      get => _obtained;
      set
      {
        _obtained = value;
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
