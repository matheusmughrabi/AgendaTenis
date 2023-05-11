using AgendaTenis.Core.Partidas.Aplicacao.ConvidarParaPartida;
using AgendaTenis.Core.Partidas.Aplicacao.RegistrarPlacar;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTenis.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class PartidasController : ControllerBase
{
    private readonly IMediator _mediator;

    public PartidasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Convidar")]
    public async Task<IActionResult> ConvidarParaPartida([FromBody] ConvidarParaPartidaCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Placar/Registrar")]
    public async Task<IActionResult> RegistrarPlacar([FromBody] RegistrarPlacarCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Placar/Responder")]
    public async Task<IActionResult> ResponderPlacar()
    {
        return Ok();
    }

    [HttpGet("Convites/Pendentes")]
    public async Task<IActionResult> ConvitesPendentes()
    {
        return Ok();
    }

    [HttpPut("Convites/Responder")]
    public async Task<IActionResult> ResponderConvite()
    {
        return Ok();
    }
}
