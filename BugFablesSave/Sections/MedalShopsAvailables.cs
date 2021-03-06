using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class MedalShopsAvailables : IBugFablesSaveSection
  {
    public class MedalShopAvailable : INotifyPropertyChanged
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

    public object Data { get; set; } = new ObservableCollection<MedalShopAvailable>[(int)MedalShop.COUNT];

    public MedalShopsAvailables()
    {
      ObservableCollection<MedalShopAvailable>[] medals = (ObservableCollection<MedalShopAvailable>[])Data;

      for (int i = 0; i < medals.Length; i++)
        medals[i] = new ObservableCollection<MedalShopAvailable>();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] medalsAvailable = saveLine.Split(CommonUtils.ElementSeparator);
      if (medalsAvailable.Length != (int)MedalShop.COUNT)
        throw new Exception(nameof(MedalShopsAvailables) + " is in an invalid format");

      ObservableCollection<MedalShopAvailable>[] medals = (ObservableCollection<MedalShopAvailable>[])Data;

      for (int i = 0; i < medalsAvailable.Length; i++)
      {
        if (medalsAvailable[i] == string.Empty)
          continue;

        string[] data = medalsAvailable[i].Split(CommonUtils.FieldSeparator);
        for (int j = 0; j < data.Length; j++)
        {
          int intOut = 0;
          if (!int.TryParse(data[j], out intOut))
          {
            throw new Exception(nameof(MedalShopsAvailables) + "[" + Enum.GetNames(typeof(MedalShop))[i] +
                                "][" + j + "] failed to parse");
          }
          if (intOut < 0 || intOut >= (int)Medal.COUNT)
          {
            throw new Exception(nameof(MedalShopsAvailables) + "[" + Enum.GetNames(typeof(MedalShop))[i] +
                                "][" + j + "]: " + intOut + " is not a valid medal ID");
          }
          medals[i].Add(new MedalShopAvailable { Medal = (Medal)intOut });
        }
      }
    }

    public string EncodeToSaveLine()
    {
      ObservableCollection<MedalShopAvailable>[] medals = (ObservableCollection<MedalShopAvailable>[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < medals.Length; i++)
      {
        for (int j = 0; j < medals[i].Count; j++)
        {
          sb.Append((int)medals[i][j].Medal);

          if (j != medals[i].Count - 1)
            sb.Append(CommonUtils.FieldSeparator);
        }

        if (i != medals.Length - 1)
          sb.Append(CommonUtils.ElementSeparator);
      }

      return sb.ToString();
    }

    public void ResetToDefault()
    {
      ObservableCollection<MedalShopAvailable>[] medals = (ObservableCollection<MedalShopAvailable>[])Data;
      foreach (var collection in medals)
        collection.Clear();
    }
  }
}
