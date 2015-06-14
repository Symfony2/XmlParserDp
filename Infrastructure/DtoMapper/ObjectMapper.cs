using System.Linq;
using AutoMapper;
using Infrastructure.Model;

namespace Infrastructure.DtoMapper
{
    public class ObjectMapper : IObjectMapper
    {
        public ObjectMapper()
        {
            XmlMappingConfiguration();
        }
        private void XmlMappingConfiguration()
        {
            
            Mapper.CreateMap<Picture, PictureOrig>()
                .ForMember(d => d.Content, opt => opt.MapFrom(src => src.Content));

            Mapper.CreateMap<OfferNewModel, OfferOldModel>()
                .ForMember(d => d.AdvcampaignId, opt => opt.Ignore())
                .ForMember(d => d.AdvcampaignName, opt => opt.Ignore())
                .ForMember(d => d.Picture, opt => opt.Ignore())
                .ForMember(d => d.Thumbnail, opt => opt.Ignore())

                .ForMember(d => d.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                .ForMember(d => d.GroupId, opt => opt.MapFrom(src => src.GroupId))
                .ForMember(d => d.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(d => d.OriginalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))

                .ForMember(d => d.Delivery, opt => opt.MapFrom(src => src.Delivery))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(d => d.ManufacturerWarranty, opt => opt.MapFrom(src => src.ManufacturerWarranty))
                .ForMember(d => d.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(d => d.Pictures, opt => opt.MapFrom(src => src.Pictures))
                .ForMember(d => d.SalesNotes, opt => opt.MapFrom(src => src.SalesNotes))
                .ForMember(d => d.TypePrefix, opt => opt.MapFrom(src => src.TypePrefix))

                .ForMember(d => d.VendorCode, opt => opt.MapFrom(src => src.VendorCode))
                .ForMember(d => d.Params, opt => opt.MapFrom(src => src.Params))
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(d => d.Vendor, opt => opt.MapFrom(src => src.Vendor))

                .ForMember(d => d.Oldprice, opt => opt.MapFrom(src => src.Price))
                .ForMember(d => d.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(d => d.ModifiedTime, opt => opt.MapFrom(src => src.ModifiedTime))
                
                .ForMember(d => d.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(d => d.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .AfterMap((nm, om) =>
                {
                    om.Thumbnail = nm.Pictures.Any() ? nm.Pictures.First().Content : " ";
                });
        }


        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}