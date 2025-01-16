using FluentValidation;
using MoviesRate.Communication.Requests;
using MoviesRate.Domain.ValueObjects;
using MoviesRate.Exception;

namespace MoviesRate.Application.UseCases.User.Login;

public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(MessagesException.EMAIL_EMPTY);
        RuleFor(x => x.Password).NotEmpty().WithMessage(MessagesException.EMAIL_EMPTY);
        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage(MessagesException.EMAIL_EMPTY);
        });
        When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
        {
            RuleFor(x => x.Password.Length).GreaterThanOrEqualTo(MoviesRateRuleContracts.MIN_LENGTH_PASSWORD).WithMessage(MessagesException.PASSWORD_SHORT);
        });
    }
}