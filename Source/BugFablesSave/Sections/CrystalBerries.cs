using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class CrystalBerries : BugFablesDataList<CrystalBerries.CrystalBerry>
{
  private readonly string[][] _crystalBerriesData;

  public CrystalBerries()
  {
    _crystalBerriesData = LoadAdditionalData("FlagsData/CrystalBerries.csv");
  }

  public override void Parse(string str)
  {
    base.Parse(str);
    for (int i = 0; i < List.Count; i++)
      List[i].Index = i;
  }

  protected override void MergeAdditionalData()
  {
    for (int i = 0; i < _crystalBerriesData.Length; i++)
    {
      List[i].Area = (Area)Enum.Parse(typeof(Area), _crystalBerriesData[i][1]);
      List[i].Description = _crystalBerriesData[i][2];
    }
  }

  public sealed class CrystalBerry : BugFablesData, INotifyPropertyChanged
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

    public override void ResetToDefault()
    {
      Obtained = false;
    }

    public override void Parse(string str)
    {
      Obtained = ParseField<bool>(str, nameof(Obtained));
    }

    public override string ToString()
    {
      return Obtained.ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
