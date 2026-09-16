using Microsoft.AspNetCore.Mvc;
using VantaGym.Aplicacao;
using VantaGym.Aplicacao.DTOs.Requisicao;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace VantaGym.Api
{
    [ApiController]
    [Route("[controller]")]
    public class FichaTreinoController : ControllerBase
    {
        private readonly IFichaTreinoAplicacao _fichaTreinoAplicacao;
        public FichaTreinoController(IFichaTreinoAplicacao fichaTreinoAplicacao)
        {
            _fichaTreinoAplicacao = fichaTreinoAplicacao;
        }

        [Authorize]
        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] FichaTreinoCriar fichaTreinoCriar)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var fichaTreino = await _fichaTreinoAplicacao.CriarAsync(fichaTreinoCriar, usuarioID);
                return Ok(fichaTreino);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Obter/{fichaTreinoID}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int fichaTreinoID)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var fichaTreinoResposta = await _fichaTreinoAplicacao.ObterAsync(fichaTreinoID, usuarioID);
                return Ok(fichaTreinoResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] FichaTreinoAtualizar fichaTreinoAtualizar)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _fichaTreinoAplicacao.AtualizarAsync(fichaTreinoAtualizar, usuarioID);
                return Ok(fichaTreinoAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Deletar/{fichaTreinoID}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int fichaTreinoID)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _fichaTreinoAplicacao.DeletarAsync(fichaTreinoID, usuarioID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult> ListarAsync()
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var fichaTreinos = await _fichaTreinoAplicacao.ListarAsync(usuarioID);
                return Ok(fichaTreinos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}