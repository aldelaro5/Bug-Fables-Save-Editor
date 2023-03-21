using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservablePartyMemberSaveData : BfObservable
{
  private readonly PartyMemberSaveData _partyMemberSaveData;

  [ObservableProperty]
  private ObservableBfResource _animId;

  public int Attack
  {
    get => _partyMemberSaveData.Attack;
    set => SetProperty(_partyMemberSaveData.Attack, value, _partyMemberSaveData,
      (x, y) => x.Attack = y);
  }

  public int BaseAttack
  {
    get => _partyMemberSaveData.BaseAttack;
    set => SetProperty(_partyMemberSaveData.BaseAttack, value, _partyMemberSaveData,
      (x, y) => x.BaseAttack = y);
  }

  public int BaseDefense
  {
    get => _partyMemberSaveData.BaseDefense;
    set => SetProperty(_partyMemberSaveData.BaseDefense, value, _partyMemberSaveData,
      (x, y) => x.BaseDefense = y);
  }

  public int BaseHp
  {
    get => _partyMemberSaveData.BaseHp;
    set => SetProperty(_partyMemberSaveData.BaseHp, value, _partyMemberSaveData,
      (x, y) => x.BaseHp = y);
  }

  public int Defense
  {
    get => _partyMemberSaveData.Defense;
    set => SetProperty(_partyMemberSaveData.Defense, value, _partyMemberSaveData,
      (x, y) => x.Defense = y);
  }

  public int Hp
  {
    get => _partyMemberSaveData.Hp;
    set => SetProperty(_partyMemberSaveData.Hp, value, _partyMemberSaveData,
      (x, y) => x.Hp = y);
  }

  public int MaxHp
  {
    get => _partyMemberSaveData.MaxHp;
    set => SetProperty(_partyMemberSaveData.MaxHp, value, _partyMemberSaveData,
      (x, y) => x.MaxHp = y);
  }

  public ObservablePartyMemberSaveData(PartyMemberSaveData partyMemberSaveData) :
    base(partyMemberSaveData)
  {
    _partyMemberSaveData = partyMemberSaveData;
    _animId = new(_partyMemberSaveData.AnimId);
  }
}
