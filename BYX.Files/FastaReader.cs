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
        
        string? header = reader.ReadLine();
        if (header is null || header.Length == 0 || header[0] != '>')
        {
            throw new FormatException("Expected header at line 1");
        }
        int nameDescriptionSeparator = header.IndexOf(' ');
        string name;
        string? description;
        if (nameDescriptionSeparator == -1)
        {
            name = header[1..];
            description = null;
        }
        else
        { 
            name = header[1..nameDescriptionSeparator];
            description = header[(nameDescriptionSeparator + 1)..];
        }

        string? sequenceString = reader.ReadLine();
        if (sequenceString is null)
        {
            throw new FormatException("Expected sequence at line 2");
        }

        Sequence sequence = new(sequenceString, _alphabet, name, description);

        return sequence;
    }
}