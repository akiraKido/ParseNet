using System.Collections.Immutable;
using ParseNet.Combinators;
using Xunit;
using static ParseNet.Functions;
using static ParseNet.Parsers.StringParsers;

namespace ParseNet.Test
{
    public class ManyTest
    {
        [Fact]
        public void StringManyTest()
        {
            var hogeParser = Literal("hoge");

            var threetimes = hogeParser.Many().Parse("hogehogehogefugahoge");
            threetimes.IsSuccess.IsTrue();
            threetimes.Result.Is("hogehogehoge");

            var empty = hogeParser.Many().Parse("xxxhoge");
            empty.IsSuccess.IsTrue();
            empty.Result.Is(string.Empty);
        }

        [Fact]
        public void ObjectManyTest()
        {
            var hogeParser = Literal("hoge");

            var threetimes = hogeParser.Many<string>().Parse("hogehogehogefugahoge");
            threetimes.IsSuccess.IsTrue();
            threetimes.Result.Is("hoge", "hoge", "hoge");

            var empty = hogeParser.Many<string>().Parse("xxxhoge");
            empty.IsSuccess.IsTrue();
            empty.Result.Is(ImmutableList<string>.Empty);
        }
    }
}