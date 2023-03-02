using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.BugFablesEnums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public class Medals : IBugFablesSaveSection
{
  public object Data { get; set; } = new ObservableCollection<MedalInfo>();

  public void ParseFromSaveLine(string saveLine)
  {
    string[] medalsData = saveLine.Split(Common.ElementSeparator);
    ObservableCollection<MedalInfo> medals = (ObservableCollection<MedalInfo>)Data;

    for (int i = 0; i < medalsData.Length; i++)
    {
      if (medalsData[i] == string.Empty)
      {
        continue;
      }

      string[] data = medalsData[i].Split(Common.FieldSeparator);
      if (data.Length != 2)
        throw new Exception($"The medal at index {i} is not well formatted");

      MedalInfo newMedalEquip = new();

      int intOut = 0;
      if (!int.TryParse(data[0], out intOut))
      {
        throw new Exception(nameof(Medals) + "[" + i + "]." + nameof(MedalInfo.Medal) + " failed to parse");
      }

      if (intOut < 0 || intOut >= (int)Medal.COUNT)
      {
        throw new Exception(nameof(Medals) + "[" + i + "]." + nameof(MedalInfo.Medal) + ": " + intOut +
                            " is not a valid medal ID");
      }

      newMedalEquip.Medal = (Medal)intOut;
      if (!int.TryParse(data[1], out intOut))
      {
        throw new Exception(nameof(Medals) + "[" + i + "]." + nameof(MedalInfo.MedalEquipTarget) + " failed to parse");
      }

      // Convert from save to our enum
      intOut += 2;
      if (intOut < 0 || intOut >= (int)MedalEquipTarget.COUNT)
      {
        throw new Exception(nameof(Medals) + "[" + i + "]." + nameof(MedalInfo.MedalEquipTarget) + ": " + intOut +
                            " is not a valid medal equip target value");
      }

      newMedalEquip.MedalEquipTarget = (MedalEquipTarget)intOut;

      medals.Add(newMedalEquip);
    }
  }

  public string EncodeToSaveLine()
  {
    ObservableCollection<MedalInfo> medals = (ObservableCollection<MedalInfo>)Data;
    StringBuilder sb = new();

    for (int i = 0; i < medals.Count; i++)
    {
      sb.Append((int)medals[i].Medal);
      sb.Append(Common.FieldSeparator);
      // the -2 is to convert from our enum to save
      sb.Append((int)medals[i].MedalEquipTarget - 2);

      if (i != medals.Count - 1)
      {
        sb.Append(Common.ElementSeparator);
      }
    }

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    ObservableCollection<MedalInfo> medals = (ObservableCollection<MedalInfo>)Data;
    medals.Clear();
  }

  public class MedalInfo : INotifyPropertyChanged
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
