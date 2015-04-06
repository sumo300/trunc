using AutoMapper;
using Trunc.Data;
using Trunc.Models;

namespace Trunc.App_Start
{
    public class TypeMappingConfig
    {
        public static void RegisterTypeMaps()
        {
            Mapper.CreateMap<UrlItemModel, UrlItem>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            Mapper.CreateMap<UrlItem, UrlItemViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}