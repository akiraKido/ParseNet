using System.Diagnostics;
using System.Text.RegularExpressions;
using ParseNet.Extensions;
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

                if (source.Length < nextPosition) return Failed<string>(source, position, "source is too short");
                
                return source.Matches(position, literal) 
                    ? Success(source, nextPosition, literal) 
                    : Failed<string>(source, position, $"not matched to {literal}");
            }

            return parser;
        }
    }
}