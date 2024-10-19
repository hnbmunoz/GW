using AutoMapper;
using MLAB.PlayerEngagement.Application.Commands;
using MLAB.PlayerEngagement.Core.Entities;

namespace MLAB.PlayerEngagement.Application.Mappers.Profiles;

public class QueueMappingProfile : Profile
{
    public QueueMappingProfile()
    {
        CreateMap<Queue, CreateQueueCommand>().ReverseMap();
    }
}
