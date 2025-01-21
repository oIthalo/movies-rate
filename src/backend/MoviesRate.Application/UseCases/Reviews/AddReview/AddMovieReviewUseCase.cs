using AutoMapper;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;
using MoviesRate.Domain.Entities;
using MoviesRate.Domain.Repositories;
using MoviesRate.Domain.Repositories.Reviews;
using MoviesRate.Domain.Services.LoggedUser;
using MoviesRate.Exception.Exceptions;

namespace MoviesRate.Application.UseCases.Reviews.AddReview;

public class AddMovieReviewUseCase : IAddMovieReviewUseCase
{
    private readonly IWriteReviewRepository _writeReviewRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public AddMovieReviewUseCase(
        IWriteReviewRepository writeReviewRepository, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        ILoggedUser loggedUser)
    {
        _writeReviewRepository = writeReviewRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task<ReviewResponse> Execute(int movieId, ReviewRequest request)
    {
        Validate(request);

        var user = await _loggedUser.User();

        var review = _mapper.Map<Review>(request);
        review.UserIdentifier = user!.Identifier;
        review.MovieId = movieId;

        await _writeReviewRepository.AddReview(review);
        await _unitOfWork.Commit();

        return _mapper.Map<ReviewResponse>(review);
    }

    private static void Validate(ReviewRequest request)
    {
        var result = new AddMovieReviewValidator().Validate(request);

        if (!result.IsValid) throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}