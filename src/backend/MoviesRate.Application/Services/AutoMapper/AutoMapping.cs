using AutoMapper;
using MoviesRate.Communication.Requests;
using MoviesRate.Domain.Entities;

namespace MoviesRate.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
    }

    public void RequestToDomain()
    {
        CreateMap<RegisterUserRequest, User>();
    }
}