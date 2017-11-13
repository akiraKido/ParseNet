using System;

namespace ParseNet
{
    public struct ParseResult<T>
    {
        public string Source { get; }
        
        public int NextPosition { get; }
        
        public bool IsSuccess { get; }

        public string Message => _message;

        public T Result => this.IsSuccess
            ? _result
            : throw new ParseFailedException(_message);
        private readonly T _result;
        private readonly string _message;

        private ParseResult(string source, int nextPosition, bool isSuccess, T result, string message)
        {
            this.Source = source;
            this.NextPosition = nextPosition;
            this.IsSuccess = isSuccess;
            _result = result;
            _message = message;
        }

        public static ParseResult<T> CreateSuccess(string source, int nextPosition, T result)
        {
            return new ParseResult<T>(source, nextPosition, true, result, null);
        }

        public static ParseResult<T> CreateFailed(string source, int nextPosition, string message)
        {
            return new ParseResult<T>(source, nextPosition, false, default, message);
        }
    }

    public class ParseFailedException : Exception
    {
        public ParseFailedException(string message)
            : base(message)
        {
        }
    }
}