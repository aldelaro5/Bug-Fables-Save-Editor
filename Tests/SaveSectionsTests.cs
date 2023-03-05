using System.Globalization;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Utils;
using Xunit;

namespace BugFablesSaveEditor.Tests;

public class SaveSectionsTests
{
  private const string DecodedSaveFileName = "SaveFiles/DecodedTextSave.txt";

  private readonly string[] saveLines = File.ReadAllLines(DecodedSaveFileName);

  public static IEnumerable<object[]> AllSaveFileSections()
  {
    for (int i = 0; i < (int)SaveFileSection.COUNT; i++)
    {
      yield return new object[] { (SaveFileSection)i };
    }
  }

  [Theory]
  [MemberData(nameof(AllSaveFileSections))]
  public void Section_ShouldNotChange_WhenOverWrittenWithoutChange(SaveFileSection section)
  {
    // Test messing with the culture to make sure it doesn't break the parsing
    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
    Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-fr");
    string strBefore = saveLines[(int)section];
    IBugFablesSaveSection sud = new SaveData().Sections[section];
    sud.ParseFromSaveLine(saveLines[(int)section]);
    string strAfter = sud.EncodeToSaveLine();
    Thread.CurrentThread.CurrentCulture = cultureInfo;
    Assert.Equal(strBefore, strAfter);
  }

  [Theory]
  [InlineData(SaveFileSection.Header, Common.FieldSeparator)]
  [InlineData(SaveFileSection.PartyMembers, Common.ElementSeparator)]
  [InlineData(SaveFileSection.Global, Common.FieldSeparator)]
  [InlineData(SaveFileSection.Quests, Common.ElementSeparator)]
  [InlineData(SaveFileSection.Items, Common.ElementSeparator)]
  [InlineData(SaveFileSection.Medals, Common.ElementSeparator)]
  [InlineData(SaveFileSection.SamiraSongs, Common.ElementSeparator)]
  [InlineData(SaveFileSection.StatBonuses, Common.ElementSeparator)]
  [InlineData(SaveFileSection.Library, Common.ElementSeparator)]
  [InlineData(SaveFileSection.EnemyEncounters, Common.ElementSeparator)]
  public void HeaderParsing_ShouldThrow_WhenIncorrectFieldsAmount(SaveFileSection section, string separator)
  {
    string sectionData = saveLines[(int)section];
    IBugFablesSaveSection sud = new SaveData().Sections[section];
    // One field too much
    sectionData += $"{separator}0";
    Assert.ThrowsAny<Exception>(() => sud.ParseFromSaveLine(sectionData));
    // One field too low
    sectionData = sectionData.Split(separator).Skip(2)
      .Aggregate((acc, field) => $"{acc}{separator}{field}");
    Assert.ThrowsAny<Exception>(() => sud.ParseFromSaveLine(sectionData));
  }
}
