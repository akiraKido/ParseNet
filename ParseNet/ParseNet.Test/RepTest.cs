using ParseNet.Combinators;
using Xunit;
using static ParseNet.Functions;
using static ParseNet.StringParsers;

namespace ParseNet.Test
{
    public class RepTest
    {
        [Fact]
        public void StringRepTest()
        {
            var hogeParser = Literal("hoge");

            var success = hogeParser.Rep(3).Parse("hogehogehogefuga");
            success.IsSuccess.IsTrue();
            success.Result.Is("hogehogehoge");

            var failed = hogeParser.Rep(3).Parse("hogehogefugahoge");
            failed.IsSuccess.IsFalse();
        }

        [Fact]
        public void ObjectRepTest()
        {
            var hogeParser = Literal("hoge");

            var success = hogeParser.Rep<string>(3).Parse("hogehogehogefuga");
            success.IsSuccess.IsTrue();
            success.Result.Is("hoge", "hoge", "hoge");

            var failed = hogeParser.Rep<string>(3).Parse("hogehogefugahoge");
            failed.IsSuccess.IsFalse();
        }
    }
}