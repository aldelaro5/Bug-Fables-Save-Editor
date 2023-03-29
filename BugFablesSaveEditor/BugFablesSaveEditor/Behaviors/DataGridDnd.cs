using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactions.DragAndDrop;

namespace BugFablesSaveEditor.Behaviors;

public class DataGridDnd<T> : DropHandlerBase where T : class
{
  private const string DraggingUpClassName = "dragging-up";
  private const string DraggingDownClassName = "dragging-down";

  private (IList<T> list, DataGrid dg, int srcIndex, int destIndex)? Validate(
    object? sender, DragEventArgs e, object? sourceContext)
  {
    if (sender is not DataGrid dg ||
        sourceContext is not T src ||
        dg.Items is not IList<T> list ||
        dg.GetVisualAt(e.GetPosition(dg)) is not Control { DataContext: T dest })
    {
      return null;
    }

    return (list, dg, list.IndexOf(src), list.IndexOf(dest));
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

    var values = result.Value;
    MoveItem(values.list, values.srcIndex, values.destIndex);
    values.dg.SelectedIndex = values.destIndex;
    return true;
  }

  public override void Enter(object? sender, DragEventArgs e, object? sourceContext,
                             object? targetContext)
  {
    var result = Validate(sender, e, sourceContext);
    if (result is null)
    {
      e.DragEffects = DragDropEffects.None;
      e.Handled = true;
      return;
    }

    var values = result.Value;
    if (values.srcIndex != values.destIndex)
    {
      string className = values.srcIndex > values.destIndex
        ? DraggingUpClassName
        : DraggingDownClassName;
      values.dg.Classes.Add(className);
    }

    e.DragEffects |= DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link;
    e.Handled = true;
  }

  public override void Leave(object? sender, RoutedEventArgs e)
  {
    base.Leave(sender, e);
    RemoveDraggingClass(sender);
  }

  public override void Drop(object? sender, DragEventArgs e, object? sourceContext,
                            object? targetContext)
  {
    RemoveDraggingClass(sender);
    base.Drop(sender, e, sourceContext, targetContext);
  }

  private static void RemoveDraggingClass(object? sender)
  {
    DataGrid dg = (sender as DataGrid)!;
    if (!dg.Classes.Remove(DraggingUpClassName))
      dg.Classes.Remove(DraggingDownClassName);
  }
}
