using AutoMapper;
using Trunc.Data;
using Trunc.Models;

namespace Trunc.App_Start {
    public class TypeMappingConfig {
        public static void RegisterTypeMaps() {
            Mapper.CreateMap<UrlItemModel, UrlItem>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.ExpireMode, opt => opt.MapFrom(src => (short) src.ExpireMode));

            Mapper.CreateMap<UrlItem, UrlItemViewModel>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(dest => dest.ExpireMode, opt => opt.MapFrom(src => (ExpireMode) src.ExpireMode))
                .ForMember(dest => dest.TouchedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UrlItemId, opt => opt.Ignore())
                .ForMember(dest => dest.HitCount, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}