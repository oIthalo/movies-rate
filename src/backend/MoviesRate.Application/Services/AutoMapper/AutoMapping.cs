﻿using AutoMapper;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;
using MoviesRate.Domain.Entities;

namespace MoviesRate.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    public void RequestToDomain()
    {
        CreateMap<RegisterUserRequest, User>();
    }

    public void DomainToResponse()
    {
        CreateMap<User, ShortUserResponse>();
    }
}