using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Avalonia.Xaml.Interactions.DragAndDrop;

namespace BugFablesSaveEditor.Core.Behaviors;

/// <summary>
/// A behavior that enables drag and drop between DataGridRows of the same underlying type.
/// This supports drag and drop between two different DataGrids.
/// </summary>
/// <typeparam name="T">The underlying type of the items</typeparam>
public class DataGridDnd<T> : DropHandlerBase where T : class
{
  private const string DraggingUpClassName = "dragging-up";
  private const string DraggingDownClassName = "dragging-down";

  private enum DragDirection
  {
    Up,
    Down
  }

  private struct DndData
  {
    public DndData() { }
    public DataGrid? SrcDataGrid = null;
    public DataGrid DestDataGrid = null!;
    public IList<T> SrcList = null!;
    public IList<T> DestList = null!;
    public int SrcIndex = -1;

    public int DestIndex = -1;
    public DragDirection Direction;
  }

  private DndData _dnd = new();

  public override bool Validate(object? sender, DragEventArgs e, object? sourceContext,
                                object? targetContext, object? state)
  {
    return Validate(sender, e, sourceContext);
  }

  public override bool Execute(object? sender, DragEventArgs e, object? sourceContext,
                               object? targetContext, object? state)
  {
    if (!Validate(sender, e, sourceContext))
    {
      DataGridDragHandlder.CurrentDndSourceDataGrid = null;
      return false;
    }

    if (!Equals(_dnd.SrcDataGrid, _dnd.DestDataGrid) && _dnd.Direction == DragDirection.Down)
      _dnd.DestIndex++;
    else if (_dnd.SrcIndex > _dnd.DestIndex && _dnd.Direction == DragDirection.Down)
      _dnd.DestIndex++;
    else if (_dnd.SrcIndex < _dnd.DestIndex && _dnd.Direction == DragDirection.Up)
      _dnd.DestIndex--;

    MoveItem(_dnd.SrcList, _dnd.DestList, _dnd.SrcIndex, _dnd.DestIndex);
    _dnd.DestDataGrid.SelectedIndex = _dnd.DestIndex;
    _dnd.DestDataGrid.ScrollIntoView(_dnd.DestList[_dnd.DestIndex], null);
    DataGridDragHandlder.CurrentDndSourceDataGrid = null;
    return true;
  }

  public override void Enter(object? sender, DragEventArgs e, object? sourceContext,
                             object? targetContext)
  {
    if (!Validate(sender, e, sourceContext))
    {
      e.DragEffects = DragDropEffects.None;
      e.Handled = true;
      return;
    }

    string className = _dnd.Direction switch
    {
      DragDirection.Down => DraggingDownClassName,
      DragDirection.Up => DraggingUpClassName,
      _ => throw new UnreachableException($"Invalid drag direction: {_dnd.Direction}")
    };
    _dnd.DestDataGrid.Classes.Add(className);

    e.DragEffects |= DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link;
    e.Handled = true;
  }

  public override void Over(object? sender, DragEventArgs e, object? sourceContext,
                            object? targetContext)
  {
    if (!Validate(sender, e, sourceContext))
    {
      e.DragEffects = DragDropEffects.None;
      e.Handled = true;
      return;
    }

    e.DragEffects |= DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link;
    e.Handled = true;

    (string toAdd, string toRemove) classUpdate = _dnd.Direction switch
    {
      DragDirection.Down => (DraggingDownClassName, DraggingUpClassName),
      DragDirection.Up => (DraggingUpClassName, DraggingDownClassName),
      _ => throw new UnreachableException($"Invalid drag direction: {_dnd.Direction}")
    };
    if (_dnd.DestDataGrid.Classes.Contains(classUpdate.toAdd))
      return;

    _dnd.DestDataGrid.Classes.Remove(classUpdate.toRemove);
    _dnd.DestDataGrid.Classes.Add(classUpdate.toAdd);
  }

  public override void Leave(object? sender, RoutedEventArgs e)
  {
    base.Leave(sender, e);
    RemoveDraggingClass(sender as DataGrid);
  }

  public override void Drop(object? sender, DragEventArgs e, object? sourceContext,
                            object? targetContext)
  {
    RemoveDraggingClass(sender as DataGrid);
    base.Drop(sender, e, sourceContext, targetContext);
    DataGridDragHandlder.CurrentDndSourceDataGrid = null;
  }

  private bool Validate(object? sender, DragEventArgs e, object? sourceContext)
  {
    if (DataGridDragHandlder.CurrentDndSourceDataGrid is not { } srcDg ||
        sender is not DataGrid destDg ||
        sourceContext is not T src ||
        srcDg.ItemsSource is not IList<T> srcList ||
        destDg.ItemsSource is not IList<T> destList ||
        srcList.IndexOf(src) == -1 ||
        destDg.GetVisualAt(e.GetPosition(destDg),
          v => v.FindDescendantOfType<DataGridCell>() is not null) is not Control
        {
          DataContext: T dest
        } visual)
    {
      return false;
    }

    DataGridCell cell = visual.FindDescendantOfType<DataGridCell>()!;
    var pos = e.GetPosition(cell);

    _dnd.SrcDataGrid = srcDg;
    _dnd.DestDataGrid = destDg;
    _dnd.SrcList = srcList;
    _dnd.DestList = destList;
    _dnd.Direction = cell.DesiredSize.Height / 2 > pos.Y ? DragDirection.Up : DragDirection.Down;
    _dnd.SrcIndex = srcList.IndexOf(src);
    _dnd.DestIndex = destList.IndexOf(dest);

    return true;
  }


  private static void RemoveDraggingClass(DataGrid? dg)
  {
    if (dg is not null && !dg.Classes.Remove(DraggingUpClassName))
      dg.Classes.Remove(DraggingDownClassName);
  }
}
