using ParseNet.Combinators;
using Xunit;
using static ParseNet.StringParsers;

namespace ParseNet.Test
{
    public class OrTest
    {
        [Fact]
        public void OrTest1()
        {
            var parser = Literal("hoge").Or(Literal("fuga"));
            
            parser.Parse("hoge").IsSuccess.IsTrue();
            parser.Parse("fuga").IsSuccess.IsTrue();
            parser.Parse("hogefuga").IsSuccess.IsTrue();
            parser.Parse("xxx").IsSuccess.IsFalse();
        }
    }
}