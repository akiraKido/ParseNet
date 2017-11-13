namespace ParseNet
{
    public delegate ParseResult<T> Parser<T>(string source, int position);
}