using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using BYX.Core;

namespace BYX.Files.Tests;

public sealed class FastaWriterTests
{
    private readonly IFileSystem _fileSystem = new MockFileSystem();
    private readonly string _filename = "test-file.fasta";
    
    [Fact]
    public void CanWriteSequence()
    {
        // Arrange
        Sequence sequence = new("AAATT", Alphabet.Dna, "test_sequence", "sequence description goes here");
        FastaWriter writer = new(_filename, _fileSystem);
        string expectedFileContent = """
                                     >test_sequence sequence description goes here
                                     AAATT

                                     """;

        // Act
        writer.Write(sequence);
        using StreamReader reader = _fileSystem.File.OpenText(_filename);
        string fileContent = reader.ReadToEnd();

        // Assert
        fileContent.ShouldBe(expectedFileContent);
    }
    
    [Fact]
    public void CanWriteUnnamedSequence()
    {
        // Arrange
        Sequence sequence = new("CCGTAC", Alphabet.Dna);
        FastaWriter writer = new(_filename, _fileSystem);
        string expectedFileContent = """
                                     >unnamed
                                     CCGTAC

                                     """;

        // Act
        writer.Write(sequence);
        using StreamReader reader = _fileSystem.File.OpenText(_filename);
        string fileContent = reader.ReadToEnd();

        // Assert
        fileContent.ShouldBe(expectedFileContent);
    }
}