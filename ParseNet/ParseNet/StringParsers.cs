using System.Collections.Concurrent;
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

        public static Parser<string> Regex(string regexString, bool? skipWhiteSpace = null)
        {
            ParseResult<string> parser(string source, int position)
            {
                Regex regex = RegexCache.GetOrAdd(regexString, (key) =>
                {
                    if (!regexString.StartsWith(@"\G"))
                    {
                        if (regexString.StartsWith(@"\g")) regexString = regexString.Substring(2);
                        regexString = $@"\G{regexString}";
                    }
                    return new Regex(regexString);
                });

                if (skipWhiteSpace == null) skipWhiteSpace = SkipWhiteSpace;
                if (skipWhiteSpace.Value)
                {
                    while (source[position].IsWhiteSpace()) position += 1;
                }

                Match match = regex.Match(source, position);

                if (match.Success)
                {
                    int nextPosition = position + match.Length;
                    return Success(source, nextPosition, match.Value);
                }
                return Failed<string>(source, position, $"not matched to {regex}");
            }

            return parser;
        }

        private static readonly ConcurrentDictionary<string, Regex> RegexCache 
            = new ConcurrentDictionary<string, Regex>();
    }
}