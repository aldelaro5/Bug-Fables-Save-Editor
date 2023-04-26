using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BugFablesLib.SaveData;

namespace BugFablesLib;

public class BfXboxPcSaveDataFormat : IBfSaveFileFormat
{
  private const int NbrFiles = 4;
  private const int BlockSize = 0x4000;
  private int _selectedFileIndex = -1;
  private readonly HeaderSaveData _header = new();
  private string[] _originalData = { "", "", "", "" };
  private readonly Func<string[], Task<int>> _chooseFileCallback;

  public BfXboxPcSaveDataFormat(Func<string[], Task<int>> chooseFileCallback)
  {
    _chooseFileCallback = chooseFileCallback;
  }

  public async Task<string> DecodeSaveDataFromSaveFile(byte[] data)
  {
    _originalData = new[] { "", "", "", "" };
    _selectedFileIndex = -1;
    try
    {
      using BinaryReader reader = new(new MemoryStream(data));
      uint nbrFiles = reader.ReadUInt32();
      if (nbrFiles != NbrFiles)
        throw new IOException($"Invalid number of files, expected {NbrFiles}, got {nbrFiles}");

      _originalData[0] = reader.ReadString();
      for (int i = 1; i < NbrFiles; i++)
        _originalData[i] = reader.ReadString();

      string[] fileNames = _originalData.Skip(1).Select(x =>
      {
        if (string.IsNullOrEmpty(x))
          return "";

        string headerStr = x.Split(Utils.LineSeparator)[(int)BfSaveData.SaveFileSection.Header];
        _header.Deserialize(headerStr);
        return _header.FileName;
      }).ToArray();

      _selectedFileIndex = await _chooseFileCallback(fileNames);
      if (_selectedFileIndex < 0 || _selectedFileIndex >= NbrFiles - 1 ||
          string.IsNullOrEmpty(_originalData[_selectedFileIndex + 1]))
        throw new Exception($"Invalid file index {_selectedFileIndex}");

      return _originalData[_selectedFileIndex + 1];
    }
    catch (Exception e)
    {
      throw new IOException($"Error reading blob file {e}");
    }
  }

  public Task<byte[]> EncodeSaveFilesFromSaveData(string saveData)
  {
    if (_selectedFileIndex == -1)
    {
      throw new Exception($"You have called {nameof(EncodeSaveFilesFromSaveData)} without " +
                          $"calling {nameof(DecodeSaveDataFromSaveFile)} first");
    }

    BinaryWriter writer = new(new MemoryStream());
    writer.Write(NbrFiles);
    writer.Write(_originalData[0]);
    for (int i = 1; i < NbrFiles; i++)
    {
      if (string.IsNullOrEmpty(_originalData[i]))
        writer.Write((byte)0);
      else
        writer.Write(i == _selectedFileIndex + 1 ? saveData : _originalData[i]);
    }

    byte[] save = ((MemoryStream)writer.BaseStream).ToArray();
    int nbrBlocks = 1;
    while (nbrBlocks * BlockSize < save.Length)
      nbrBlocks *= 2;
    byte[] data = new byte[nbrBlocks * BlockSize];
    Array.Copy(save, data, save.Length);
    return Task.FromResult(data);
  }
}
