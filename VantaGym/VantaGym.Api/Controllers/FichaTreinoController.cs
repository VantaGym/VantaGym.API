using Microsoft.AspNetCore.Mvc;
using VantaGym.Aplicacao;
using VantaGym.Aplicacao.DTOs.Requisicao;

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

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] FichaTreinoCriar fichaTreinoCriar)
        {
            try
            {
                var fichaTreino = await _fichaTreinoAplicacao.CriarAsync(fichaTreinoCriar);
                return Ok(fichaTreino);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obter/{fichaTreinoID}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int fichaTreinoID)
        {
            try
            {
                var fichaTreinoResposta = await _fichaTreinoAplicacao.ObterAsync(fichaTreinoID);
                return Ok(fichaTreinoResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] FichaTreinoAtualizar fichaTreinoAtualizar)
        {
            try
            {
                await _fichaTreinoAplicacao.AtualizarAsync(fichaTreinoAtualizar);
                return Ok(fichaTreinoAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{fichaTreinoID}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int fichaTreinoID)
        {
            try
            {
                await _fichaTreinoAplicacao.DeletarAsync(fichaTreinoID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult> ListarAsync()
        {
            try
            {
                var fichaTreinos = await _fichaTreinoAplicacao.ListarAsync();
                return Ok(fichaTreinos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}