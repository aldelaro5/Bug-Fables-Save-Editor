using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class PartyViewModel : ObservableObject
{
  [ObservableProperty]
  private ViewModelCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> _partyMembers;

  [ObservableProperty]
  private ViewModelCollection<BfAnimId, ObservableBfNamedId> _followers;

  public PartyViewModel() : this(new(new()), new(new())) { }
  public PartyViewModel(ViewModelCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> partyMembers,
                        ViewModelCollection<BfAnimId, ObservableBfNamedId> followers)
  {
    _partyMembers = partyMembers;
    _followers = followers;
  }
}
