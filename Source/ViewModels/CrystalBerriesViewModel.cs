using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.CrystalBerries;

namespace BugFablesSaveEditor.ViewModels;

public partial class CrystalBerriesViewModel : ObservableObject
{
  private readonly string[] _areas = Utils.GetEnumDescriptions<Area>();

  [ObservableProperty]
  private IList<CrystalBerry> _crystalBerries;

  [ObservableProperty]
  private DataGridCollectionView _crystalBerriesFiltered;

  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private string _textFilter = "";

  partial void OnTextFilterChanged(string value) => CrystalBerriesFiltered.Refresh();

  [RelayCommand]
  private void ToggleAllFiltered()
  {
    bool newObtained = !CrystalBerries.Any(x => x.Obtained);
    foreach (CrystalBerry cb in CrystalBerries)
      cb.Obtained = newObtained;
    CrystalBerriesFiltered.Refresh();
  }

  public CrystalBerriesViewModel() : this(new SaveData())
  {
  }

  public CrystalBerriesViewModel(SaveData saveData)
  {
    _saveData = saveData;
    _crystalBerries = _saveData.CrystalBerries.List;
    _crystalBerriesFiltered = new(_crystalBerries);
    _crystalBerriesFiltered.Filter = arg =>
    {
      if (string.IsNullOrEmpty(TextFilter))
        return true;

      CrystalBerry crystalBerry = (CrystalBerry)arg;
      StringBuilder sb = new();
      sb.Append(crystalBerry.Index).Append(' ');
      sb.Append(_areas[(int)crystalBerry.Area]).Append(' ');
      sb.Append(crystalBerry.Description);
      return sb.ToString().Contains(TextFilter, StringComparison.OrdinalIgnoreCase);
    };
  }
}
