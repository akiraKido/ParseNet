using ParseNet.Combinators;
using Xunit;
using static ParseNet.StringParsers;

namespace ParseNet.Test
{
    public class LeftTest
    {
        [Fact]
        public void LeftTest1()
        {
            var parser = Literal("hoge").Left(Literal("fuga"));
            
            parser.Parse("hogefuga").IsSuccess.IsTrue();
            parser.Parse("hoge").IsSuccess.IsFalse();
            parser.Parse("hogefuga").Result.Is("hoge");
        }
    }
}