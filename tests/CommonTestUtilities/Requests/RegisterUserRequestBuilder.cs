using Bogus;
using MoviesRate.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RegisterUserRequestBuilder
{
    public static RegisterUserRequest Build(int passwordLength = 8) =>
        new Faker<RegisterUserRequest>()
            .RuleFor(x => x.Name, (f) => (f.Person.FirstName))
            .RuleFor(x => x.Email, (f, u) => (f.Internet.Email(u.Name)))
            .RuleFor(x => x.Password, (f, u) => (f.Internet.Password(passwordLength)));
}