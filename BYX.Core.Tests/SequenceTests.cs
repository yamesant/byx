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
        Sequence a = new("ABBA", new("ABC"), "454545", "Simple");
        Sequence b = new("ABBA", new("ABC"), "454545", "Simple");
        
        // Assert
        a.ShouldBe(b);
    }

    [Fact]
    public void DifferentCharactersNotEqual()
    {
        // Arrange
        Sequence a = new("AA", new("ABC"));
        Sequence b = new("ACC", new("ABC"));

        // Assert
        a.ShouldNotBe(b);
    }

    [Fact]
    public void DifferentAlphabetsNotEqual()
    {
        // Arrange
        Sequence a = new("ABBA", new("ABC"));
        Sequence b = new("ABBA", new("AB"));
        
        // Assert
        a.ShouldNotBe(b);
    }
    
    [Fact]
    public void DifferentNamesNotEqual()
    {
        // Arrange
        Sequence a = new("ABBA", new("AB"), "seq_a");
        Sequence b = new("ABBA", new("AB"), "seq_b");
        
        // Assert
        a.ShouldNotBe(b);
    }
    
    [Fact]
    public void DifferentDescriptionsNotEqual()
    {
        // Arrange
        Sequence a = new("ABBA", new("AB"), "seq", "1");
        Sequence b = new("ABBA", new("AB"), "seq", "2");
        
        // Assert
        a.ShouldNotBe(b);
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
    
    [Fact]
    public void NullNameGivenDefaultName()
    {
        // Arrange
        Sequence a = new("X", new("X"));
        
        // Assert
        a.Name.ShouldBe(Sequence.DefaultName);
    }
    
    [Fact]
    public void EmptyNameGivenDefaultName()
    {
        // Arrange
        Sequence a = new("X", new("X"), "");
        
        // Assert
        a.Name.ShouldBe(Sequence.DefaultName);
    }
    
    [Theory]
    [InlineData(" name")]
    [InlineData("name ")]
    [InlineData("na me")]
    public void SequenceNamesContainNoSpaces(string name)
    {
        // Act
        Action action = () => _ = new Sequence("AABBAA", new("BA"), name);

        // Assert
        action.ShouldThrow<ArgumentException>();
    }
}