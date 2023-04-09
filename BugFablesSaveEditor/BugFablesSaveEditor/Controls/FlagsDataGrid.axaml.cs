using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace BugFablesSaveEditor.Controls;

public partial class FlagsDataGrid : DataGrid, IStyleable
{
  Type IStyleable.StyleKey => typeof(DataGrid);

  public FlagsDataGrid() => InitializeComponent();
}
