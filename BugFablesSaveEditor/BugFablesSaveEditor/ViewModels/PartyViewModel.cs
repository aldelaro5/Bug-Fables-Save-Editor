using System.Collections.ObjectModel;
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

  public PartyViewModel() : this(new(), new()) { }

  public PartyViewModel(Collection<PartyMemberSaveData> partyMembers,
                        Collection<BfAnimId> followers)
  {
    _partyMembers = new(partyMembers);
    _followers = new(followers);
  }
}
