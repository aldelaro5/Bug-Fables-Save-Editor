using System.Collections.Generic;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Followers;
using static BugFablesSaveEditor.BugFablesSave.Sections.PartyMembers;

namespace BugFablesSaveEditor.ViewModels;

public partial class PartyViewModel : ObservableObject
{
  [ObservableProperty]
  private string[] _animIDs = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<FollowerInfo> _followersVm = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<PartyMemberInfo> _partyMembersVm = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  private AnimID _selectedFollowerAnimIdForAdd = 0;

  [ObservableProperty]
  private AnimID _selectedPartyMemberAnimIdForAdd = 0;

  public PartyViewModel() : this(new SaveData())
  {
    PartyMembersVm.Collection.Add(new PartyMemberInfo { Trueid = (AnimID)198 });
    PartyMembersVm.Collection.Add(new PartyMemberInfo { Trueid = (AnimID)340 });
    PartyMembersVm.Collection.Add(new PartyMemberInfo { Trueid = (AnimID)297 });

    FollowersVm.Collection.Add(new FollowerInfo { AnimID = (AnimID)150 });
    FollowersVm.Collection.Add(new FollowerInfo { AnimID = (AnimID)268 });
    FollowersVm.Collection.Add(new FollowerInfo { AnimID = (AnimID)244 });
  }

  public PartyViewModel(SaveData saveData)
  {
    SaveData = saveData;
    AnimIDs = Utils.GetEnumDescriptions<AnimID>();

    List<string>? exposedProperties = new() { nameof(PartyMemberInfo.Trueid) };
    PartyMembersVm =
      new ReorderableCollectionViewModel<PartyMemberInfo>(SaveData.PartyMembers.List,
        exposedProperties);
    FollowersVm = new ReorderableCollectionViewModel<FollowerInfo>(SaveData.Followers.List);
  }

  [RelayCommand]
  private void AddPartyMember()
  {
    PartyMembersVm.Collection.Add(new PartyMemberInfo { Trueid = SelectedPartyMemberAnimIdForAdd });
    PartyMembersVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddFollower()
  {
    FollowersVm.Collection.Add(new FollowerInfo { AnimID = SelectedFollowerAnimIdForAdd });
    FollowersVm.CollectionView.Refresh();
  }
}
