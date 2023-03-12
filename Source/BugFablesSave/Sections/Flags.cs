using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Flags : BugFablesDataList<Flags.FlagInfo>
{
  private readonly string[][] _flagsData;

  public Flags()
  {
    _flagsData = LoadAdditionalData("FlagsData/Flags.csv");
  }

  public override void Parse(string str)
  {
    base.Parse(str);
    for (int i = 0; i < List.Count; i++)
      List[i].Index = i;
  }

  protected override void MergeAdditionalData()
  {
    for (int i = 0; i < _flagsData.Length; i++)
      List[i].Description = _flagsData[i][1].Replace('~', '\n');
  }

  public sealed class FlagInfo : BugFablesData, INotifyPropertyChanged
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
