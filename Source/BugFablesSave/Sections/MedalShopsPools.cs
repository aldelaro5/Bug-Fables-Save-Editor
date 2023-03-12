using System.ComponentModel;
using System.Runtime.CompilerServices;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class
  MedalShopsPools : BugFablesDataList<MedalShopsPools.MedalsShopPoolInfo>
{
  public MedalShopsPools()
  {
    ElementSeparator = Utils.SecondarySeparator;
    NbrExpectedElements = (int)MedalShop.COUNT;
    while (List.Count < (int)MedalShop.COUNT)
      List.Add(new MedalsShopPoolInfo());
  }

  public sealed class MedalsShopPoolInfo : BugFablesDataList<MedalInShopPool>
  {
  }

  public sealed class MedalInShopPool : BugFablesData, INotifyPropertyChanged
  {
    private Medal _medal;

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

    public override void ResetToDefault()
    {
      Medal = 0;
    }

    public override void Parse(string str)
    {
      Medal = (Medal)ParseField<int>(str, nameof(Medal));
    }

    public override string ToString()
    {
      return ((int)Medal).ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
