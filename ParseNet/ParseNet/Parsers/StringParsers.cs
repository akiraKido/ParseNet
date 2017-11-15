using static ParseNet.Functions;

namespace ParseNet.Parsers
{
    public static class StringParsers
    {
        public static Parser<string> Literal(string literal)
        {
            ParseResult<string> parser(string source, int position)
            {
                int nextPosition = position + literal.Length;

                if (source.Length < nextPosition)
                {
                    return EndOfSource<string>(source, position);
                }

                if (string.Compare(source, position, literal, 0, literal.Length) == 0)
                {
                    return Success(source, nextPosition, literal);
                }

                return Failed<string>(source, position, $"not matched to {literal}");
            }

            return parser;
        }
    }
}