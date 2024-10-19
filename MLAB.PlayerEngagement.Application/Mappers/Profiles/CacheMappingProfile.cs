using AutoMapper;
using MLAB.PlayerEngagement.Application.Commands;
using MLAB.PlayerEngagement.Core.Entities;

namespace MLAB.PlayerEngagement.Application.Mappers.Profiles;

public class CacheMappingProfile : Profile
{
    public CacheMappingProfile()
    {
        CreateMap<Cache, CreateMemoryCacheCommand>().ReverseMap();
    }
}
