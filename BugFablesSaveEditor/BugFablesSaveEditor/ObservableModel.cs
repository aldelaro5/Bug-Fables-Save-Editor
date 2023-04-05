using BugFablesLib;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor;

public abstract class ObservableModel : ObservableRecipient
{
  protected ObservableModel(IBfSerializable underlyingData) => UnderlyingData = underlyingData;
  public virtual IBfSerializable UnderlyingData { get; }
}
