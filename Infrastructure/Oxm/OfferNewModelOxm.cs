using Contrib.XmlSerializer;
using Infrastructure.Model;

namespace Infrastructure.Oxm
{
    public class OfferNewModelOxm : BaseMapper<OfferNewModel>
    {
        public OfferNewModelOxm()
            : base("offer")
        {
            Root().Attribute("available", m => m.IsAvailable).Set((m, value) => m.IsAvailable = value);
            Root().Attribute("group_id", m => m.GroupId).Set((m, value) => m.GroupId = value);
            Root().Attribute("type", m => m.Type).Set((m, value) => m.Type = value);
            Root().Attribute("id", m => m.Id).Set((m, value) => m.Id = value);

            Element("categoryId", m => m.CategoryId).Set((m, value) => m.CategoryId = value);
            Element("cpa", m => m.Cpa).Set((m, value) => m.Cpa = value);
            Element("currencyId", m => m.CurrencyId).Set((m, value) => m.CurrencyId = value);
            Element("delivery", m => m.Delivery).Set((m, value) => m.Delivery = value);
            Element("delivery", m => m.Delivery).Set((m, value) => m.Delivery = value);
            Element("description", m => m.Description).Set((m, value) => m.Description = value);
            Element("discount", m => m.Discount).Set((m, value) => m.Discount = value);
            Element("manufacturer_warranty", m => m.ManufacturerWarranty).Set((m, value) => m.ManufacturerWarranty = value);
            Element("market_category", m => m.MarketCategory).Set((m, value) => m.MarketCategory = value);
            Element("model", m => m.Model).Set((m, value) => m.Model = value);
            Element("name", m => m.Name).Set((m, value) => m.Name = value);

            CustomElementMapper("param", m => m.ColorParam).Set((m, value) => m.ColorParam = value);
            
        }

        protected override OfferNewModel Return()
        {
            throw new System.NotImplementedException();
        }
    }
}