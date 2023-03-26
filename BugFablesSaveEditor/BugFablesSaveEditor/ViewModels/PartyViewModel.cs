using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class PartyViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableBfCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> _partyMembers;
  [ObservableProperty]
  private ObservableBfCollection<BfAnimId, ObservableBfNamedId> _followers;
  [ObservableProperty]
  private ObservableBfNamedId _newPartyMemberAnimId = new(new BfAnimId {Id = 0});
  [ObservableProperty]
  private ObservableBfNamedId _newFollowerAnimId = new(new BfAnimId {Id = 0});

  [RelayCommand]
  private void AddPartyMember(int animId)
  {
    PartyMemberSaveData newMember = new() { AnimId = { Id = animId } };
    PartyMembers.Add(new(newMember));
  }

  [RelayCommand]
  private void AddFollower(int animId)
  {
    BfAnimId newFollower = new() { Id = animId };
    Followers.Add(new(newFollower));
  }

  public PartyViewModel(ObservableBfCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> partyMembers, ObservableBfCollection<BfAnimId, ObservableBfNamedId> followers)
  {
    _partyMembers = partyMembers;
    _followers = followers;
  }

  public PartyViewModel()
  {
    _partyMembers = new(new(), _ => new List<ObservablePartyMemberSaveData>());
    _followers = new(new(), _ => new List<ObservableBfNamedId>());
  }
}
