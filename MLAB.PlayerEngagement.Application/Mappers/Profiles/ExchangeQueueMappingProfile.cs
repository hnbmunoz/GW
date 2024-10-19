using AutoMapper;
using MLAB.PlayerEngagement.Application.Commands;
using MLAB.PlayerEngagement.Core.Entities;

namespace MLAB.PlayerEngagement.Application.Mappers.Profiles;

public class ExchangeQueueMappingProfile : Profile
{
    public ExchangeQueueMappingProfile()
    {
        CreateMap<ExchangeQueue, CreateQueuePublishCommand>().ReverseMap();
    }
}
