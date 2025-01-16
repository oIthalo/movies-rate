using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.User.Register;

public class RegisterUserTest : MoviesRateClassFixture
{
    public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Success()
    {
        var request = RegisterUserRequestBuilder.Build();
        var response = await DoPost("v1/user/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}