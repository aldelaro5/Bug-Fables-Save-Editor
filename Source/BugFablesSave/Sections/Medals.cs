using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Medals : BugFablesDataList<Medals.MedalInfo>
{
  public Medals()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  public sealed class MedalInfo : BugFablesData, INotifyPropertyChanged
  {
    private Medal _medal;

    private MedalEquipTarget _medalEquipTarget;

    public Medal Medal
    {
      get => _medal;
      set
      {
        if ((int)value == -1)
        {
          return;
        }

        _medal = value;
        NotifyPropertyChanged();
      }
    }

    public MedalEquipTarget MedalEquipTarget
    {
      get => _medalEquipTarget;
      set
      {
        if ((int)value == -1)
        {
          return;
        }

        _medalEquipTarget = value;
        NotifyPropertyChanged();
      }
    }

    public override void ResetToDefault()
    {
      Medal = 0;
      MedalEquipTarget = 0;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(Utils.PrimarySeparator);

      Medal = (Medal)ParseField<int>(data[0], nameof(Medal));
      MedalEquipTarget = (MedalEquipTarget)ParseField<int>(data[1], nameof(MedalEquipTarget)) + 2;
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append((int)Medal);
      sb.Append(Utils.PrimarySeparator);
      // the -2 is to convert from our enum to save
      sb.Append((int)MedalEquipTarget - 2);

      return sb.ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
