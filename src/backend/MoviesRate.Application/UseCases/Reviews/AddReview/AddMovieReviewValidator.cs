using FluentValidation;
using MoviesRate.Communication.Requests;
using MoviesRate.Exception;

namespace MoviesRate.Application.UseCases.Reviews.AddReview;

public class AddMovieReviewValidator : AbstractValidator<ReviewRequest>
{
    public AddMovieReviewValidator()
    {
        RuleFor(x => x.Comments).NotEmpty().WithMessage(MessagesException.COMMENTS_EMPTY);
        RuleFor(x => x.Comments.Length).LessThanOrEqualTo(500).WithMessage(MessagesException.COMMENTS_INVALID);
        When(x => !string.IsNullOrWhiteSpace(x.Comments), () =>
        {
            RuleFor(x => x.Comments.Length).LessThanOrEqualTo(500).WithMessage(MessagesException.COMMENTS_INVALID);
        });
    }
}