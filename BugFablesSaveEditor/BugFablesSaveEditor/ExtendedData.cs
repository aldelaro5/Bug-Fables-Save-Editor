using System.Collections.Generic;
using System.IO;
using System.Linq;
using BugFablesLib.Data;

namespace BugFablesSaveEditor;

public static class ExtendedData
{
  public static Dictionary<int, string[]> CrystalBerriesDetails { get; }
  public static Dictionary<int, string[]> FlagsDetails { get; }
  public static Dictionary<int, string[]> FlagvarsDetails { get; }
  public static Dictionary<int, string[]> FlagstringsDetails { get; }
  public static IReadOnlyDictionary<string, Dictionary<int, string[]>> RegionalFlagsDetails { get; }

  static ExtendedData()
  {
    CrystalBerriesDetails = new();
      //ReadFromFile($"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/CrystalBerriesDetails.csv");
    FlagsDetails = new();//ReadFromFile($"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/FlagsDetails.csv");
    FlagvarsDetails = new();//ReadFromFile($"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/FlagvarsDetails.csv");
    FlagstringsDetails = new();//ReadFromFile($"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/FlagstringsDetails.csv");

    IReadOnlyList<string> areaNames = BugFablesLib.Utils.GetAllBfNames(new BfArea());
    var regionals = new Dictionary<string, Dictionary<int, string[]>>();
    foreach (var name in areaNames)
    {
      Dictionary<int, string[]> data = new();//ReadFromFile($"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/Regionals/{name}.csv");
      regionals.Add(name, data);
    }

    RegionalFlagsDetails = regionals;
  }

  private static Dictionary<int, string[]> ReadFromFile(string file)
  {
    string[] fileData = File.ReadAllLines(file);
    var result = new Dictionary<int, string[]>();

    foreach (string s in fileData)
    {
      string[] data = s.Split(';');
      int index = int.Parse(data[0]);
      result[index] = data.Skip(1).ToArray();
    }

    return result;
  }
}
