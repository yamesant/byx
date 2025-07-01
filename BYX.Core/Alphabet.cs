namespace BYX.Core;

public sealed class Alphabet : ValueObject
{
    private const int N = 26;
    private readonly bool[] _included = new bool[N];

    public Alphabet(string characters)
    {
        foreach (char c in characters)
        {
            if (c is < 'A' or > 'Z')
            {
                throw new ArgumentException();
            }
            _included[c - 'A'] = true;
        }
    }
    public static Alphabet Dna => new("ACGT");
    public static Alphabet Protein => new("ACDEFGHIKLMNPQRSTVWY");
    public bool Contains(char c)
    {
        return c is >= 'A' and <= 'Z' && _included[c - 'A'];
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        return _included.Cast<object?>();
    }
}