using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Infrastructure.Model;

namespace XmlParser.Data.Mapping
{
    public class OfferNewModelMap : EntityTypeConfiguration<OfferNewModel>
    {
        public OfferNewModelMap()
        {
            this.ToTable("OfferNewModel");
            this.HasKey(d => d.OfferId);
            this.Property(p => p.Id).IsOptional();
            this.Property(p => p.IsAvailable).IsRequired();
            this.Property(p => p.GroupId).IsOptional();
            this.Property(p => p.Type).IsOptional();
            this.Property(p => p.Id).IsOptional();
            this.Property(p => p.CategoryId).IsOptional();
            this.Property(p => p.Cpa).IsOptional();
            this.Property(p => p.CurrencyId).IsOptional();
            this.Property(p => p.Delivery).IsRequired();
            this.Property(p => p.Description).IsOptional();
            this.Property(p => p.Discount).IsOptional();
            this.Property(p => p.ManufacturerWarranty).IsRequired();
            this.Property(p => p.MarketCategory).IsOptional();
            this.Property(p => p.Model).IsOptional();
            this.Property(p => p.Name).IsOptional();
            this.Property(p => p.Pickup).IsRequired();
            this.Property(p => p.Price).IsOptional();
            this.Property(p => p.SalesNotes).IsOptional();
            this.Property(p => p.Store).IsRequired();
            this.Property(p => p.TypePrefix).IsOptional();
            this.Property(p => p.Vendor).IsOptional();
            this.Property(p => p.VendorCode).IsOptional();
            this.Property(p => p.Url).IsOptional();
            this.Property(p => p.ModifiedTime).IsOptional();
            
            this.HasMany(p=>p.Params)
                .WithRequired(p=>p.OfferNewModel)
                .HasForeignKey(k=>k.OfferNewModelId)
                .WillCascadeOnDelete(true);
            
            this.HasMany(p=>p.Pictures)
                .WithRequired(p => p.OfferNewModel)
                .HasForeignKey(k=>k.OfferNewModelId)
                .WillCascadeOnDelete(true);
        }
    }
}