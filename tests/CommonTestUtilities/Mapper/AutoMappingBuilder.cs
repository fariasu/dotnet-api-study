using AutoMapper;
using TaskManager.Application.AutoMapper;

namespace CommonTestUtilities.Mapper;

public class AutoMappingBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        });
        
        return mapper.CreateMapper();
    }
}