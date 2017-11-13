namespace ParseNet.Combinators
{
    public static partial class Combinators
    {
        public static Parser<T> Or<T>(this Parser<T> left, Parser<T> right)
        {
            ParseResult<T> parser(string source, int position)
            {
                var leftResult = left(source, position);

                return leftResult.IsSuccess
                    ? leftResult
                    : right(source, position);
            }

            return parser;
        }
    }
}