namespace ParseNet.Combinators
{

    public static partial class Combinators
    {
        public static Parser<T> Right<T>(this Parser<T> left, Parser<T> right)
        {
            ParseResult<T> parser(string source, int position)
            {
                var leftResult = left(source, position);
                if (leftResult.IsSuccess)
                {
                    var rigtResult = right(source, leftResult.NextPosition);
                    return rigtResult;
                }
                return leftResult;
            }

            return parser;
        }
        
    }

}