using System.Collections.Generic;

namespace Infrastructure.Model
{
    public class OfferOldModel
    {
        public bool IsAvailable { get; set; }
        public int OriginalId { get; set; }//todo is unique property only old has
        public string Type { get; set; }
        public string Id { get; set; }
        public bool Delivery { get; set; }
        public string Description { get; set; }
        public bool ManufacturerWarranty { get; set; }
        public string Model { get; set; }
        public IEnumerable<PictureOrig> Pictures { get; set; }
        public string SalesNotes { get; set; }
        public string TypePrefix { get; set; }
        public string VendorCode { get; set; }
        public IEnumerable<Param> Params { get; set; }
        public int CategoryId { get; set; }
        public string Vendor { get; set; }
        public decimal Oldprice { get; set; } //todo is unique property only old has
        public decimal Price { get; set; }
        public int AdvcampaignId { get; set; } //todo is unique property only old has
        public string AdvcampaignName { get; set; } //todo is unique property only old has
        public string ModifiedTime { get; set; }
        public string Picture { get; set; } //todo is unique property only old has
        public string Thumbnail { get; set; } //todo is unique property only old has
        public string Url { get; set; }
        public string CurrencyId { get; set; }
        public string Name { get; set; }
    }
}