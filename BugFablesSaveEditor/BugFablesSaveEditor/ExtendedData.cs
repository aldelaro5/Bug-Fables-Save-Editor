using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BugFablesSaveEditor;

public static class ExtendedData
{
  public static IReadOnlyList<string[]> CrystalBerriesDetails { get; }

  static ExtendedData()
  {
    CrystalBerriesDetails = File
      .ReadAllLines(
        $"{AppDomain.CurrentDomain.BaseDirectory}/ExtendedData/CrystalBerriesDetails.csv")
      .Select(x => x.Split(";").Skip(1).ToArray()).ToList();
  }
}
