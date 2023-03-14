using System;
using System.Collections.Generic;
using System.Text;

namespace BugFablesLib;

public abstract class BfDataList<T> : BfData
  where T : BfData, new()
{
  private readonly IList<T> _list = new List<T>();
  protected string ElementSeparator = Utils.PrimarySeparator;
  protected int NbrExpectedElements = -1;

  public IList<T> List
  {
    get => _list;
  }

  public override void Parse(string str)
  {
    if (str == string.Empty)
      return;

    string[] elements = str.Split(new[] { ElementSeparator }, StringSplitOptions.None);
    if (NbrExpectedElements != -1 && elements.Length != NbrExpectedElements)
    {
      throw new FormatException(
        $"Expected {NbrExpectedElements} {typeof(T).Name} " +
        $"elements, but got {elements.Length}");
    }

    int i = 0;
    try
    {
      for (i = 0; i < elements.Length; i++)
      {
        if (i >= List.Count)
        {
          T newElement = new();
          newElement.Parse(elements[i]);
          List.Add(newElement);
        }
        else
        {
          List[i].Parse(elements[i]);
        }
      }
    }
    catch (FormatException e)
    {
      throw new FormatException(
        $"The {typeof(T).Name} at index {i} is in an invalid format",
        e);
    }
  }

  public sealed override string ToString()
  {
    StringBuilder sb = new();
    for (int i = 0; i < List.Count; i++)
    {
      T element = List[i];
      sb.Append(element);
      if (i != List.Count - 1)
        sb.Append(ElementSeparator);
    }

    return sb.ToString();
  }

  public sealed override void ResetToDefault()
  {
    foreach (T element in List)
      element.ResetToDefault();
  }
}
