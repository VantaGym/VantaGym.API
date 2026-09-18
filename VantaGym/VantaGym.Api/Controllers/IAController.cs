using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VantaGym.Aplicacao.Interfaces;
using VantaGym.Aplicacao.DTOs;

namespace VantaGym.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class IAController : ControllerBase
{
    private readonly IIAAplicacao _iaAplicacao;

    public IAController(IIAAplicacao iaAplicacao)
    {
        _iaAplicacao = iaAplicacao;
    }

    [HttpPost]
    [Route("GerarTreino")]
    [Authorize]
    public async Task<IActionResult> GerarTreinoAsync([FromBody] TreinoIACriar treino)
    {
        try
        {
            var resposta = await _iaAplicacao.GerarTreinoAsync(treino);
            return Ok(resposta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}