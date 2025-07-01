namespace BYX.Core.Tests;

public sealed class SequenceTests
{
    [Fact]
    public void FailToCreateSequenceWithCharactersOutsideAlphabet()
    {
        // Arrange
        Alphabet alphabet = new("ABC");
        string characters = "BBCD";
        
        // Act
        Action action = () => _ = new Sequence(characters, alphabet);
        
        // Assert
        action.ShouldThrow<ArgumentException>();
    }

    [Fact]
    public void SameSequencesEqual()
    {
        // Arrange
        Sequence a = new("ABBA", new("ABC"));
        Sequence b = new("ABBA", new("ABC"));
        
        // Act
        bool result = a.Equals(b);
        
        // Assert
        result.ShouldBe(true);
    }
    
    [Fact]
    public void SameCharacterDifferentAlphabetSequencesNotEqual()
    {
        // Arrange
        Sequence a = new("ABBA", new("ABC"));
        Sequence b = new("ABBA", new("AB"));
        
        // Act
        bool result = a.Equals(b);
        
        // Assert
        result.ShouldBe(false);
    }

    [Fact]
    public void CanFormat()
    {
        // Arrange
        Sequence sequence = new("ATFATHAKSDDVKHALGETGFTFREVDDRLVGDYAPRWRA", Alphabet.Protein);
        int lineWidth = 10;
        string expectedString = """
                                ATFATHAKSD
                                DVKHALGETG
                                FTFREVDDRL
                                VGDYAPRWRA
                                """;
        
        // Act
        string result = sequence.ToFormattedString(lineWidth);
        
        // Assert
        result.ShouldBe(expectedString);
    }
}