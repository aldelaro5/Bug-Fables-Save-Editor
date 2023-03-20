using System.Collections.Generic;
using BugFablesLib;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public class ObservableBfResource : ObservableObject
{
  private readonly BfResource _resource;

  public int Id
  {
    get => _resource.Id;
    set => SetProperty(_resource.Id, value, _resource, (resource, n) => resource.Id = n);
  }

  public IReadOnlyList<string> AllResourceNames => _resource.AllNames;

  public ObservableBfResource(BfResource resource) => _resource = resource;
}
