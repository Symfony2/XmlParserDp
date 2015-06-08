using System;

namespace Infrastructure.Model
{
    public class Picture
    {
        public Guid Id { get; set; }
        public Guid OfferNewModelId { get; set; }
        public OfferNewModel OfferNewModel { get; set; }
        public string Content { get; set; }
    }
}