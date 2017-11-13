using System.Diagnostics;
using System.Text.RegularExpressions;
using static ParseNet.Functions;

namespace ParseNet
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
                    return Failed<string>(source, position, "source is too short");
                }

                if (source.Substring(position, literal.Length) == literal)
                {
                    return Success(source, nextPosition, literal);
                }

                return Failed<string>(source, position, $"not matched to {literal}");
            }

            return parser;
        }
    }
}