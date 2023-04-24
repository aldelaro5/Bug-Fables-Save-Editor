using System.Globalization;
using Xunit;
using static BugFablesLib.BfSaveData.SaveFileSection;

namespace BugFablesLib.Tests;

public class SaveSectionsTests
{
  private const string CommaSeparator = ",";
  private const string AtSymbolSeparator = "@";
  private const string DecodedSaveFileName = "SaveFiles/DecodedTextSave.txt";

  private readonly string[] saveLines = File.ReadAllLines(DecodedSaveFileName);

  public static IEnumerable<object[]> AllSaveFileSections()
  {
    for (int i = 0; i < (int)COUNT; i++)
      yield return new object[] { (BfSaveData.SaveFileSection)i };
  }

  [Theory]
  [MemberData(nameof(AllSaveFileSections))]
  public void Section_ShouldNotChange_WhenOverWrittenWithoutChange(
    BfSaveData.SaveFileSection section)
  {
    // Test messing with the culture to make sure it doesn't break the parsing
    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
    Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-fr");
    string strBefore = saveLines[(int)section];
    var sud = new BfSaveData().Data[(int)section];
    sud.Deserialize(saveLines[(int)section]);
    string strAfter = sud.Serialize();
    Thread.CurrentThread.CurrentCulture = cultureInfo;
    Assert.Equal(strBefore, strAfter);
  }

  [Theory]
  [InlineData(Header, CommaSeparator)]
  [InlineData(PartyMembers, AtSymbolSeparator)]
  [InlineData(Global, CommaSeparator)]
  [InlineData(Quests, AtSymbolSeparator)]
  [InlineData(Items, AtSymbolSeparator)]
  [InlineData(Medals, AtSymbolSeparator)]
  [InlineData(SamiraSongs, AtSymbolSeparator)]
  [InlineData(StatBonuses, AtSymbolSeparator)]
  [InlineData(Library, AtSymbolSeparator)]
  [InlineData(EnemyEncounters, AtSymbolSeparator)]
  public void SectionParsing_ShouldThrow_WhenIncorrectFieldsAmount(
    BfSaveData.SaveFileSection section, string separator)
  {
    string sectionData = saveLines[(int)section];
    var sud = new BfSaveData().Data[(int)section];
    // One field too much
    sectionData += $"{separator}0";
    Assert.ThrowsAny<Exception>(() => sud.Deserialize(sectionData));
    // One field too low
    sectionData = sectionData.Split(separator).Skip(2)
      .Aggregate((acc, field) => $"{acc}{separator}{field}");
    Assert.ThrowsAny<Exception>(() => sud.Deserialize(sectionData));
  }
}
