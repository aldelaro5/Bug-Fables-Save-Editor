namespace BugFablesSaveEditor.BugFablesSave
{
  public interface IBugFablesSaveSection
  {
    public object Data { get; set; }

    public void ResetToDefault();
    public void ParseFromSaveLine(string saveLine);
    public string EncodeToSaveLine();
  }
}
