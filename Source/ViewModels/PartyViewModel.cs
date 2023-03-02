using System.Collections;
using System.Collections.ObjectModel;
using System.Reactive;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using static BugFablesSaveEditor.BugFablesSave.Sections.Followers;
using static BugFablesSaveEditor.BugFablesSave.Sections.PartyMembers;

namespace BugFablesSaveEditor.ViewModels;

public class PartyViewModel : ViewModelBase
{
  public enum PartyType
  {
    PartyMember,
    Follower
  }

  private string[] _animIds;
  private ObservableCollection<Follower> _followers;

  private ObservableCollection<PartyMemberInfo> _partyMembers;

  private SaveData _saveData;
  private Follower _selectedFollower;

  private AnimID _selectedFollowerAnimIDForAdd = 0;

  private PartyMemberInfo _selectedPartyMember;

  private AnimID _selectedPartyMemberAnimIDForAdd = 0;

  public PartyViewModel()
  {
    SaveData = new SaveData();
    Initialise();

    PartyMembers.Add(new PartyMemberInfo { Trueid = (AnimID)198 });
    PartyMembers.Add(new PartyMemberInfo { Trueid = (AnimID)340 });
    PartyMembers.Add(new PartyMemberInfo { Trueid = (AnimID)297 });

    Followers.Add(new Follower { AnimID = (AnimID)150 });
    Followers.Add(new Follower { AnimID = (AnimID)268 });
    Followers.Add(new Follower { AnimID = (AnimID)244 });
  }

  public PartyViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Initialise();
  }

  public SaveData SaveData
  {
    get => _saveData;
    set
    {
      _saveData = value;
      this.RaisePropertyChanged();
    }
  }

  public string[] AnimIDs
  {
    get => _animIds;
    set
    {
      _animIds = value;
      this.RaisePropertyChanged();
    }
  }

  public AnimID SelectedPartyMemberAnimIDForAdd
  {
    get => _selectedPartyMemberAnimIDForAdd;
    set
    {
      _selectedPartyMemberAnimIDForAdd = value;
      this.RaisePropertyChanged();
    }
  }

  public AnimID SelectedFollowerAnimIDForAdd
  {
    get => _selectedFollowerAnimIDForAdd;
    set
    {
      _selectedFollowerAnimIDForAdd = value;
      this.RaisePropertyChanged();
    }
  }

  public PartyMemberInfo SelectedPartyMember
  {
    get => _selectedPartyMember;
    set
    {
      _selectedPartyMember = value;
      this.RaisePropertyChanged();
    }
  }

  public Follower SelectedFollower
  {
    get => _selectedFollower;
    set
    {
      _selectedFollower = value;
      this.RaisePropertyChanged();
    }
  }

  public ObservableCollection<PartyMemberInfo> PartyMembers
  {
    get => _partyMembers;
    set
    {
      _partyMembers = value;
      this.RaisePropertyChanged();
    }
  }

  public ObservableCollection<Follower> Followers
  {
    get => _followers;
    set
    {
      _followers = value;
      this.RaisePropertyChanged();
    }
  }

  public ReactiveCommand<Unit, Unit> CmdReorderPartyMembersUp { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderPartyMembersDown { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderFollowersUp { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderFollowersDown { get; set; }

  private void Initialise()
  {
    AnimIDs = Common.GetEnumDescriptions<AnimID>();
    PartyMembers = (ObservableCollection<PartyMemberInfo>)SaveData.Sections[SaveFileSection.PartyMembers].Data;
    Followers = (ObservableCollection<Follower>)SaveData.Sections[SaveFileSection.Followers].Data;

    CmdReorderPartyMembersUp = ReactiveCommand.Create(() =>
    {
      ReorderAnimID(PartyType.PartyMember, ReorderDirection.Up);
    }, this.WhenAnyValue(x => x.SelectedPartyMember, x => x != null && PartyMembers[0] != x));
    CmdReorderPartyMembersDown = ReactiveCommand.Create(() =>
    {
      ReorderAnimID(PartyType.PartyMember, ReorderDirection.Down);
    }, this.WhenAnyValue(x => x.SelectedPartyMember, x => x != null && PartyMembers[PartyMembers.Count - 1] != x));

    CmdReorderFollowersUp = ReactiveCommand.Create(() =>
    {
      ReorderAnimID(PartyType.Follower, ReorderDirection.Up);
    }, this.WhenAnyValue(x => x.SelectedFollower, x => x != null && Followers[0] != x));
    CmdReorderFollowersDown = ReactiveCommand.Create(() =>
    {
      ReorderAnimID(PartyType.Follower, ReorderDirection.Down);
    }, this.WhenAnyValue(x => x.SelectedFollower, x => x != null && Followers[Followers.Count - 1] != x));
  }

  private void ReorderAnimID(PartyType type, ReorderDirection direction)
  {
    object selectedItem;
    AnimID animId;
    IList itemsCollection;
    switch (type)
    {
      case PartyType.PartyMember:
        selectedItem = SelectedPartyMember;
        animId = ((PartyMemberInfo)selectedItem).Trueid;
        itemsCollection = PartyMembers;
        break;
      case PartyType.Follower:
        selectedItem = SelectedFollower;
        animId = ((Follower)selectedItem).AnimID;
        itemsCollection = Followers;
        break;
      default:
        return;
    }

    int index = itemsCollection.IndexOf(selectedItem);
    int newIndex = index;
    if (direction == ReorderDirection.Up)
    {
      newIndex--;
    }
    else if (direction == ReorderDirection.Down)
    {
      newIndex++;
    }

    itemsCollection.Remove(selectedItem);

    switch (type)
    {
      case PartyType.PartyMember:
        itemsCollection.Insert(newIndex, new PartyMemberInfo { Trueid = animId });
        SelectedPartyMember = PartyMembers[newIndex];
        break;
      case PartyType.Follower:
        itemsCollection.Insert(newIndex, new Follower { AnimID = animId });
        SelectedFollower = Followers[newIndex];
        break;
    }
  }

  public void AddPartyMember()
  {
    PartyMembers.Add(new PartyMemberInfo { Trueid = SelectedPartyMemberAnimIDForAdd });
  }

  public void RemovePartyMember(PartyMemberInfo partyMemberInfo)
  {
    PartyMembers.Remove(partyMemberInfo);
  }

  public void AddFollower()
  {
    Followers.Add(new Follower { AnimID = SelectedFollowerAnimIDForAdd });
  }

  public void RemoveFollower(Follower follower)
  {
    Followers.Remove(follower);
  }
}
