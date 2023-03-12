using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Followers;
using static BugFablesSaveEditor.BugFablesSave.Sections.PartyMembers;

namespace BugFablesSaveEditor.ViewModels;

public partial class PartyViewModel : ObservableObject
{
  private readonly ObservableCollection<PartyMemberInfo> _partyMemberInfos;

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

    _partyMemberInfos = new ObservableCollection<PartyMemberInfo>(SaveData.PartyMembers.List);
    PartyMembersVm =
      new ReorderableCollectionViewModel<PartyMemberInfo>(SaveData.PartyMembers.List);
    _partyMemberInfos.CollectionChanged += CollectionOnCollectionChanged;

    FollowersVm = new ReorderableCollectionViewModel<FollowerInfo>(SaveData.Followers.List);
  }

  private void CollectionOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (e.Action == NotifyCollectionChangedAction.Add)
      _partyMemberInfos.Insert(e.NewStartingIndex,
        new PartyMemberInfo { Trueid = ((PartyMember)e.NewItems![0]!).AnimID });
    else if (e.Action == NotifyCollectionChangedAction.Remove)
      _partyMemberInfos.RemoveAt(e.OldStartingIndex);
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
