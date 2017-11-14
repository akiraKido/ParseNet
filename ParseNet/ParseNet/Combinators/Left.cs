using static ParseNet.Functions;

namespace ParseNet.Combinators
{

    public static partial class Combinators
    {
        public static Parser<TLeft> Left<TLeft, TRight>(this Parser<TLeft> left, Parser<TRight> right)
        {
            ParseResult<TLeft> parser(string source, int position)
            {
                var leftResult = left(source, position);
                if (leftResult.IsSuccess)
                {
                    var rightResult = right(source, leftResult.NextPosition);
                    return rightResult.IsSuccess
                        ? Success(source, rightResult.NextPosition, leftResult.Result)
                        : Failed<TLeft>(source, position, rightResult.Message);
                }
                return leftResult;
            }

            return parser;
        }
        
    }

}