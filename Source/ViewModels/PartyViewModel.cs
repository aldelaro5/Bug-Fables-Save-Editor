using System.Collections;
using System.Collections.ObjectModel;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Followers;
using static BugFablesSaveEditor.BugFablesSave.Sections.PartyMembers;

namespace BugFablesSaveEditor.ViewModels;

public partial class PartyViewModel : ObservableObject
{
  public enum PartyType
  {
    PartyMember,
    Follower
  }

  [ObservableProperty]
  private string[] _animIDs = null!;
  [ObservableProperty]
  private ObservableCollection<Follower> _followers = null!;

  [ObservableProperty]
  private ObservableCollection<PartyMemberInfo> _partyMembers = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;
  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderFollowersUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderFollowersDownCommand))]
  private Follower? _selectedFollower;

  [ObservableProperty]
  private AnimID _selectedFollowerAnimIDForAdd = 0;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderPartyMembersUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderPartyMembersDownCommand))]
  private PartyMemberInfo? _selectedPartyMember;

  [ObservableProperty]
  private AnimID _selectedPartyMemberAnimIDForAdd = 0;

  public PartyViewModel() : this(new SaveData())
  {
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
    AnimIDs = Common.GetEnumDescriptions<AnimID>();
    PartyMembers = (ObservableCollection<PartyMemberInfo>)SaveData.Sections[SaveFileSection.PartyMembers].Data;
    Followers = (ObservableCollection<Follower>)SaveData.Sections[SaveFileSection.Followers].Data;
  }

  [RelayCommand(CanExecute = nameof(CanReorderPartyMemberUp))]
  private void CmdReorderPartyMembersUp()
  {
    ReorderAnimID(PartyType.PartyMember, ReorderDirection.Up);
  }
  private bool CanReorderPartyMemberUp()
  {
    return PartyMembers.Count > 0 && SelectedPartyMember is not null && PartyMembers.Count > 0 && PartyMembers[0] != SelectedPartyMember;
  }

  [RelayCommand(CanExecute = nameof(CanReorderPartyMemberDown))]
  private void CmdReorderPartyMembersDown()
  {
    ReorderAnimID(PartyType.PartyMember, ReorderDirection.Down);
  }
  private bool CanReorderPartyMemberDown()
  {
    return PartyMembers.Count > 0 && SelectedPartyMember is not null && PartyMembers.Count > 0 && PartyMembers[^1] != SelectedPartyMember;
  }

  [RelayCommand(CanExecute = nameof(CanReorderFollowerUp))]
  private void CmdReorderFollowersUp()
  {
    ReorderAnimID(PartyType.Follower, ReorderDirection.Up);
  }
  private bool CanReorderFollowerUp()
  {
    return Followers.Count > 0 && SelectedFollower is not null && Followers.Count > 0 && Followers[0] != SelectedFollower;
  }

  [RelayCommand(CanExecute = nameof(CanReorderFollowerDown))]
  private void CmdReorderFollowersDown()
  {
    ReorderAnimID(PartyType.Follower, ReorderDirection.Down);
  }
  private bool CanReorderFollowerDown()
  {
    return Followers.Count > 0 && SelectedFollower is not null && Followers.Count > 0 && Followers[^1] != SelectedFollower;
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
      newIndex--;
    else if (direction == ReorderDirection.Down)
      newIndex++;

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

  [RelayCommand]
  private void AddPartyMember()
  {
    PartyMembers.Add(new PartyMemberInfo { Trueid = SelectedPartyMemberAnimIDForAdd });
  }

  [RelayCommand]
  private void RemovePartyMember(PartyMemberInfo partyMemberInfo)
  {
    PartyMembers.Remove(partyMemberInfo);
  }

  [RelayCommand]
  private void AddFollower()
  {
    Followers.Add(new Follower { AnimID = SelectedFollowerAnimIDForAdd });
  }

  [RelayCommand]
  private void RemoveFollower(Follower follower)
  {
    Followers.Remove(follower);
  }
}
