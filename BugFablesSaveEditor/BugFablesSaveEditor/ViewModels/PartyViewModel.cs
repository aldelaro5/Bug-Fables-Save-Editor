using System.Collections.Generic;
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
  private ObservablePartyMemberSaveData _newPartyMemberAnimId = new(new());
  [ObservableProperty]
  private ObservableBfNamedId _newFollowerAnimId = new(new BfAnimId());

  [RelayCommand]
  private void AddPartyMember(ObservablePartyMemberSaveData partyMemberSaveData)
  {
    PartyMembers.Add(new(partyMemberSaveData.UnderlyingData));
  }

  [RelayCommand]
  private void AddFollower(ObservableBfNamedId animId)
  {
    Followers.Add(new(animId.UnderlyingData));
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
