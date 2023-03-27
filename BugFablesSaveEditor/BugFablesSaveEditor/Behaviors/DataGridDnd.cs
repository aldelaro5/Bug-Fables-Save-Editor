using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactions.DragAndDrop;

namespace BugFablesSaveEditor.Behaviors;

public class DataGridDnd<T> : DropHandlerBase where T : class
{
  private (IList<T> list, int srcIndex, int destIndex)? Validate(
    object? sender, DragEventArgs e, object? sourceContext)
  {
    if (sender is not DataGrid dg ||
        sourceContext is not T src ||
        dg.Items is not IList<T> list ||
        dg.GetVisualAt(e.GetPosition(dg)) is not Control { DataContext: T dest })
    {
      return null;
    }

    return (list, list.IndexOf(src), list.IndexOf(dest));
  }

  public override bool Validate(object? sender, DragEventArgs e, object? sourceContext,
                                object? targetContext, object? state)
  {
    return Validate(sender, e, sourceContext) is not null;
  }

  public override bool Execute(object? sender, DragEventArgs e, object? sourceContext,
                               object? targetContext, object? state)
  {
    var result = Validate(sender, e, sourceContext);
    if (result is null)
      return false;

    MoveItem(result.Value.list, result.Value.srcIndex, result.Value.destIndex);
    return true;
  }
}
