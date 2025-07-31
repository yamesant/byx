using System.Text;

namespace BYX.Core;

public sealed class Sequence : ValueObject
{
    public string Characters { get; }
    public Alphabet Alphabet { get; }
    public string Name { get; }
    public const string DefaultName = "unnamed";
    public string? Description { get; }
    public Sequence(string characters, Alphabet alphabet, string name = DefaultName, string? description = null)
    {
        if (characters.Any(c => !alphabet.Contains(c)))
        {
            throw new ArgumentException();
        }

        name = name.Length == 0 ? DefaultName : name;
        if (name.Contains(' '))
        {
            throw new ArgumentException();
        }

        Characters = characters;
        Alphabet = alphabet;
        Name = name;
        Description = description;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        foreach (char c in Characters)
        {
            yield return c;
        }
        yield return Alphabet;
        yield return Name;
        yield return Description;
    }

    public string ToFormattedString(int lineWidth)
    {
        StringBuilder result = new();
        for (int i = 0; i < Characters.Length; i++)
        {
            if (i > 0 && i % lineWidth == 0)
            {
                result.Append('\n');
            }
            result.Append(Characters[i]);
        }
        return result.ToString();
    }
}