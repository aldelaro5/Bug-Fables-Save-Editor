using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Items : BugFablesDataList<Items.ItemsPoessionTypeInfo>
{
  public IList<ItemInfo> Inventory { get => List[(int)ItemPossessionType.Inventory].List; }
  public IList<ItemInfo> Key { get => List[(int)ItemPossessionType.KeyItem].List; }
  public IList<ItemInfo> Stored { get => List[(int)ItemPossessionType.Stored].List; }

  public Items()
  {
    ElementSeparator = Utils.SecondarySeparator;
    NbrExpectedElements = (int)ItemPossessionType.COUNT;
    while (List.Count < (int)ItemPossessionType.COUNT)
      List.Add(new ItemsPoessionTypeInfo());
  }

  public sealed class ItemsPoessionTypeInfo : BugFablesDataList<ItemInfo>
  {
  }

  public sealed class ItemInfo : BugFablesData, INotifyPropertyChanged
  {
    private Item _item;

    public Item Item
    {
      get => _item;
      set
      {
        if ((int)value == -1)
        {
          return;
        }

        _item = value;
        NotifyPropertyChanged();
      }
    }

    public override void ResetToDefault()
    {
      Item = 0;
    }

    public override void Parse(string str)
    {
      Item = (Item)ParseField<int>(str, nameof(Item));
    }

    public override string ToString()
    {
      return ((int)Item).ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
