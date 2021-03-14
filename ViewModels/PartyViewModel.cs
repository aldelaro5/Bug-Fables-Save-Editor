using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

    public class OrderedAnimID : INotifyPropertyChanged
    {
      private int _index;
      public int Index
      {
        get { return _index; }
        set { _index = value; NotifyPropertyChanged(); }
      }

      public PartyType PartyType { get; set; }

      private AnimID _animID;
      public AnimID AnimID
      {
        get { return _animID; }
        set
        {
          // This workaround an issue where changing tabs sets this to -1???
          if ((int)value == -1)
            return;
          _animID = value;
          NotifyPropertyChanged();
        }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
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

    private ObservableCollection<PartyMemberInfo> savePartyMemberInfos;
    private ObservableCollection<AnimID> saveFollowers;

    private ObservableCollection<OrderedAnimID> _orderedPartyMembers = new ObservableCollection<OrderedAnimID>();
    public ObservableCollection<OrderedAnimID> OrderedPartyMembers
    {
      get { return _orderedPartyMembers; }
      set { _orderedPartyMembers = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<OrderedAnimID> _orderedFollowers = new ObservableCollection<OrderedAnimID>();
    public ObservableCollection<OrderedAnimID> OrderedFollowers
    {
      get { return _orderedFollowers; }
      set { _orderedFollowers = value; this.RaisePropertyChanged(); }
    }

    public PartyViewModel()
    {
      SaveData = new SaveData();
      AnimIDs = Common.GetEnumDescriptions<AnimID>();
      savePartyMemberInfos = (ObservableCollection<PartyMemberInfo>)SaveData.Sections[SaveFileSection.PartyMembers].Data;
      savePartyMemberInfos.CollectionChanged += OnSaveDataPartyChanged;
      saveFollowers = (ObservableCollection<AnimID>)SaveData.Sections[SaveFileSection.Followers].Data;
      saveFollowers.CollectionChanged += OnSaveFollowersChanged;

      savePartyMemberInfos.Add(new PartyMemberInfo { Trueid = (AnimID)198 });
      savePartyMemberInfos.Add(new PartyMemberInfo { Trueid = (AnimID)340 });
      savePartyMemberInfos.Add(new PartyMemberInfo { Trueid = (AnimID)297 });

      saveFollowers.Add((AnimID)150);
      saveFollowers.Add((AnimID)268);
      saveFollowers.Add((AnimID)244);
    }

    public PartyViewModel(SaveData saveData)
    {
      SaveData = saveData;
      AnimIDs = Common.GetEnumDescriptions<AnimID>();
      savePartyMemberInfos = (ObservableCollection<PartyMemberInfo>)SaveData.Sections[SaveFileSection.PartyMembers].Data;
      savePartyMemberInfos.CollectionChanged += OnSaveDataPartyChanged;
      saveFollowers = (ObservableCollection<AnimID>)SaveData.Sections[SaveFileSection.Followers].Data;
      saveFollowers.CollectionChanged += OnSaveFollowersChanged;
    }

    private void OnSaveDataPartyChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
      {
        foreach (PartyMemberInfo item in e.NewItems)
          AddOrderedAnimIDFromSaveDataItem(item, PartyType.PartyMember);
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
      {
        foreach (PartyMemberInfo item in e.OldItems)
          RemovedOrderedAnimIDWithIndex(e.OldStartingIndex, PartyType.PartyMember);
      }
      else if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        ClearOrderedAnimIDCollection(PartyType.PartyMember);

        if (e.NewItems != null)
        {
          foreach (PartyMemberInfo item in e.NewItems)
            AddOrderedAnimIDFromSaveDataItem(item, PartyType.PartyMember);
        }
      }
    }

    private void OnSaveFollowersChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
      {
        foreach (AnimID item in e.NewItems)
          AddOrderedAnimIDFromSaveDataItem(item, PartyType.Follower);
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
      {
        foreach (AnimID item in e.OldItems)
          RemovedOrderedAnimIDWithIndex(e.OldStartingIndex, PartyType.Follower);
      }
      else if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        ClearOrderedAnimIDCollection(PartyType.Follower);

        if (e.NewItems != null)
        {
          foreach (AnimID item in e.NewItems)
            AddOrderedAnimIDFromSaveDataItem(item, PartyType.Follower);
        }
      }
    }

    private void ClearOrderedAnimIDCollection(PartyType partyType)
    {
      if (partyType == PartyType.PartyMember)
      {
        foreach (var orderedAnimID in OrderedPartyMembers)
          orderedAnimID.PropertyChanged -= OnOrderedAnimIDChanged;

        OrderedPartyMembers.Clear();
      }
      else if (partyType == PartyType.Follower)
      {
        foreach (var orderedAnimID in OrderedFollowers)
          orderedAnimID.PropertyChanged -= OnOrderedAnimIDChanged;

        OrderedFollowers.Clear();
      }
    }

    private void RemovedOrderedAnimIDWithIndex(int index, PartyType partyType)
    {
      if (partyType == PartyType.PartyMember)
      {
        OrderedAnimID orderedAnimID = OrderedPartyMembers[index];
        orderedAnimID.PropertyChanged -= OnOrderedAnimIDChanged;
        OrderedPartyMembers.Remove(orderedAnimID);
        if (OrderedPartyMembers.Count == 0)
          return;

        for (int i = index; i < OrderedPartyMembers.Max(x => x.Index); i++)
          OrderedPartyMembers[i].Index--;
      }
      else if (partyType == PartyType.Follower)
      {
        OrderedAnimID orderedAnimID = OrderedFollowers[index];
        orderedAnimID.PropertyChanged -= OnOrderedAnimIDChanged;
        OrderedFollowers.Remove(orderedAnimID);
        if (OrderedFollowers.Count == 0)
          return;

        for (int i = index; i < OrderedFollowers.Max(x => x.Index); i++)
          OrderedFollowers[i].Index--;
      }
    }

    private void AddOrderedAnimIDFromSaveDataItem(object item, PartyType partyType)
    {
      if (partyType == PartyType.PartyMember)
      {
        OrderedAnimID orderedAnimID = new OrderedAnimID();
        orderedAnimID.Index = OrderedPartyMembers.Count;
        orderedAnimID.AnimID = ((PartyMemberInfo)item).Trueid;
        orderedAnimID.PartyType = partyType;
        orderedAnimID.PropertyChanged += OnOrderedAnimIDChanged;
        OrderedPartyMembers.Add(orderedAnimID);
      }
      else if (partyType == PartyType.Follower)
      {
        OrderedAnimID orderedAnimID = new OrderedAnimID();
        orderedAnimID.Index = OrderedFollowers.Count;
        orderedAnimID.AnimID = (AnimID)item;
        orderedAnimID.PartyType = partyType;
        orderedAnimID.PropertyChanged += OnOrderedAnimIDChanged;
        OrderedFollowers.Add(orderedAnimID);
      }
    }

    public void AddPartyMember()
    {
      savePartyMemberInfos.Add(new PartyMemberInfo { Trueid = SelectedPartyMemberAnimIDForAdd });
    }

    public void RemovePartyMember(int index)
    {
      savePartyMemberInfos.RemoveAt(index);
    }

    public void AddFollower()
    {
      saveFollowers.Add(SelectedFollowerAnimIDForAdd);
    }

    public void RemoveFollower(int index)
    {
      saveFollowers.RemoveAt(index);
    }

    private void OnOrderedAnimIDChanged(object? sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(OrderedAnimID.AnimID) && sender != null)
      {
        OrderedAnimID orderedAnimID = (OrderedAnimID)sender;
        if (orderedAnimID.PartyType == PartyType.PartyMember)
          savePartyMemberInfos[orderedAnimID.Index].Trueid = orderedAnimID.AnimID;
        else if (orderedAnimID.PartyType == PartyType.Follower)
          saveFollowers[orderedAnimID.Index] = orderedAnimID.AnimID;
      }
    }
  }
}
