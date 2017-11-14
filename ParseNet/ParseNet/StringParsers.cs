using System.Diagnostics;
using System.Text.RegularExpressions;
using ParseNet.Extensions;
using static ParseNet.Functions;

namespace ParseNet
{
    public static class StringParsers
    {
        public static bool SkipWhiteSpace = false;
        
        public static Parser<string> Literal(string literal, bool? skipWhiteSpace = null)
        {
            ParseResult<string> parser(string source, int position)
            {
                if (skipWhiteSpace == null) skipWhiteSpace = SkipWhiteSpace;
                if (skipWhiteSpace.Value)
                {
                    while (source[position].IsWhiteSpace()) position += 1;
                }
                
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