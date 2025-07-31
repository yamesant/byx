using System.IO.Abstractions;
using BYX.Core;

namespace BYX.Files;

public sealed class FastaWriter
{
    private readonly string _path;
    private readonly IFileSystem _fileSystem;
    
    public FastaWriter(string path, IFileSystem? fileSystem = null)
    {
        _path = path;
        _fileSystem = fileSystem ?? new FileSystem();
    }

    public void Write(Sequence sequence)
    {
        using StreamWriter writer = _fileSystem.File.CreateText(_path);
        string header = ">" + sequence.Name + (sequence.Description is null ? "" : " " + sequence.Description);
        writer.WriteLine(header);
        writer.WriteLine(sequence.Characters);
    }
}