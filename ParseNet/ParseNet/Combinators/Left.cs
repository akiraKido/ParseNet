using static ParseNet.Functions;

namespace ParseNet.Combinators
{

    public static partial class Combinators
    {
        public static Parser<T> Left<T>(this Parser<T> left, Parser<T> right)
        {
            ParseResult<T> parser(string source, int position)
            {
                var leftResult = left(source, position);
                if (leftResult.IsSuccess)
                {
                    var rightResult = right(source, leftResult.NextPosition);
                    return rightResult.IsSuccess
                        ? leftResult
                        : rightResult;
                }
                return leftResult;
            }

            return parser;
        }
        
    }

}