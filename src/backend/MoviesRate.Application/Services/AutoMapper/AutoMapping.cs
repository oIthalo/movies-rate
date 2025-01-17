﻿using AutoMapper;
using MoviesRate.Communication.Requests;
using MoviesRate.Domain.Dtos;
using MoviesRate.Domain.Entities;

namespace MoviesRate.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DtosMappings();
    }

    public void RequestToDomain()
    {
        CreateMap<RegisterUserRequest, User>();
    }

    public void DtosMappings()
    {
        CreateMap<Movie, MovieResponseDto>();

        CreateMap<MoviesList, MoviesListResponseDto>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies));
    }
}