using System;
using static ParseNet.Functions;

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

        public static Parser<T> OrAny<T>(this Parser<T> left, params Parser<T>[] rights)
        {
            if(rights.Length == 0) throw new ArgumentException("right must not be null");
            
            ParseResult<T> parser(string source, int position)
            {
                var leftResult = left(source, position);

                if (leftResult.IsSuccess) return leftResult;

                ParseResult<T> rightResult = default;
                foreach (var right in rights)
                {
                    rightResult = right(source, position);
                    if (rightResult.IsSuccess) return rightResult;
                }
                return rightResult;
            }

            return parser;
            
        }
    }
}