using FluentValidation;
using MoviesRate.Communication.Requests;
using MoviesRate.Domain.ValueObjects;
using MoviesRate.Exception;

namespace MoviesRate.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(MessagesException.NAME_EMPTY);
        RuleFor(x => x.Email).NotEmpty().WithMessage(MessagesException.EMAIL_EMPTY);
        RuleFor(x => x.Password).NotEmpty().WithMessage(MessagesException.PASSWORD_EMPTY);
        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage(MessagesException.EMAIL_INVALID);
        });
        When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
        {
            RuleFor(x => x.Password.Length).GreaterThanOrEqualTo(MoviesRateRuleContracts.MIN_LENGTH_PASSWORD).WithMessage(MessagesException.PASSWORD_SHORT);
        });
    }
}