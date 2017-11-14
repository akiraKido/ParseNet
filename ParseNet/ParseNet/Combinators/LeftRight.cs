using static ParseNet.Functions;

namespace ParseNet.Combinators
{
    public static partial class Combinators
    {
        public static Parser<TLeft> Left<TLeft, TRight>(this Parser<TLeft> left, Parser<TRight> right)
        {
            ParseResult<TLeft> LeftImpl(string source, int position)
            {
                var leftResult = left(source, position);
                if (leftResult.IsSuccess)
                {
                    var rightResult = right(source, leftResult.NextPosition);

                    return rightResult.IsSuccess
                        ? Success(source, rightResult.NextPosition, leftResult.Result)
                        : Failed<TLeft>(source, position, rightResult.Message);
                }

                return Failed<TLeft>(source, position, leftResult.Message);
            }

            return LeftImpl;
        }

        public static Parser<TRight> Right<TLeft, TRight>(this Parser<TLeft> left, Parser<TRight> right)
        {
            ParseResult<TRight> RightImpl(string source, int position)
            {
                var leftResult = left(source, position);
                if (leftResult.IsSuccess)
                {
                    var rightResult = right(source, leftResult.NextPosition);

                    return rightResult.IsSuccess
                        ? rightResult
                        : Failed<TRight>(source, position, rightResult.Message);
                }

                return Failed<TRight>(source, position, leftResult.Message);
            }

            return RightImpl;
        }
    }
}