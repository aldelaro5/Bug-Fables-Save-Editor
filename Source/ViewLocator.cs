using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor;

public class ViewLocator : IDataTemplate
{
  public IControl Build(object? data)
  {
    string? name = data?.GetType().FullName!.Split('`')[0].Replace("ViewModel", "View");
    Type? type = name is not null ? Type.GetType(name) : null;
    if (type != null)
      return (Control)Activator.CreateInstance(type)!;

    return new TextBlock { Text = "Not Found: " + name };
  }

  public bool Match(object? data)
  {
    return data is ObservableObject;
  }
}
