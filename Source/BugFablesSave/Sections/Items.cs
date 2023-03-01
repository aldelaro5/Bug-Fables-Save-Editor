using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Items : IBugFablesSaveSection
  {
    public class ItemInfo : INotifyPropertyChanged
    {
      private Item _item;
      public Item Item
      {
        get { return _item; }
        set
        {
          if ((int)value == -1)
            return;

          _item = value;
          NotifyPropertyChanged();
        }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }
    public object Data { get; set; } = new ObservableCollection<ItemInfo>[(int)ItemPossessionType.COUNT];

    public Items()
    {
      ObservableCollection<ItemInfo>[] items = (ObservableCollection<ItemInfo>[])Data;

      for (int i = 0; i < items.Length; i++)
        items[i] = new ObservableCollection<ItemInfo>();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] itemsData = saveLine.Split(Common.ElementSeparator);
      if (itemsData.Length != (int)ItemPossessionType.COUNT)
        throw new Exception(nameof(Items) + " is in an invalid format");

      ObservableCollection<ItemInfo>[] items = (ObservableCollection<ItemInfo>[])Data;

      for (int i = 0; i < itemsData.Length; i++)
      {
        if (itemsData[i] == string.Empty)
          continue;

        string[] data = itemsData[i].Split(Common.FieldSeparator);
        for (int j = 0; j < data.Length; j++)
        {
          int intOut = 0;
          if (!int.TryParse(data[j], out intOut))
          {
            throw new Exception(nameof(Items) + "[" + Enum.GetNames(typeof(ItemPossessionType))[i] +
                                "][" + j + "] failed to parse");
          }
          if (intOut < 0 || intOut >= (int)Item.COUNT)
            throw new Exception(nameof(Items) + "[" + Enum.GetNames(typeof(ItemPossessionType))[i] +
                                "][" + j + "]: " + intOut + " is not a valid item ID");
          items[i].Add(new ItemInfo { Item = (Item)intOut });
        }
      }
    }

    public string EncodeToSaveLine()
    {
      ObservableCollection<ItemInfo>[] items = (ObservableCollection<ItemInfo>[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < items.Length; i++)
      {
        for (int j = 0; j < items[i].Count; j++)
        {
          sb.Append((int)items[i][j].Item);

          if (j != items[i].Count - 1)
            sb.Append(Common.FieldSeparator);
        }

        if (i != items.Length - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }

    public void ResetToDefault()
    {
      ObservableCollection<ItemInfo>[] items = (ObservableCollection<ItemInfo>[])Data;
      foreach (var collection in items)
        collection.Clear();
    }
  }
}
