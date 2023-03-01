using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class MedalShopsPools : IBugFablesSaveSection
  {
    public class MedalShopPool : INotifyPropertyChanged
    {
      private Medal _medal;
      public Medal Medal
      {
        get { return _medal; }
        set
        {
          if ((int)value == -1)
            return;

          _medal = value;
          NotifyPropertyChanged();
        }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public object Data { get; set; } = new ObservableCollection<MedalShopPool>[(int)MedalShop.COUNT];

    public MedalShopsPools()
    {
      ObservableCollection<MedalShopPool>[] medals = (ObservableCollection<MedalShopPool>[])Data;

      for (int i = 0; i < medals.Length; i++)
        medals[i] = new ObservableCollection<MedalShopPool>();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] medalPools = saveLine.Split(Common.ElementSeparator);
      if (medalPools.Length != (int)MedalShop.COUNT)
        throw new Exception(nameof(MedalShopsPools) + " is in an invalid format");

      ObservableCollection<MedalShopPool>[] medals = (ObservableCollection<MedalShopPool>[])Data;

      for (int i = 0; i < medalPools.Length; i++)
      {
        if (medalPools[i] == string.Empty)
          continue;

        string[] data = medalPools[i].Split(Common.FieldSeparator);
        for (int j = 0; j < data.Length; j++)
        {
          int intOut = 0;
          if (!int.TryParse(data[j], out intOut))
          {
            throw new Exception(nameof(MedalShopsPools) + "[" + Enum.GetNames(typeof(MedalShop))[i] +
                                "][" + j + "] failed to parse");
          }
          if (intOut < 0 || intOut >= (int)Medal.COUNT)
          {
            throw new Exception(nameof(MedalShopsPools) + "[" + Enum.GetNames(typeof(MedalShop))[i] +
                                "][" + j + "]: " + intOut + " is not a valid medal ID");
          }
          medals[i].Add(new MedalShopPool { Medal = (Medal)intOut });
        }
      }
    }

    public string EncodeToSaveLine()
    {
      ObservableCollection<MedalShopPool>[] medals = (ObservableCollection<MedalShopPool>[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < medals.Length; i++)
      {
        for (int j = 0; j < medals[i].Count; j++)
        {
          sb.Append((int)medals[i][j].Medal);

          if (j != medals[i].Count - 1)
            sb.Append(Common.FieldSeparator);
        }

        if (i != medals.Length - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }

    public void ResetToDefault()
    {
      ObservableCollection<MedalShopPool>[] medals = (ObservableCollection<MedalShopPool>[])Data;
      foreach (var collection in medals)
        collection.Clear();
    }
  }
}
