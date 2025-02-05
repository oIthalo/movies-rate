﻿using AutoMapper;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;
using MoviesRate.Domain.Dtos;
using MoviesRate.Domain.Entities;

namespace MoviesRate.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DtosMappings();
        DomainToResponse();
    }

    public void RequestToDomain()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<ReviewRequest, Review>();
    }

    public void DomainToResponse()
    {
        CreateMap<Review, ReviewResponse>();
    }

    public void DtosMappings()
    {
        CreateMap<Movie, MovieResponseDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.NoteAverage, opt => opt.MapFrom(src => src.NoteAverage));

        CreateMap<MoviesList, MoviesListResponseDto>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies));
    }
}