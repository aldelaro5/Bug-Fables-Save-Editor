using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservablePartyMemberSaveData : ObservableModel
{
  public sealed override PartyMemberSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfNamedId _animId;

  public int Attack
  {
    get => UnderlyingData.Attack;
    set => SetProperty(UnderlyingData.Attack, value, UnderlyingData, (data, i) => data.Attack = i);
  }
  public int BaseAttack
  {
    get => UnderlyingData.BaseAttack;
    set => SetProperty(UnderlyingData.BaseAttack, value, UnderlyingData, (data, i) => data.BaseAttack = i);
  }
  public int BaseDefense
  {
    get => UnderlyingData.BaseDefense;
    set => SetProperty(UnderlyingData.BaseDefense, value, UnderlyingData, (data, i) => data.BaseDefense = i);
  }
  public int BaseHp
  {
    get => UnderlyingData.BaseHp;
    set => SetProperty(UnderlyingData.BaseHp, value, UnderlyingData, (data, i) => data.BaseHp = i);
  }
  public int Defense
  {
    get => UnderlyingData.Defense;
    set => SetProperty(UnderlyingData.Defense, value, UnderlyingData, (data, i) => data.Defense = i);
  }
  public int Hp
  {
    get => UnderlyingData.Hp;
    set => SetProperty(UnderlyingData.Hp, value, UnderlyingData, (data, i) => data.Hp = i);
  }
  public int MaxHp
  {
    get => UnderlyingData.MaxHp;
    set => SetProperty(UnderlyingData.MaxHp, value, UnderlyingData, (data, i) => data.MaxHp = i);
  }

  public ObservablePartyMemberSaveData(PartyMemberSaveData partyMemberSaveData) :
    base(partyMemberSaveData)
  {
    UnderlyingData = partyMemberSaveData;
    _animId = new(UnderlyingData.AnimId);
  }
}
