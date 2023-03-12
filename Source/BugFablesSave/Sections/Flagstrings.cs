using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Flagstrings : BugFablesDataList<Flagstrings.FlagstringInfo>
{
  private readonly string[][] _flagstringsInfo;

  public Flagstrings()
  {
    ElementSeparator = "|SPLIT|";
    _flagstringsInfo = LoadAdditionalData("FlagsData/Flagstrings.csv");
  }

  public override void Parse(string str)
  {
    base.Parse(str);
    for (int i = 0; i < List.Count; i++)
      List[i].Index = i;
  }

  protected override void MergeAdditionalData()
  {
    for (int i = 0; i < _flagstringsInfo.Length; i++)
      List[i].Description = _flagstringsInfo[i][1].Replace('~', '\n');
  }

  public sealed class FlagstringInfo : BugFablesData, INotifyPropertyChanged
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

    public override void ResetToDefault()
    {
      Str = string.Empty;
    }

    public override void Parse(string str)
    {
      Str = str;
    }

    public override string ToString()
    {
      return Str;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
