namespace Infrastructure.Model
{
    public class OfferNewModel
    {
        public bool IsAvailable { get; set; }
        public int GroupId { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public int CategoryId { get; set; }
        public int Cpa { get; set; }
        public string CurrencyId { get; set; }
        public bool Delivery { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public bool ManufacturerWarranty { get; set; }
        public string MarketCategory { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public Param ColorParam { get; set; }
    }
}