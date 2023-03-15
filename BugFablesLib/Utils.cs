using System;
using System.Globalization;

namespace BugFablesLib;

internal static class Utils
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
}
