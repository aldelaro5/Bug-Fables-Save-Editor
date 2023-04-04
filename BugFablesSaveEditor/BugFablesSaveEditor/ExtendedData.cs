using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BugFablesLib.Data;

namespace BugFablesSaveEditor;

public static class ExtendedData
{
  public static IReadOnlyList<string[]> CrystalBerriesDetails { get; }
  public static IReadOnlyList<string> FlagsDetails { get; }
  public static IReadOnlyList<string> FlagvarsDetails { get; }
  public static IReadOnlyList<string> FlagstringsDetails { get; }
  public static IReadOnlyDictionary<string, string[]> RegionalFlagsDetails { get; }


  static ExtendedData()
  {
    CrystalBerriesDetails = File
      .ReadAllLines(
        $"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/CrystalBerriesDetails.csv")
      .Select(x => x.Split(";").Skip(1).ToArray()).ToList();

    FlagsDetails = File
      .ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/FlagsDetails.csv")
      .Select(x => x.Split(";")[1]).ToList();

    FlagvarsDetails = File
      .ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/FlagvarsDetails.csv")
      .Select(x => x.Split(";")[1]).ToList();

    FlagstringsDetails = File
      .ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/FlagstringsDetails.csv")
      .Select(x => x.Split(";")[1]).ToList();

    IReadOnlyList<string> areaNames = BugFablesLib.Utils.GetAllBfNames(new BfArea());
    var regionals = new Dictionary<string, string[]>();
    for (int i = 0; i < areaNames.Count; i++)
    {
      string name = areaNames[i];
      string[] data = File
        .ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/Regionals/{name}.csv")
        .Select(x => x.Split(";")[1]).ToArray();
      regionals.Add(name, data);
    }

    RegionalFlagsDetails = regionals;
  }
}
