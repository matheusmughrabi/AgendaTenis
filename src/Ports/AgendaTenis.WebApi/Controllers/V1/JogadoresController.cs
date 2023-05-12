using AgendaTenis.Core.Jogadores.Aplicacao.BuscarAdversarios;
using AgendaTenis.Core.Jogadores.Aplicacao.CompletarPerfil;
using AgendaTenis.Core.Jogadores.Aplicacao.ObterResumoJogador;
using AgendaTenis.Core.Jogadores.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AgendaTenis.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class JogadoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public JogadoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Perfil/Completar")]
    [Authorize]
    public async Task<IActionResult> CompletarPerfil([FromBody] CompletarPerfilCommand request)
    {
        request.UsuarioId = Guid.Parse(this.User.Identity.Name);
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("Adversarios/Buscar")]
    [Authorize]
    public async Task<IActionResult> BuscarAdversarios(string pais, string estado, string cidade, CategoriaEnum categoria)
    {
        var request = new BuscarAdversariosCommand()
        {
            UsuarioId = Guid.Parse(this.User.Identity.Name),
            Pais = pais,
            Estado = estado,
            Cidade = cidade,
            Categoria = categoria
        };

        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("Resumo")]
    [Authorize]
    public async Task<IActionResult> ObterResumoJogador()
    {
        var request = new ObterResumoJogadorCommand() { UsuarioId = Guid.Parse(this.User.Identity.Name) };
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
