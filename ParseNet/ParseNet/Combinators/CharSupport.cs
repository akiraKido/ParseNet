using System.Collections.Generic;
using System.Linq;
using static ParseNet.Functions;

namespace ParseNet.Combinators
{
    public static partial class Combinators
    {
        public static Parser<string> AsString(this Parser<IEnumerable<char>> parser)
        {
            return (source, position) => AsStringImpl(source, position, parser);
        }

        private static ParseResult<string> AsStringImpl(string source, int position, Parser<IEnumerable<char>> parser)
        {
            var result = parser(source, position);

            return result.IsSuccess
                ? Success(source, result.NextPosition, new string(result.Result.ToArray()))
                : Failed<string>(source, result.NextPosition, result.Message);
        }
    }
}