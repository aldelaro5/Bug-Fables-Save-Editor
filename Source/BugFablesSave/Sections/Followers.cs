using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Followers : BugFablesDataList<Followers.FollowerInfo>
{
  public sealed class FollowerInfo : BugFablesData, INotifyPropertyChanged
  {
    private AnimID _animId;

    public AnimID AnimID
    {
      get => _animId;
      set
      {
        if ((int)value == -1)
        {
          return;
        }

        _animId = value;
        NotifyPropertyChanged();
      }
    }

    public override void ResetToDefault()
    {
      AnimID = 0;
    }

    public override void Parse(string str)
    {
      AnimID = (AnimID)ParseField<int>(str, nameof(AnimID));
    }

    public override string ToString()
    {
      return ((int)AnimID).ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
