using AutoMapper;
using MLAB.PlayerEngagement.Application.Mappers.Profiles;

namespace MLAB.PlayerEngagement.Application.Mappers;

public class QueueMapper
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() => {
        var config = new MapperConfiguration(cfg => {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<QueueMappingProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });
    public static IMapper Mapper => Lazy.Value;
}
