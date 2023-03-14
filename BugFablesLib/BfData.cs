using System;
using System.Globalization;

namespace BugFablesLib;

public abstract class BfData
{
  public abstract void ResetToDefault();
  public abstract void Parse(string str);
  public abstract override string ToString();

  protected T ParseField<T>(string str, string fieldName)
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
        $"{fieldName} with the value {str} is not a valid {nameof(T)} value");
    }
  }
}
