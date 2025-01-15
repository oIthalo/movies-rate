using MoviesRate.Domain.Security.Criptography;
using MoviesRate.Infrastructure.Security.BCryptNet;

namespace CommonTestUtilities.Criptography;

public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build() => new BCryptNet();
}