using CommonTestUtilities.Requests;
using FluentAssertions;
using MoviesRate.Application.UseCases.User.Login;
using MoviesRate.Application.UseCases.User.Register;
using MoviesRate.Exception;

namespace Validators.Test.User.Login;

public class LoginUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = LoginUserRequestBuilder.Build();
        var result = new LoginUserValidator().Validate(request);

        result.Errors.Should().HaveCount(0);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var request = LoginUserRequestBuilder.Build();
        request.Email = string.Empty;

        var result = new LoginUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var request = LoginUserRequestBuilder.Build();
        request.Email = "invalidemail.com";

        var result = new LoginUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.EMAIL_INVALID));
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var request = LoginUserRequestBuilder.Build();
        request.Password = string.Empty;

        var result = new LoginUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.PASSWORD_EMPTY));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void Error_Short_Password(int passwordLength)
    {
        var request = LoginUserRequestBuilder.Build(passwordLength);

        var result = new LoginUserValidator().Validate(request);

        result.Errors.Should().HaveCount(1);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.PASSWORD_SHORT));
    }
}