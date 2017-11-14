using static ParseNet.Functions;

namespace ParseNet.Combinators
{

    public static partial class Combinators
    {
        public static Parser<TRight> Right<TLeft, TRight>(this Parser<TLeft> left, Parser<TRight> right)
        {
            ParseResult<TRight> parser(string source, int position)
            {
                var leftResult = left(source, position);
                if (leftResult.IsSuccess)
                {
                    var rigtResult = right(source, leftResult.NextPosition);
                    return rigtResult;
                }
                return Failed<TRight>(source, position, leftResult.Message);
            }

            return parser;
        }
        
    }

}