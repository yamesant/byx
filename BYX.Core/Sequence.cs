using System.Text;

namespace BYX.Core;

public sealed class Sequence : ValueObject
{
    public string Characters { get; }
    public Alphabet Alphabet { get; }
    public Sequence(string characters, Alphabet alphabet)
    {
        if (characters.Any(c => !alphabet.Contains(c)))
        {
            throw new ArgumentException();
        }
        Characters = characters;
        Alphabet = alphabet;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        foreach (char c in Characters)
        {
            yield return c;
        }
        yield return Alphabet;
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