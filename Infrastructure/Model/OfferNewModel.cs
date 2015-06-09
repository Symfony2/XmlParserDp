using System;
using System.Collections.Generic;

namespace Infrastructure.Model
{
    public class OfferNewModel
    {
        public bool IsAvailable { get; set; }
        public int GroupId { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public int Cpa { get; set; }
        public string CurrencyId { get; set; }
        public bool Delivery { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public bool ManufacturerWarranty { get; set; }
        public string MarketCategory { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public IEnumerable<Param> Params { get; set; }
        public bool Pickup { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
        public int Price { get; set; }
        public string SalesNotes { get; set; }
        public bool Store { get; set; }
        public string TypePrefix { get; set; }
        public string Vendor { get; set; }
        public string VendorCode { get; set; }
        public string Url { get; set; }
        public string ModifiedTime { get; set; }
    }
}