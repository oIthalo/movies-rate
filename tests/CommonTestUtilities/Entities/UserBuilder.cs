using Bogus;
using CommonTestUtilities.Criptography;
using MoviesRate.Domain.Entities;

namespace CommonTestUtilities.Entities;

public class UserBuilder
{
    public static (User user, string password) Build(int passwordLength = 8)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var password = new Faker().Internet.Password(passwordLength);

        var user = new Faker<User>()
            .RuleFor(x => x.Id, (f) => 1)
            .RuleFor(x => x.Active, (f) => true)
            .RuleFor(x => x.CreatedOn, (f) => DateTime.UtcNow)
            .RuleFor(x => x.Name, (f) => (f.Person.FirstName))
            .RuleFor(x => x.Email, (f, u) => (f.Internet.Email(u.Name)))
            .RuleFor(x => x.Password, (f) => passwordEncripter.Encrypt(password))
            .RuleFor(x => x.Identifier, (f) => Guid.NewGuid());

        return (user, password);
    }
}