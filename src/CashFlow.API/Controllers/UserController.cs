using CashFlow.Application.UseCases.User.Register;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.User;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] ResponseRegisteredUserJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}