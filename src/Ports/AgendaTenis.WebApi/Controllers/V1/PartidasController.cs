using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTenis.WebApi.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class PartidasController : ControllerBase
{
    [HttpPost("Convidar")]
    public async Task<IActionResult> ConvidarParaPartida()
    {
        return Ok();
    }

    [HttpPut("Placar/Registrar")]
    public async Task<IActionResult> RegistrarPlacar()
    {
        return Ok();
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
