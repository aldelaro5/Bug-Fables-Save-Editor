using System.Threading.Tasks;

namespace BugFablesLib;

public interface IBfSaveFileFormat
{
  internal Task<string> DecodeSaveDataFromSaveFile(byte[] data);
  internal Task<byte[]> EncodeSaveFilesFromSaveData(string saveData);
}
