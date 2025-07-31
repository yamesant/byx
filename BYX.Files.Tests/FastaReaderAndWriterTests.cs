using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using BYX.Core;

namespace BYX.Files.Tests;

public sealed class FastaReaderAndWriterTests
{
    private readonly IFileSystem _fileSystem = new MockFileSystem();
    private readonly string _filename = "test-file.fasta";
    
    public static IEnumerable<object[]> GetSequences()
    {
        yield return [
            new Sequence("AAATT", Alphabet.Dna, "", "sequence description goes here")
        ];
        yield return [
            new Sequence("KLLVGRSTL", Alphabet.Protein, "protein_seq")
        ];
    }

    [Theory]
    [MemberData(nameof(GetSequences))]
    public void WriteReadPreservesSequence(Sequence sequence)
    {
        // Arrange
        FastaWriter writer = new(_filename, _fileSystem);
        FastaReader reader = new(sequence.Alphabet, _filename, _fileSystem);

        // Act
        writer.Write(sequence);
        Sequence otherSequence = reader.ReadFirst();

        // Assert
        otherSequence.ShouldNotBeNull();
        otherSequence.ShouldBe(sequence);
    }
}