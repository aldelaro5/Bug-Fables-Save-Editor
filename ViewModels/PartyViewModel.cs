using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System.Collections.ObjectModel;
using static BugFablesSaveEditor.BugFablesSave.Sections.Followers;
using static BugFablesSaveEditor.BugFablesSave.Sections.PartyMembers;

namespace BugFablesSaveEditor.ViewModels
{
  public class PartyViewModel : ViewModelBase
  {
    public enum PartyType
    {
      PartyMember,
      Follower
    }

    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private string[] _animIds;
    public string[] AnimIDs
    {
      get { return _animIds; }
      set { _animIds = value; this.RaisePropertyChanged(); }
    }

    private AnimID _selectedPartyMemberAnimIDForAdd = 0;
    public AnimID SelectedPartyMemberAnimIDForAdd
    {
      get { return _selectedPartyMemberAnimIDForAdd; }
      set { _selectedPartyMemberAnimIDForAdd = value; this.RaisePropertyChanged(); }
    }

    private AnimID _selectedFollowerAnimIDForAdd = 0;
    public AnimID SelectedFollowerAnimIDForAdd
    {
      get { return _selectedFollowerAnimIDForAdd; }
      set { _selectedFollowerAnimIDForAdd = value; this.RaisePropertyChanged(); }
    }

    private ObservableCollection<PartyMemberInfo> _partyMembers = new ObservableCollection<PartyMemberInfo>();
    public ObservableCollection<PartyMemberInfo> PartyMembers
    {
      get { return _partyMembers; }
      set { _partyMembers = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<Follower> _followers = new ObservableCollection<Follower>();
    public ObservableCollection<Follower> Followers
    {
      get { return _followers; }
      set { _followers = value; this.RaisePropertyChanged(); }
    }

    public PartyViewModel()
    {
      SaveData = new SaveData();
      AnimIDs = Common.GetEnumDescriptions<AnimID>();
      PartyMembers = (ObservableCollection<PartyMemberInfo>)SaveData.Sections[SaveFileSection.PartyMembers].Data;
      Followers = (ObservableCollection<Follower>)SaveData.Sections[SaveFileSection.Followers].Data;

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

    public void AddPartyMember()
    {
      PartyMembers.Add(new PartyMemberInfo { Trueid = SelectedPartyMemberAnimIDForAdd });
    }

    public void RemovePartyMember(PartyMemberInfo partyMemberInfo)
    {
      PartyMembers.Remove(partyMemberInfo);
    }

    public void AddFollower()
    {
      Followers.Add(new Follower { AnimID = SelectedFollowerAnimIDForAdd });
    }

    public void RemoveFollower(Follower follower)
    {
      Followers.Remove(follower);
    }
  }
}
