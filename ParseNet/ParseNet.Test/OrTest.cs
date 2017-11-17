using ParseNet.Combinators;
using Xunit;
using static ParseNet.Parsers.StringParsers;

namespace ParseNet.Test
{
    public class OrTest
    {
        private readonly Parser<string> parser = Literal("hoge").Or(Literal("fuga"));
        
        [Fact]
        public void SimpleOrSuccesTest()
        {
            parser.Parse("hoge").IsSuccess.IsTrue();
            parser.Parse("fuga").IsSuccess.IsTrue();
            parser.Parse("hogefuga").IsSuccess.IsTrue();
        }
        
        [Fact]
        public void SimpleOrFailTest()
        {
            parser.Parse("xxx").IsSuccess.IsFalse();
        }
        
        [Fact]
        public void CheckThatOrResultIsTheFirstResult()
        {
            parser.Parse("hogefuga").Result.Is("hoge");
        }
        
    }
}