using Bogus;
using MoviesRate.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class LoginUserRequestBuilder
{
    public static LoginUserRequest Build(int passwordLength = 8) =>
        new Faker<LoginUserRequest>()
            .RuleFor(x => x.Email, (f) => (f.Internet.Email()))
            .RuleFor(x => x.Password, (f) => (f.Internet.Password(passwordLength)));
}