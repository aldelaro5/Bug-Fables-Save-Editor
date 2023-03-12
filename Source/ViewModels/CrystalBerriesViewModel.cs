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
  private readonly string[] _areas;

  [ObservableProperty]
  private IList<CrystalBerry> _crystalBerries = null!;

  [ObservableProperty]
  private DataGridCollectionView _crystalBerriesFiltered = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  private string _textFilter = null!;

  partial void OnTextFilterChanged(string value)
  {
    CrystalBerriesFiltered.Refresh();
  }

  public CrystalBerriesViewModel() : this(new SaveData())
  {
  }

  public CrystalBerriesViewModel(SaveData saveData)
  {
    SaveData = saveData;
    _areas = Utils.GetEnumDescriptions<Area>();
    CrystalBerries = SaveData.CrystalBerries.List;
    CrystalBerriesFiltered = new DataGridCollectionView(CrystalBerries);
    CrystalBerriesFiltered.Filter = FilterCrystalBerries;
  }

  private bool FilterCrystalBerries(object arg)
  {
    if (string.IsNullOrEmpty(TextFilter))
    {
      return true;
    }

    CrystalBerry crystalBerry = (CrystalBerry)arg;
    StringBuilder sb = new();
    sb.Append(crystalBerry.Index).Append(' ');
    sb.Append(_areas[(int)crystalBerry.Area]).Append(' ');
    sb.Append(crystalBerry.Description);
    return sb.ToString().Contains(TextFilter, StringComparison.OrdinalIgnoreCase);
  }

  [RelayCommand]
  private void ToggleAllFiltered()
  {
    bool newObtained = true;
    foreach (object? item in CrystalBerriesFiltered)
    {
      CrystalBerry cb = (CrystalBerry)item;
      if (cb.Obtained)
      {
        newObtained = false;
        break;
      }
    }

    foreach (object? item in CrystalBerriesFiltered)
    {
      CrystalBerry cb = (CrystalBerry)item;
      cb.Obtained = newObtained;
    }
  }
}
