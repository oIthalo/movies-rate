﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MoviesRate.Application.Services.AutoMapper;
using MoviesRate.Application.UseCases.Dashboard.GetAll;
using MoviesRate.Application.UseCases.Dashboard.GetPopular;
using MoviesRate.Application.UseCases.Dashboard.GetRecommended;
using MoviesRate.Application.UseCases.Dashboard.GetTopRated;
using MoviesRate.Application.UseCases.Movies.GetMovieById;
using MoviesRate.Application.UseCases.Movies.GetMoviesBySearch;
using MoviesRate.Application.UseCases.Reviews.AddReview;
using MoviesRate.Application.UseCases.User.Delete;
using MoviesRate.Application.UseCases.User.Login;
using MoviesRate.Application.UseCases.User.Register;
using MoviesRate.Application.UseCases.User.Update;

namespace MoviesRate.Application;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

        services.AddScoped<IGetMoviesTopRatedUseCase, GetMoviesTopRatedUseCase>();
        services.AddScoped<IPopularMoviesDashboardUseCase, PopularMoviesDashboardUseCase>();
        services.AddScoped<IGetAllDashboardMoviesUseCase, GetAllMoviesUseCase>();
        services.AddScoped<IGetRandomRecommendedMovieUseCase, GetRandomRecommendedMovieUseCase>();
        services.AddScoped<IGetMovieByIdUseCase, GetMovieByIdUseCase>();

        services.AddScoped<IAddMovieReviewUseCase, AddMovieReviewUseCase>();

        services.AddScoped<IGetMoviesBySearch, GetMoviesBySearch>();
    }

    public static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new MapperConfiguration(autoMapperOptions =>
        {
            autoMapperOptions.AddProfile(new AutoMapping());
        }).CreateMapper());
    }
}