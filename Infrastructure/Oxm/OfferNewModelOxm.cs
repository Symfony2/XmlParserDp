using System;
using System.Linq;
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
            Element("description", m => m.Description).Set((m, value) => m.Description = value);
            Element("discount", m => m.Discount).Set((m, value) => m.Discount = value);
            Element("manufacturer_warranty", m => m.ManufacturerWarranty).Set((m, value) => m.ManufacturerWarranty = value);
            Element("market_category", m => m.MarketCategory).Set((m, value) => m.MarketCategory = value);
            Element("model", m => m.Model).Set((m, value) => m.Model = value);
            Element("name", m => m.Name).Set((m, value) => m.Name = value);
            SelfElementCollection("param", m => m.Params).Set((m, value) => m.Params = value.ToList());
            Element("pickup", m => m.Pickup).Set((m, value) => m.Pickup = value);
            SelfElementCollection("picture", m => m.Pictures).Set((m, value) => m.Pictures = value.ToList());
            Element("price", m => m.Price).Set((m, value) => m.Price = value);
            Element("sales_notes", m => m.SalesNotes).Set((m, value) => m.SalesNotes = value);
            Element("store", m => m.Store).Set((m, value) => m.Store = value);
            Element("typePrefix", m => m.TypePrefix).Set((m, value) => m.TypePrefix = value);
            Element("vendor", m => m.Vendor).Set((m, value) => m.Vendor = value);
            Element("vendorCode", m => m.VendorCode).Set((m, value) => m.VendorCode = value);
            Element("url", m => m.Url).Set((m, value) => m.Url = value);
            Element("modified_time", m => m.ModifiedTime).Set((m, value) => m.ModifiedTime = value);
        }

        protected override OfferNewModel Return()
        {
            return new OfferNewModel();
        }
    }
}