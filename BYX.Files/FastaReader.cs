using System.IO.Abstractions;
using BYX.Core;

namespace BYX.Files;

public sealed class FastaReader
{
    private readonly Alphabet _alphabet;
    private readonly string _path;
    private readonly IFileSystem _fileSystem;
    
    public FastaReader(Alphabet alphabet, string path, IFileSystem? fileSystem = null)
    {
        _alphabet = alphabet;
        _path = path;
        _fileSystem = fileSystem ?? new FileSystem();
    }
    
    public Sequence ReadFirst()
    {
        using StreamReader reader = _fileSystem.File.OpenText(_path);
        throw new NotImplementedException();
    }
}