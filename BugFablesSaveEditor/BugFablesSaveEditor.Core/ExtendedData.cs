using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using BugFablesLib.Data;

namespace BugFablesSaveEditor.Core;

public static class ExtendedData
{
  public static Dictionary<int, string[]> CrystalBerriesDetails { get; }
  public static Dictionary<int, string[]> FlagsDetails { get; }
  public static Dictionary<int, string[]> FlagvarsDetails { get; }
  public static Dictionary<int, string[]> FlagstringsDetails { get; }
  public static IReadOnlyDictionary<string, Dictionary<int, string[]>> RegionalFlagsDetails { get; }

  static ExtendedData()
  {
    string basePath = $"avares://{typeof(App).Assembly.GetName().Name}/Assets";
    CrystalBerriesDetails = new(ReadFromAssetPath($"{basePath}/ExtendedData/CrystalBerriesDetails.csv"));
    FlagsDetails = new(ReadFromAssetPath($"{basePath}/ExtendedData/FlagsDetails.csv"));
    FlagvarsDetails = new(ReadFromAssetPath($"{basePath}/ExtendedData/FlagvarsDetails.csv"));
    FlagstringsDetails = new(ReadFromAssetPath($"{basePath}/ExtendedData/FlagstringsDetails.csv"));

    IReadOnlyList<string> areaNames = BugFablesLib.Utils.GetAllBfNames(new BfArea());
    Dictionary<string, Dictionary<int, string[]>> regionals = new();
    foreach (string name in areaNames)
    {
      string fileName = name.Replace('\'', ' ');
      Dictionary<int, string[]> data = new(ReadFromAssetPath($"{basePath}/ExtendedData/Regionals/{fileName}.csv"));
      regionals.Add(name, data);
    }

    RegionalFlagsDetails = regionals;
  }

  private static Dictionary<int, string[]> ReadFromAssetPath(string file)
  {
    Stream crystalBerriesDataStream = AssetLoader.Open(new Uri(file));
    StreamReader crystalBerriesDataStreamReader = new(crystalBerriesDataStream);
    string[] fileData = crystalBerriesDataStreamReader.ReadToEnd().Trim().Split('\n');
    Dictionary<int, string[]> result = new();

    foreach (string s in fileData)
    {
      if (string.IsNullOrEmpty(s))
        continue;

      string[] data = s.Split(';');
      int index = int.Parse(data[0]);
      result[index] = data.Skip(1).ToArray();
    }

    return result;
  }
}
