using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class SamiraSongs : BugFablesDataList<SamiraSongs.SongInfo>
{
  private const int songNotBought = -1;
  private const int songBought = 1;

  public SamiraSongs()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  public sealed class SongInfo : BugFablesData, INotifyPropertyChanged
  {
    private bool _isBought;
    private Song _song;

    public Song Song
    {
      get => _song;
      set
      {
        if ((int)value == -1)
        {
          return;
        }

        _song = value;
        NotifyPropertyChanged();
      }
    }

    public bool IsBought
    {
      get => _isBought;
      set
      {
        _isBought = value;
        NotifyPropertyChanged();
      }
    }

    public override void ResetToDefault()
    {
      Song = 0;
      IsBought = false;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(Utils.PrimarySeparator);

      Song = (Song)ParseField<int>(data[0], nameof(Song));
      IsBought = ParseField<int>(data[1], nameof(IsBought)) == songBought;
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append((int)Song);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(IsBought ? songBought : songNotBought);

      return sb.ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
