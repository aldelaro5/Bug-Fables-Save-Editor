using System;
using System.Text;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.CrystalBerries;

namespace BugFablesSaveEditor.ViewModels;

public partial class CrystalBerriesViewModel : ViewModelBase
{
  [ObservableProperty]
  private CrystalBerry[] _crystalBerries;

  [ObservableProperty]
  private DataGridCollectionView _crystalBerriesFiltered;

  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private string _textFilter;

  partial void OnTextFilterChanged(string value)
  {
    _crystalBerriesFiltered.Refresh();
  }

  private string[] _areas;

  public CrystalBerriesViewModel()
  {
    SaveData = new SaveData();
    Initialise();
  }

  public CrystalBerriesViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Initialise();
  }

  private void Initialise()
  {
    _areas = Common.GetEnumDescriptions<Area>();
    CrystalBerries = (CrystalBerry[])SaveData.Sections[SaveFileSection.CrystalBerries].Data;
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
