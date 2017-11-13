using System;
using System.Collections.Immutable;
using static ParseNet.Functions;

namespace ParseNet.Combinators
{
    public static class Combinators
    {
        public static Parser<ImmutableList<T>> Seq<T>(this Parser<T> first, Parser<T> second)
        {
            ParseResult<ImmutableList<T>> parse(string s, int index)
            {
                var firstResult = first(s, index);
                if (firstResult.IsSuccess)
                {
                    var secondResult = second(s, firstResult.NextPosition);

                    return secondResult.IsSuccess
                        ? Success(s, secondResult.NextPosition, ImmutableList<T>.Empty.Add(firstResult.Result).Add(secondResult.Result))
                        : Failed<ImmutableList<T>>(s, secondResult.NextPosition, secondResult.Message);
                }
                else
                {
                    return Failed<ImmutableList<T>>(s, firstResult.NextPosition, firstResult.Message);
                }
            }

            return parse;
        }
        
        public static Parser<ImmutableList<T>> Seq<T>(this Parser<ImmutableList<T>> first, Parser<T> second)
        {
            ParseResult<ImmutableList<T>> parse(string s, int index)
            {
                var firstResult = first(s, index);
                if (firstResult.IsSuccess)
                {
                    var secondResult = second(s, firstResult.NextPosition);

                    return secondResult.IsSuccess
                        ? Success(s, secondResult.NextPosition, firstResult.Result.Add(secondResult.Result))
                        : Failed<ImmutableList<T>>(s, secondResult.NextPosition, secondResult.Message);
                }
                else
                {
                    return Failed<ImmutableList<T>>(s, firstResult.NextPosition, firstResult.Message);
                }
            }

            return parse;
        }

        public static Parser<TResult> Seq<TFirst, TSecond, TResult>(this Parser<TFirst> first, Parser<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            ParseResult<TResult> parse(string s, int index)
            {
                var firstResult = first(s, index);
                if (firstResult.IsSuccess)
                {
                    var secondResult = second(s, firstResult.NextPosition);

                    return secondResult.IsSuccess
                        ? Success(s, secondResult.NextPosition, resultSelector(firstResult.Result, secondResult.Result))
                        : Failed<TResult>(s, secondResult.NextPosition, secondResult.Message);
                }
                else
                {
                    return Failed<TResult>(s, firstResult.NextPosition, firstResult.Message);
                }
            }

            return parse;
        }

        public static Parser<string> Seq(this Parser<string> first, Parser<string> second)
        {
            ParseResult<string> parse(string s, int index)
            {
                var firstResult = first(s, index);
                if (firstResult.IsSuccess)
                {
                    var secondResult = second(s, firstResult.NextPosition);

                    return secondResult.IsSuccess
                        ? Success(s, secondResult.NextPosition, firstResult.Result + secondResult.Result)
                        : Failed<string>(s, secondResult.NextPosition, secondResult.Message);
                }
                else
                {
                    return Failed<string>(s, firstResult.NextPosition, firstResult.Message);
                }
            }

            return parse;
        }
    }
}