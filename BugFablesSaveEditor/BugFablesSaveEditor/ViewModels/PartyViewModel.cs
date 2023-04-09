using System;
using System.Collections.ObjectModel;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class PartyViewModel : ObservableObject, IDisposable
{
  [ObservableProperty]
  private ViewModelCollection<PartyMemberSaveData, PartyMemberSaveDataModel> _partyMembers;

  [ObservableProperty]
  private ViewModelCollection<BfAnimId, BfNamedIdModel> _followers;

  public PartyViewModel() : this(new(), new()) { }

  public PartyViewModel(Collection<PartyMemberSaveData> partyMembers,
                        Collection<BfAnimId> followers)
  {
    _partyMembers = new(partyMembers);
    _followers = new(followers);
  }

  public void Dispose()
  {
    PartyMembers.Dispose();
    Followers.Dispose();
  }
}
