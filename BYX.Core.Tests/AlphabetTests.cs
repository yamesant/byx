using Shouldly;

namespace BYX.Core.Tests;

public sealed class AlphabetTests
{
    [Fact]
    public void DoesNotAcceptLowerCaseCharacters()
    {
        // Act
        Action action = () => _ = new Alphabet("acgt");
        
        // Assert
        action.ShouldThrow<ArgumentException>();
        
    }
    [Fact]
    public void DnaAlphabetContainsAdenine()
    {
        // Arrange
        Alphabet alphabet = Alphabet.Dna;
        char residueCharacter = 'A';
        
        // Act
        bool result = alphabet.Contains(residueCharacter);
        
        // Assert
        result.ShouldBe(true);
    }
    
    [Fact]
    public void ProteinAlphabetContainsAsparticAcid()
    {
        // Arrange
        Alphabet alphabet = Alphabet.Protein;
        char residueCharacter = 'D';
        
        // Act
        bool result = alphabet.Contains(residueCharacter);
        
        // Assert
        result.ShouldBe(true);
    }

    [Fact]
    public void SameAlphabetsEqual()
    {
        // Arrange
        Alphabet a = new("ABC");
        Alphabet b = new("ABC");
        
        // Act
        bool result = a.Equals(b);
        
        // Assert
        result.ShouldBe(true);
    }
    
    [Fact]
    public void DifferentAlphabetsNotEqual()
    {
        // Arrange
        Alphabet a = new("AB");
        Alphabet b = new("BC");
        
        // Act
        bool result = a.Equals(b);
        
        // Assert
        result.ShouldBe(false);
    }

    [Fact]
    public void RepeatedCharactersIgnoredInEquality()
    {
        // Arrange
        Alphabet a = new("A");
        Alphabet b = new("AA");
        
        // Act
        bool result = a.Equals(b);
        
        // Assert
        result.ShouldBe(true);
    }
    
    [Fact]
    public void CharacterOrderIgnoredInEquality()
    {
        // Arrange
        Alphabet a = new("AB");
        Alphabet b = new("BA");
        
        // Act
        bool result = a.Equals(b);
        
        // Assert
        result.ShouldBe(true);
    }
}