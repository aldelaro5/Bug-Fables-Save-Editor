using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservablePartyMemberSaveData : ObservableModel
{
  public sealed override PartyMemberSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfNamedId _animId;

  public ReactiveProperty<int> Attack { get; }
  public ReactiveProperty<int> BaseAttack { get; }
  public ReactiveProperty<int> BaseDefense { get; }
  public ReactiveProperty<int> BaseHp { get; }
  public ReactiveProperty<int> Defense { get; }
  public ReactiveProperty<int> Hp { get; }
  public ReactiveProperty<int> MaxHp { get; }

  public ObservablePartyMemberSaveData(PartyMemberSaveData partyMemberSaveData) :
    base(partyMemberSaveData)
  {
    UnderlyingData = partyMemberSaveData;
    _animId = new(UnderlyingData.AnimId);
    Hp = ReactiveProperty.FromObject(UnderlyingData, data => data.Hp);
    MaxHp = ReactiveProperty.FromObject(UnderlyingData, data => data.MaxHp);
    BaseHp = ReactiveProperty.FromObject(UnderlyingData, data => data.BaseHp);
    Attack = ReactiveProperty.FromObject(UnderlyingData, data => data.Attack);
    BaseAttack = ReactiveProperty.FromObject(UnderlyingData, data => data.BaseAttack);
    Defense = ReactiveProperty.FromObject(UnderlyingData, data => data.Defense);
    BaseDefense = ReactiveProperty.FromObject(UnderlyingData, data => data.BaseDefense);
  }
}
