using AutoMapper;
using MoviesRate.Application.Services.AutoMapper;

namespace CommonTestUtilities.Mapper;

public class AutoMapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

        return mapper;
    }
}