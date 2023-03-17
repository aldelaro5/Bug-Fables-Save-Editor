using System;
using System.Collections.ObjectModel;
using System.Text;

namespace BugFablesLib;

public class BfSerializableDataCollection<TBfSerializable> : Collection<TBfSerializable>, IBfSerializable
  where TBfSerializable : IBfSerializable, new()
{
  protected string ElementSeparator = Utils.AtSymbolSeparator;
  protected int NbrExpectedElements = -1;

  public BfSerializableDataCollection() { }

  public BfSerializableDataCollection(string elementSeparator, int nbrExpectedElements = -1)
  {
    ElementSeparator = elementSeparator;
    NbrExpectedElements = nbrExpectedElements;
  }

  public override string ToString() => Serialize();

  public void Deserialize(string str)
  {
    if (str == string.Empty)
      return;

    string[] elements = str.Split(new[] { ElementSeparator }, StringSplitOptions.None);
    if (NbrExpectedElements != -1 && elements.Length != NbrExpectedElements)
    {
      throw new FormatException(
        $"Expected {NbrExpectedElements} {typeof(TBfSerializable).Name} " +
        $"elements, but got {elements.Length}");
    }

    int i = 0;
    try
    {
      for (i = 0; i < elements.Length; i++)
      {
        if (i >= Count)
        {
          TBfSerializable newElement = new();
          newElement.Deserialize(elements[i]);
          Add(newElement);
        }
        else
        {
          this[i].Deserialize(elements[i]);
        }
      }
    }
    catch (FormatException e)
    {
      throw new FormatException(
        $"The {typeof(TBfSerializable).Name} at index {i} is in an invalid format",
        e);
    }
  }

  public string Serialize()
  {
    StringBuilder sb = new();
    for (int i = 0; i < Count; i++)
    {
      TBfSerializable element = this[i];
      sb.Append(element.Serialize());
      if (i != Count - 1)
        sb.Append(ElementSeparator);
    }

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    foreach (var element in this)
      element.ResetToDefault();
  }
}
