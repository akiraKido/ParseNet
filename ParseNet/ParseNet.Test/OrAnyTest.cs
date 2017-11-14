using ParseNet.Combinators;
using Xunit;
using static ParseNet.StringParsers;

namespace ParseNet.Test
{
    public class OrAnyTest
    {
        private readonly Parser<string> parser = Literal("hoge").OrAny(Literal("fuga"), Literal("moge"));
        
        [Fact]
        public void SimpleOrAnySuccesTest()
        {
            parser.Parse("hoge").IsSuccess.IsTrue();
            parser.Parse("fuga").IsSuccess.IsTrue();
            parser.Parse("moge").IsSuccess.IsTrue();
            parser.Parse("hogefugamoge").IsSuccess.IsTrue();
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