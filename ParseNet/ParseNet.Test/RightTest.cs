using ParseNet.Combinators;
using Xunit;
using static ParseNet.StringParsers;

namespace ParseNet.Test
{
    public class RightTest
    {
        [Fact]
        public void RightTest1()
        {
            var parser = Literal("hoge").Right(Literal("fuga"));
            
            parser.Parse("hogefuga").IsSuccess.IsTrue();
            parser.Parse("hoge").IsSuccess.IsFalse();
            parser.Parse("hogefuga").Result.Is("fuga");
        }
    }
}