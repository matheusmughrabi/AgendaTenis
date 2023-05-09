using Microsoft.AspNetCore.Mvc;

namespace AgendaTenis.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class JogadoresController : ControllerBase
{
    [HttpPost("Perfil/Completar")]
    public async Task<IActionResult> CompletarPerfil()
    {
        return Ok();
    }

    [HttpGet("Adversarios/Buscar")]
    public async Task<IActionResult> BuscarAdversarios()
    {
        return Ok();
    }

    [HttpGet("Resumo")]
    public async Task<IActionResult> ObterResumoJogador()
    {
        return Ok();
    }
}
