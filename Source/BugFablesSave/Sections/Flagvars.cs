using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Flagvars : BugFablesDataList<Flagvars.FlagvarInfo>
{
  private readonly string[][] _flagvarssInfo;

  public Flagvars()
  {
    _flagvarssInfo = LoadAdditionalData("FlagsData/Flagvars.csv");
  }

  public override void Parse(string str)
  {
    base.Parse(str);
    for (int i = 0; i < List.Count; i++)
      List[i].Index = i;
  }

  protected override void MergeAdditionalData()
  {
    for (int i = 0; i < _flagvarssInfo.Length; i++)
    {
      List[i].Description = _flagvarssInfo[i][1].Replace('~', '\n');
    }
  }

  public sealed class FlagvarInfo : BugFablesData, INotifyPropertyChanged
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

    public override void ResetToDefault()
    {
      Var = 0;
    }

    public override void Parse(string str)
    {
      Var = ParseField<int>(str, nameof(Var));
    }

    public override string ToString()
    {
      return Var.ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
