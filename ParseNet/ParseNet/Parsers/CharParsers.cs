using System;
using static ParseNet.Functions;

namespace ParseNet.Parsers
{
    public static class CharParsers
    {
        public static Parser<char> Literal(char c)
        {
            return Is(ch => ch == c);
        }

        public static Parser<char> Digit()
        {
            return Is(char.IsDigit);
        }

        public static Parser<char> Any()
        {
            return Is(c => true);
        }

        public static Parser<char> Is(Func<char, bool> func)
        {
            return (source, position) => IsImpl(source, position, func);
        }

        private static ParseResult<char> IsImpl(string source, int position, Func<char, bool> func)
        {
            if (source.Length <= position)
            {
                return EndOfSource<char>(source, position);
            }

            var c = source[position];

            return func(c)
                ? Success(source, position + 1, c)
                : Failed<char>(source, position, "not matched");
        }
    }
}