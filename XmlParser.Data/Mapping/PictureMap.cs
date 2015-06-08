using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Infrastructure.Model;

namespace XmlParser.Data.Mapping
{
    public class PictureMap : EntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
            this.ToTable("Picture");
            this.HasKey(d => d.Id);
            this.Property(p => p.Content).IsOptional();

            this.HasRequired(r => r.OfferNewModel)
                .WithMany(p => p.Pictures)
                .WillCascadeOnDelete(true);
        } 
    }
}