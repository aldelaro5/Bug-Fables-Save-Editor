using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class RegionalFlags : IBugFablesSaveSection
  {
    public class RegionalInfo : INotifyPropertyChanged
    {
      private int _index;
      public int Index
      {
        get { return _index; }
        set { _index = value; NotifyPropertyChanged(); }
      }

      private bool _enabled;
      public bool Enabled
      {
        get { return _enabled; }
        set { _enabled = value; NotifyPropertyChanged(); }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    private const int NbrSlots = 100;

    public object Data { get; set; } = new RegionalInfo[NbrSlots];

    public RegionalFlags()
    {
      var array = (RegionalInfo[])Data;
      for (int i = 0; i < array.Length; i++)
        array[i] = new RegionalInfo { Index = i };
    }

    public string EncodeToSaveLine()
    {
      RegionalInfo[] regionals = (RegionalInfo[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < regionals.Length; i++)
      {
        sb.Append(regionals[i].Enabled);

        if (i != regionals.Length - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] regionalFlagsData = saveLine.Split(Common.FieldSeparator);
      if (regionalFlagsData.Length != NbrSlots)
        throw new Exception(nameof(RegionalFlags) + " is in an invalid format");

      RegionalInfo[] regionalFlags = (RegionalInfo[])Data;

      for (int i = 0; i < regionalFlagsData.Length; i++)
      {
        bool boolOut = false;
        if (!bool.TryParse(regionalFlagsData[i], out boolOut))
          throw new Exception(nameof(RegionalFlags) + "[" + i + "] failed to parse");
        regionalFlags[i].Enabled = boolOut;
      }
    }
  }
}
