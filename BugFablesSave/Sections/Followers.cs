using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Followers : IBugFablesSaveSection
  {
    public class Follower : INotifyPropertyChanged
    {
      private AnimID _animId;
      public AnimID AnimID
      {
        get { return _animId; }
        set
        {
          if ((int)value == -1)
            return;

          _animId = value;
          NotifyPropertyChanged();
        }
      }


      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public object Data { get; set; } = new ObservableCollection<Follower>();

    public string EncodeToSaveLine()
    {
      ObservableCollection<Follower> followers = (ObservableCollection<Follower>)Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < followers.Count; i++)
      {
        sb.Append((int)followers[i].AnimID);

        if (i != followers.Count - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] followersData = saveLine.Split(Common.FieldSeparator);
      ObservableCollection<Follower> followers = (ObservableCollection<Follower>)Data;

      for (int i = 0; i < followersData.Length; i++)
      {
        if (followersData[i] == string.Empty)
          continue;

        int intOut = 0;
        if (!int.TryParse(followersData[i], out intOut))
          throw new Exception(nameof(Followers) + "[" + i + "] failed to parse");
        if (intOut < 0 || intOut >= (int)AnimID.COUNT)
          throw new Exception(nameof(Followers) + "[" + i + "]: " + intOut + " is not a valid anim ID");
        followers.Add(new Follower { AnimID = (AnimID)intOut });
      }
    }
  }
}
