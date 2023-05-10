using AgendaTenis.Core.Identity.Aplicacao.CriarConta;
using AgendaTenis.Core.Identity.Aplicacao.GerarToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTenis.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CriarConta")]
    public async Task<IActionResult> CriarConta(CriarContaCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("GerarToken")]
    public async Task<IActionResult> GerarToken(GerarTokenCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
