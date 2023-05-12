using AgendaTenis.Core.Partidas.Aplicacao.ConvidarParaPartida;
using AgendaTenis.Core.Partidas.Aplicacao.ConvitesPendentes;
using AgendaTenis.Core.Partidas.Aplicacao.RegistrarPlacar;
using AgendaTenis.Core.Partidas.Aplicacao.ResponderConvite;
using AgendaTenis.Core.Partidas.Aplicacao.ResponderPlacar;
using AgendaTenis.WebApi.Polices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("Convites/Convidar")]
    [Authorize]
    public async Task<IActionResult> ConvidarParaPartida([FromBody] ConvidarParaPartidaCommand command)
    {
        command.DesafianteId = Guid.Parse(this.User.Identity.Name);
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Convites/Responder")]
    [Authorize]
    public async Task<IActionResult> ResponderConvite([FromServices] AdversarioDaPartidaPoliceHandler policeHandler, [FromBody] ResponderConviteCommand command)
    {
        var usuarioEhAdversarioDaPartida = await policeHandler.Validar(command.Id);
        if (!usuarioEhAdversarioDaPartida)
            return Unauthorized();

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("Convites/Pendentes")]
    [Authorize]
    public async Task<IActionResult> ConvitesPendentes()
    {
        var response = await _mediator.Send(new ObterConvitesPendentesCommand() { UsuarioId = this.User.Identity.Name });
        return Ok(response);
    }

    [HttpPut("Placar/Registrar")]
    [Authorize]
    public async Task<IActionResult> RegistrarPlacar([FromServices] JogadorDaPartidaPoliceHandler policeHandler, [FromBody] RegistrarPlacarCommand command)
    {
        var usuarioEhJogadorDaPartida = await policeHandler.Validar(command.Id);
        if (!usuarioEhJogadorDaPartida)
            return Unauthorized();

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Placar/Responder")]
    [Authorize]
    public async Task<IActionResult> ResponderPlacar([FromServices] JogadorDaPartidaPoliceHandler policeHandler, [FromBody] ResponderPlacarCommand command)
    {
        var usuarioEhJogadorDaPartida = await policeHandler.Validar(command.Id);
        if (!usuarioEhJogadorDaPartida)
            return Unauthorized();

        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
