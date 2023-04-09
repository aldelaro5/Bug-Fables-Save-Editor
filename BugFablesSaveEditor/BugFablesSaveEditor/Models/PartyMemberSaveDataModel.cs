using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public partial class PartyMemberSaveDataModel : ObservableObject, IModelWrapper<PartyMemberSaveData>
{
  public PartyMemberSaveData Model { get; }

  [ObservableProperty]
  private BfNamedIdModel _animId;

  public int Attack
  {
    get => Model.Attack;
    set => SetProperty(Model.Attack, value, Model, (data, i) => data.Attack = i);
  }

  public int BaseAttack
  {
    get => Model.BaseAttack;
    set => SetProperty(Model.BaseAttack, value, Model, (data, i) => data.BaseAttack = i);
  }

  public int BaseDefense
  {
    get => Model.BaseDefense;
    set => SetProperty(Model.BaseDefense, value, Model, (data, i) => data.BaseDefense = i);
  }

  public int BaseHp
  {
    get => Model.BaseHp;
    set => SetProperty(Model.BaseHp, value, Model, (data, i) => data.BaseHp = i);
  }

  public int Defense
  {
    get => Model.Defense;
    set => SetProperty(Model.Defense, value, Model, (data, i) => data.Defense = i);
  }

  public int Hp
  {
    get => Model.Hp;
    set => SetProperty(Model.Hp, value, Model, (data, i) => data.Hp = i);
  }

  public int MaxHp
  {
    get => Model.MaxHp;
    set => SetProperty(Model.MaxHp, value, Model, (data, i) => data.MaxHp = i);
  }

  public static IModelWrapper<PartyMemberSaveData> WrapModel(PartyMemberSaveData model) =>
    new PartyMemberSaveDataModel(model);

  private PartyMemberSaveDataModel(PartyMemberSaveData partyMemberSaveData)
  {
    Model = partyMemberSaveData;
    _animId = new(Model.AnimId);
  }
}
