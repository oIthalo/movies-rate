using Microsoft.AspNetCore.Mvc;
using MoviesRate.Application.UseCases.User.Login;
using MoviesRate.Application.UseCases.User.Register;
using MoviesRate.Communication.Requests;
using MoviesRate.Communication.Response;

namespace MoviesRate.API.Controllers;

public class UserController : MoviesRateControllerBase
{
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(ShortUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RegisterUserRequest request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(ShortUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUserUseCase useCase,
        [FromBody] LoginUserRequest request)
    {
        var result = await useCase.Execute(request);
        return Ok(result);
    }
}