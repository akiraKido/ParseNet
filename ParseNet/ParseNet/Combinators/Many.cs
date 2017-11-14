using System.Collections.Immutable;
using static ParseNet.Functions;

namespace ParseNet.Combinators
{
    public static partial class Combinators
    {
        public static Parser<ImmutableList<T>> Many<T>(this Parser<T> parser)
        {
            ParseResult<ImmutableList<T>> ParserImpl(string source, int position, ImmutableList<T> results)
            {
                var result = parser(source, position);

                return result.IsSuccess
                    ? ParserImpl(source, result.NextPosition, results.Add(result.Result))
                    : Success(source, position, results);
            }

            return (source, position) => ParserImpl(source, position, ImmutableList<T>.Empty);
        }

        public static Parser<string> Many(this Parser<string> parser)
        {
            ParseResult<string> ParserImpl(string source, int position, string results)
            {
                var result = parser(source, position);

                return result.IsSuccess
                    ? ParserImpl(source, result.NextPosition, results + result.Result)
                    : Success(source, position, results);
            }

            return (source, position) => ParserImpl(source, position, string.Empty);
        }
    }
}