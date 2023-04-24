namespace BugFablesLib;

public interface IBfSaveFileFormat
{
  internal string DecodeSaveDataFromSaveFile(byte[] data);
  internal byte[] EncodeSaveFilesFromSaveData(string saveData);
}
