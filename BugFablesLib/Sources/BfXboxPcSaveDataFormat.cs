using System;
using System.Collections.Generic;
using System.IO;

namespace BugFablesLib;

public class BfXboxPcSaveDataFormat : IBfSaveFileFormat
{
  private const int NbrFiles = 4;
  private const int BlockSize = 0x4000;
  private int _selectedFileIndex = -1;
  private string?[] _originalData = { null, null, null, null };
  private readonly Func<IList<string>, int> _chooseFileCallback;

  public BfXboxPcSaveDataFormat(Func<IList<string>, int> chooseFileCallback)
  {
    _chooseFileCallback = chooseFileCallback;
  }

  public string DecodeSaveDataFromSaveFile(byte[] data)
  {
    _originalData = new string?[] { null, null, null, null };
    _selectedFileIndex = -1;
    List<string> files = new();
    try
    {
      using BinaryReader reader = new(new MemoryStream(data));
      uint nbrFiles = reader.ReadUInt32();
      if (nbrFiles != NbrFiles)
        throw new IOException($"Invalid number of files, expected {NbrFiles}, got {nbrFiles}");

      _originalData[0] = reader.ReadString();
      for (int i = 1; i < NbrFiles; i++)
      {
        _originalData[i] = reader.ReadString();
        files.Add(_originalData[i]!);
      }

      _selectedFileIndex = _chooseFileCallback(files);
      if (_selectedFileIndex < 0 || _selectedFileIndex >= files.Count)
        throw new Exception($"Invalid file index {_selectedFileIndex}");

      return files[_selectedFileIndex];
    }
    catch (Exception e)
    {
      throw new IOException($"Error reading blob file {e}");
    }
  }

  public byte[] EncodeSaveFilesFromSaveData(string saveData)
  {
    if (_selectedFileIndex == -1)
    {
      throw new Exception($"You have called {nameof(EncodeSaveFilesFromSaveData)} without " +
                          $"calling {nameof(DecodeSaveDataFromSaveFile)} first");
    }

    BinaryWriter writer = new(new MemoryStream());
    writer.Write(NbrFiles);
    writer.Write(_originalData[0]!);
    for (int i = 1; i < NbrFiles; i++)
    {
      if (string.IsNullOrEmpty(_originalData[i]))
        break;

      writer.Write(i == _selectedFileIndex + 1 ? saveData : _originalData[i]!);
    }

    byte[] save = ((MemoryStream)writer.BaseStream).ToArray();
    int nbrBlocks = 1;
    while (nbrBlocks * BlockSize < save.Length)
      nbrBlocks *= 2;
    byte[] data = new byte[nbrBlocks * BlockSize];
    Array.Copy(save, data, save.Length);
    return data;
  }
}
