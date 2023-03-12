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

    public AnimID AnimId
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
      AnimId = 0;
    }

    public override void Parse(string str)
    {
      AnimId = (AnimID)ParseField<int>(str, nameof(AnimId));
    }

    public override string ToString()
    {
      return ((int)AnimId).ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
