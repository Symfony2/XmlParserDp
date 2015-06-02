using System.Collections.Generic;
using Contrib.XmlSerializer;
using Infrastructure.Model;

namespace ParserTests.TestMocks
{
    public class OfferTest
    {
        public IEnumerable<Param> Params { get; set; }
    }

    public class OfferTestOxm : BaseMapper<OfferTest>
    {
        public OfferTestOxm() : base("offer_test")
        {
            SelfElementCollection("param", m => m.Params).Set((m, value) => m.Params = value);
        }

        protected override OfferTest Return()
        {
            return new OfferTest();
        }
    }
}