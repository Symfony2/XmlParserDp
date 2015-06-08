using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Infrastructure.Model;

namespace XmlParser.Data.Mapping
{
    public class ParamMap : EntityTypeConfiguration<Param>
    {
        public ParamMap()
        {
            this.ToTable("Param");
            this.HasKey(d => d.Id);
            this.Property(p => p.Name).IsOptional();
            this.Property(p => p.Unit).IsOptional();
            this.Property(p => p.Content).IsOptional();

            this.HasRequired(r => r.OfferNewModel)
                .WithMany(p => p.Params)
                .WillCascadeOnDelete(true);
        }
    }
}