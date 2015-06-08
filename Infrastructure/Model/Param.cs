using System;

namespace Infrastructure.Model
{
    public class Param
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Content { get; set; }
        public Guid OfferNewModelId { get; set; }
        public OfferNewModel OfferNewModel { get; set; }
    }
}