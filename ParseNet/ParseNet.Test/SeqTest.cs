using System.Collections.Immutable;
using ParseNet.Combinators;
using Xunit;
using static ParseNet.Functions;
using static ParseNet.Parsers.StringParsers;

namespace ParseNet.Test
{
    public class SeqTest
    {
        [Fact]
        public void StringSeqTest()
        {
            Parser<string> hogeFugaParser = Literal("hoge").Seq(Literal("fuga"));
            
            hogeFugaParser("hogefuga", 0).IsSuccess.IsTrue();
            hogeFugaParser("hogefuga", 0).Result.Is("hogefuga");
            
            hogeFugaParser("hoge", 0).IsSuccess.IsFalse();
            hogeFugaParser("fugahoge", 0).IsSuccess.IsFalse();
        }

        [Fact]
        public void ObjectSeqTest()
        {
            Parser<Hoge> hogeParser = (source, position) =>
            {
                var result = Literal("hoge")(source, position);
                return result.IsSuccess
                    ? Success(source, result.NextPosition, new Hoge())
                    : Failed<Hoge>(source, result.NextPosition, result.Message);
            };
            
            Parser<Fuga> fugaParser = (source, position) =>
            {
                var result = Literal("fuga")(source, position);
                return result.IsSuccess
                    ? Success(source, result.NextPosition, new Fuga())
                    : Failed<Fuga>(source, result.NextPosition, result.Message);
            };

            Parser<string[]> hogeFugaParser =
                hogeParser.Seq(fugaParser, (hoge, fuga) => new string[] {hoge.Name, fuga.Name});

            hogeFugaParser("hogefuga", 0).IsSuccess.IsTrue();
            hogeFugaParser("hogefuga", 0).Result.Is("hoge", "fuga");

            Parser<ImmutableList<Hoge>> hogehogeParser = hogeParser.Seq(hogeParser);
            hogehogeParser("hogehoge", 0).IsSuccess.IsTrue();
            hogehogeParser("hogehoge", 0).Result.Is(new Hoge(), new Hoge());
        }

        private class Hoge
        {
            public string Name => "hoge";

            public override bool Equals(object obj)
                => obj is Hoge;
        }

        private class Fuga
        {
            public string Name => "fuga";

            public override bool Equals(object obj)
                => obj is Fuga;
        }
    }
}