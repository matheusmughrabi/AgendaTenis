using AgendaTenis.Core.Identity.Aplicacao.CriarConta;
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
    public async Task<IActionResult> CriarConta()
    {
        var response = await _mediator.Send(new CriarContaCommand());
        return Ok();
    }

    [HttpPost("GerarToken")]
    public async Task<IActionResult> GerarToken()
    {
        return Ok();
    }
}
