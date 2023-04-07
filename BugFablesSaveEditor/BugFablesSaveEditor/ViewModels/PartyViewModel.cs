using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class PartyViewModel : ObservableObject
{
  [ObservableProperty]
  private ViewModelCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> _partyMembers;

  [ObservableProperty]
  private ViewModelCollection<BfAnimId, ObservableBfNamedId> _followers;

  public PartyViewModel(
    ViewModelCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> partyMembers,
    ViewModelCollection<BfAnimId, ObservableBfNamedId> followers)
  {
    _partyMembers = partyMembers;
    _followers = followers;
  }

  public PartyViewModel()
  {
    _partyMembers = new(new(), x => new ObservablePartyMemberSaveData(x));
    _followers = new(new(), x => new ObservableBfNamedId(x));
  }
}
