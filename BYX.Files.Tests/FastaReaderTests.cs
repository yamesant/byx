using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using BYX.Core;

namespace BYX.Files.Tests;

public sealed class FastaReaderTests
{
    private readonly IFileSystem _fileSystem = new MockFileSystem();
    private readonly string _filename = "test-file.fasta";
    
    [Fact]
    public void CanReadSequence()
    {
        // Arrange
        string fileContent = """
                             >seq 100
                             GGTTCC

                             """;
        using StreamWriter writer = _fileSystem.File.CreateText(_filename);
        writer.Write(fileContent);
        writer.Close();
        Sequence expectedSequence = new("GGTTCC", Alphabet.Dna, "seq", "100");
        FastaReader reader = new(Alphabet.Dna, _filename, _fileSystem);

        // Act
        Sequence sequence = reader.ReadFirst();

        // Assert
        sequence.ShouldNotBeNull();
        sequence.ShouldBe(expectedSequence);
    }
    
    [Fact]
    public void CanReadMissingDescription()
    {
        // Arrange
        string fileContent = """
                             >seq
                             GGTTCC

                             """;
        using StreamWriter writer = _fileSystem.File.CreateText(_filename);
        writer.Write(fileContent);
        writer.Close();
        Sequence expectedSequence = new("GGTTCC", Alphabet.Dna, "seq");
        FastaReader reader = new(Alphabet.Dna, _filename, _fileSystem);

        // Act
        Sequence sequence = reader.ReadFirst();

        // Assert
        sequence.ShouldNotBeNull();
        sequence.ShouldBe(expectedSequence);
    }
    
    [Fact]
    public void CanReadUnnamed()
    {
        // Arrange
        string fileContent = """
                             >
                             GGTTCC

                             """;
        using StreamWriter writer = _fileSystem.File.CreateText(_filename);
        writer.Write(fileContent);
        writer.Close();
        Sequence expectedSequence = new("GGTTCC", Alphabet.Dna);
        FastaReader reader = new(Alphabet.Dna, _filename, _fileSystem);

        // Act
        Sequence sequence = reader.ReadFirst();

        // Assert
        sequence.ShouldNotBeNull();
        sequence.ShouldBe(expectedSequence);
    }
    
    [Fact]
    public void CanReadEmptySequence()
    {
        // Arrange
        string fileContent = """
                             >seq
                             
                             
                             """;
        using StreamWriter writer = _fileSystem.File.CreateText(_filename);
        writer.Write(fileContent);
        writer.Close();
        Sequence expectedSequence = new("", Alphabet.Dna, "seq");
        FastaReader reader = new(Alphabet.Dna, _filename, _fileSystem);

        // Act
        Sequence sequence = reader.ReadFirst();

        // Assert
        sequence.ShouldNotBeNull();
        sequence.ShouldBe(expectedSequence);
    }
}