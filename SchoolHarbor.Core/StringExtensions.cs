namespace SchoolHarbor.Core;

public static class StringExtensions
{
    internal static string ToFormat(this string format, params object?[] args) //TODO: remove in v5
        => string.Format(format, args);

    public static string ToCamelCase(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return string.Empty;
        }

        var newFirstLetter = char.ToLowerInvariant(s[0]);
        if (newFirstLetter == s[0])
            return s;

        return s.Length <= 256
            ? FastChangeFirstLetter(newFirstLetter, s)
            : newFirstLetter + s.Substring(1);
    }

    public static string ToPascalCase(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return string.Empty;
        }

        var newFirstLetter = char.ToUpperInvariant(s[0]);
        if (newFirstLetter == s[0])
            return s;

        return s.Length <= 256
           ? FastChangeFirstLetter(newFirstLetter, s)
           : newFirstLetter + s.Substring(1);
    }

    private static string FastChangeFirstLetter(char newFirstLetter, string s)
    {
        Span<char> buffer = stackalloc char[s.Length];
        buffer[0] = newFirstLetter;
        s.AsSpan().Slice(1).CopyTo(buffer.Slice(1));
        return buffer.ToString();
    }

    public static string ToConstantCase(this string value) //TODO: rewrite to stackalloc/ string.Create()
    {
        int i;
        int strLength = value.Length;
        for (i = 0; i < strLength - 1; ++i)
        {
            var curChar = value[i];
            var nextChar = value[i + 1];

            if (char.IsLower(curChar) && char.IsUpper(nextChar))
            {
                InsertUnderscore();
                continue;
            }

            if (char.IsDigit(curChar) && char.IsLetter(nextChar))
            {
                InsertUnderscore();
                continue;
            }

            if (char.IsLetter(curChar) && char.IsDigit(nextChar))
            {
                InsertUnderscore();
                continue;
            }
            
            if (i < strLength - 2 && char.IsUpper(curChar) && char.IsUpper(nextChar) && char.IsLower(value[i + 2]))
            {
                InsertUnderscore();
                continue;
            }
        }

        return value.ToUpperInvariant();

        void InsertUnderscore()
        {
            value = value.Substring(0, ++i) + '_' + value.Substring(i);
            ++strLength;
        }
    }

    private static readonly char[] _bangs = new char[] { '!', '[', ']' };
    
    public static string TrimGraphQLTypes(this string name) => name.Trim().Trim(_bangs);
}