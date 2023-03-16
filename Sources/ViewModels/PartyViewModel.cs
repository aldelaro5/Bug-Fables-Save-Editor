using System.Collections.Generic;
using BugFablesDataLib;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesDataLib.Sections.Followers;
using static BugFablesDataLib.Sections.PartyMembers;

namespace BugFablesSaveEditor.ViewModels;

public partial class PartyViewModel : ObservableObject
{
  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private string[] _animIDs = Utils.GetEnumDescriptions<AnimID>();

  [ObservableProperty]
  private ReorderableCollectionViewModel<FollowerInfo> _followersVm;

  [ObservableProperty]
  private ReorderableCollectionViewModel<PartyMemberInfo> _partyMembersVm;

  [ObservableProperty]
  private AnimID _selectedFollowerAnimIdForAdd = 0;

  [ObservableProperty]
  private AnimID _selectedPartyMemberAnimIdForAdd = 0;

  public PartyViewModel() : this(new SaveData())
  {
    PartyMembersVm.Collection.Add(new PartyMemberInfo { AnimId = (AnimID)198 });
    PartyMembersVm.Collection.Add(new PartyMemberInfo { AnimId = (AnimID)340 });
    PartyMembersVm.Collection.Add(new PartyMemberInfo { AnimId = (AnimID)297 });

    FollowersVm.Collection.Add(new FollowerInfo { AnimId = (AnimID)150 });
    FollowersVm.Collection.Add(new FollowerInfo { AnimId = (AnimID)268 });
    FollowersVm.Collection.Add(new FollowerInfo { AnimId = (AnimID)244 });
  }

  public PartyViewModel(SaveData saveData)
  {
    _saveData = saveData;

    List<string> exposedProperties = new() { nameof(PartyMemberInfo.AnimId) };
    _partyMembersVm = new(_saveData.PartyMembers.List, exposedProperties);
    _followersVm = new(_saveData.Followers.List);
  }

  [RelayCommand]
  private void AddPartyMember()
  {
    PartyMembersVm.Collection.Add(new PartyMemberInfo { AnimId = SelectedPartyMemberAnimIdForAdd });
    PartyMembersVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddFollower()
  {
    FollowersVm.Collection.Add(new FollowerInfo { AnimId = SelectedFollowerAnimIdForAdd });
    FollowersVm.CollectionView.Refresh();
  }
}
