using Contrib.XmlSerializer;
using Infrastructure.Model;

namespace ParserTests.TestMocks
{
    public class OfferTest
    {
        public Param ColorParam { get; set; }
        public Param CollectionParam { get; set; }
        public Param SeasonParam { get; set; }
        public Param ManufacturerParam { get; set; }
        public Param SizeParam { get; set; }
        public Param GenderParam { get; set; }
        public Param AgeParam { get; set; }
    }

    public class OfferTestOxm : BaseMapper<OfferTest>
    {
        public OfferTestOxm() : base("offer_test")
        {
            CustomElementMapper("param", m => m.ColorParam).Set((m, value) => m.ColorParam = value);
            CustomElementMapper("param", m => m.CollectionParam).Set((m, value) => m.CollectionParam = value);
            CustomElementMapper("param", m => m.SeasonParam).Set((m, value) => m.SeasonParam = value);
            CustomElementMapper("param", m => m.ManufacturerParam).Set((m, value) => m.ManufacturerParam = value);
            CustomElementMapper("param", m => m.SizeParam).Set((m, value) => m.SizeParam = value);
            CustomElementMapper("param", m => m.GenderParam).Set((m, value) => m.GenderParam = value);
            CustomElementMapper("param", m => m.AgeParam).Set((m, value) => m.AgeParam = value);
        }

        protected override OfferTest Return()
        {
            return new OfferTest();
        }
    }
}