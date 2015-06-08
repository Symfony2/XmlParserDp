using Contrib.XmlSerializer;
using Infrastructure.Model;

namespace Infrastructure.Oxm
{
    public class OfferOldModelOxm : BaseMapper<OfferOldModel>
    {
        public OfferOldModelOxm()
            : base("offer")
        {
            Root().Attribute("available", m => m.IsAvailable).Set((m, value) => m.IsAvailable = value);
            Root().Attribute("group_id", m => m.GroupId).Set((m, value) => m.GroupId = value).Default(null);
            Root().Attribute("original_id", m => m.OriginalId).Set((m, value) => m.OriginalId = value).Default(null);
            Root().Attribute("type", m => m.Type).Set((m, value) => m.Type = value);
            Root().Attribute("id", m => m.Id).Set((m, value) => m.Id = value).Default(null);

            Element("delivery", m => m.Delivery).Set((m, value) => m.Delivery = value);
            Element("description", m => m.Description).Set((m, value) => m.Description = value);
            Element("manufacturer_warranty", m => m.ManufacturerWarranty).Set((m, value) => m.ManufacturerWarranty = value);
            Element("model", m => m.Model).Set((m, value) => m.Model = value);
            SelfElementCollection("picture_orig", m => m.Pictures).Set((m, value) => m.Pictures = value);
            Element("sales_notes", m => m.SalesNotes).Set((m, value) => m.SalesNotes = value);
            Element("typePrefix", m => m.TypePrefix).Set((m, value) => m.TypePrefix = value);
            Element("vendorCode", m => m.VendorCode).Set((m, value) => m.VendorCode = value);
            SelfElementCollection("param", m => m.Params).Set((m, value) => m.Params = value);
            Element("categoryId", m => m.CategoryId).Set((m, value) => m.CategoryId = value);
            Element("vendor", m => m.Vendor).Set((m, value) => m.Vendor = value);
            Element("oldprice", m => m.Oldprice).Set((m, value) => m.Oldprice = value);
            Element("price", m => m.Price).Set((m, value) => m.Price = value);
            Element("advcampaign_id", m => m.AdvcampaignId).Set((m, value) => m.AdvcampaignId = value);
            Element("advcampaign_name", m => m.AdvcampaignName).Set((m, value) => m.AdvcampaignName = value);
            Element("modified_time", m => m.ModifiedTime).Set((m, value) => m.ModifiedTime = value);
            Element("picture", m => m.Picture).Set((m, value) => m.Picture = value);
            Element("thumbnail", m => m.Thumbnail).Set((m, value) => m.Thumbnail = value);
            Element("url", m => m.Url).Set((m, value) => m.Url = value);
            Element("currencyId", m => m.CurrencyId).Set((m, value) => m.CurrencyId = value);
            Element("name", m => m.Name).Set((m, value) => m.Name = value);

//            Element("cpa", m => m.Cpa).Set((m, value) => m.Cpa = value);
//            Element("discount", m => m.Discount).Set((m, value) => m.Discount = value);
//            Element("market_category", m => m.MarketCategory).Set((m, value) => m.MarketCategory = value);
//            Element("pickup", m => m.Pickup).Set((m, value) => m.Pickup = value);
//            Element("store", m => m.Store).Set((m, value) => m.Store = value);
        }

        protected override OfferOldModel Return()
        {
            return new OfferOldModel();
        }
    }
}