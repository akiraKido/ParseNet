using ParseNet.Combinators;
using Xunit;
using static ParseNet.StringParsers;

namespace ParseNet.Test
{
    public class LeftRightTest
    {
        [Fact]
        public void LeftTest()
        {
            var hogeFugaLeft = Literal("hoge").Left(Literal("fuga"));
            var result = hogeFugaLeft.Parse("hogefuga");
            result.IsSuccess.IsTrue();
            result.Result.Is("hoge");

            var failed = hogeFugaLeft.Parse("hogexxfuga");
            failed.IsSuccess.IsFalse();
        }

        [Fact]
        public void RightTest()
        {
            var hogeFugaRight = Literal("hoge").Right(Literal("fuga"));
            var result = hogeFugaRight.Parse("hogefuga");
            result.IsSuccess.IsTrue();
            result.Result.Is("fuga");

            var failed = hogeFugaRight.Parse("hogexxfuga");
            failed.IsSuccess.IsFalse();
        }
    }
}