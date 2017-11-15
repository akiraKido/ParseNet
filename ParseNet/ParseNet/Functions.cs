namespace ParseNet
{
    public static class Functions
    {
        public static ParseResult<T> Success<T>(string source, int nextPosition, T result)
            => ParseResult<T>.CreateSuccess(source, nextPosition, result);

        public static ParseResult<T> Failed<T>(string source, int nextPosition, string message)
            => ParseResult<T>.CreateFailed(source, nextPosition, message);

        public static ParseResult<T> Parse<T>(this Parser<T> parser, string source)
            => parser(source, 0);

        public static ParseResult<T> EndOfSource<T>(string source, int nextPosition)
            => Failed<T>(source, nextPosition, "end of source");
    }
}