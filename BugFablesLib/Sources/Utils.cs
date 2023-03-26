using System;
using System.Collections.Generic;
using System.Globalization;
using BugFablesLib.Data;

namespace BugFablesLib;

public static class Utils
{
  internal const string CommaSeparator = ",";
  internal const string AtSymbolSeparator = "@";

  internal static T ParseValueType<T>(string str, string fieldName)
    where T : struct, IConvertible
  {
    if (string.IsNullOrWhiteSpace(str))
      return default;

    try
    {
      return (T)Convert.ChangeType(str, typeof(T), CultureInfo.InvariantCulture);
    }
    catch (Exception)
    {
      throw new Exception(
        $"{fieldName} with the value {str} is not a valid {typeof(T).Name} value");
    }
  }

  public static IReadOnlyList<string> GetAllBfNames<T>(T namedId)
    where T : BfNamedId => namedId.VanillaNames;
}
