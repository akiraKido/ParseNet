using System.Collections.Immutable;
using static ParseNet.Functions;

namespace ParseNet.Combinators
{
    public static partial class Combinators
    {
        public static Parser<ImmutableList<T>> Rep<T>(this Parser<T> parser, int count)
        {
            ParseResult<ImmutableList<T>> ParseInner(string source, int position, int c, ImmutableList<T> results)
            {
                if (c == 0)
                {
                    return Success(source, position, results);
                }

                var result = parser(source, position);
                if (!result.IsSuccess)
                {
                    return Failed<ImmutableList<T>>(source, position, result.Message);
                }

                return ParseInner(source, result.NextPosition, c - 1, results.Add(result.Result));
            }

            return (source, position) => ParseInner(source, position, count, ImmutableList<T>.Empty);
        }

        public static Parser<string> Rep(this Parser<string> parser, int count)
        {
            ParseResult<string> ParseInner(string source, int position, int c, string result)
            {
                if (c == 0)
                {
                    return Success(source, position, result);
                }

                var r = parser(source, position);
                if (!r.IsSuccess)
                {
                    return Failed<string>(source, position, r.Message);
                }

                return ParseInner(source, r.NextPosition, c - 1, result + r.Result);
            }

            return (source, position) => ParseInner(source, position, count, string.Empty);
        }
    }
}