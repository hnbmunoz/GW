using AutoMapper;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Response;

namespace MLAB.PlayerEngagement.Application.Mappers.Profiles;

public class CurrencyMappingProfile : Profile
{
    public CurrencyMappingProfile()
    {
        CreateMap<AllCurrencyResponse, CurrencyModel > ()
            .ForMember(model => model.CurrencyId, response => response.MapFrom(ol => ol.Id))
            .ForMember(model => model.CurrencyCode, response => response.MapFrom(ol => ol.Name))
            .ForMember(model => model.Status, response => response.MapFrom(ol => ol.Status))
            .ForMember(model => model.CreatedBy, model => model.Ignore())
            .ForMember(model => model.CreatedDate, model => model.Ignore())
            .ForMember(model => model.UpdatedBy, model => model.Ignore())
            .ForMember(model => model.UpdatedDate, model => model.Ignore())
            .ReverseMap();
    }
}
