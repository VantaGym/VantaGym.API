using Microsoft.AspNetCore.Mvc;
using VantaGym.Aplicacao;
using VantaGym.Aplicacao.DTOs.Requisicao;

namespace VantaGym.Api
{
    [ApiController]
    [Route("[controller]")]
    public class HistoricoTreinoController : ControllerBase
    {
        private readonly IHistoricoTreinoAplicacao _historicoTreinoAplicacao;
        public HistoricoTreinoController(IHistoricoTreinoAplicacao historicoTreinoAplicacao)
        {
            _historicoTreinoAplicacao = historicoTreinoAplicacao;
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] HistoricoTreinoCriar historicoTreinoCriar)
        {
            try
            {
                var historicoTreino = await _historicoTreinoAplicacao.CriarAsync(historicoTreinoCriar);
                return Ok(historicoTreino);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obter/{historicoTreinoID}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int historicoTreinoID)
        {
            try
            {
                var historicoTreinoResposta = await _historicoTreinoAplicacao.ObterAsync(historicoTreinoID);
                return Ok(historicoTreinoResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] HistoricoTreinoAtualizar historicoTreinoAtualizar)
        {
            try
            {
                await _historicoTreinoAplicacao.AtualizarAsync(historicoTreinoAtualizar);
                return Ok(historicoTreinoAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{historicoTreinoID}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int historicoTreinoID)
        {
            try
            {
                await _historicoTreinoAplicacao.DeletarAsync(historicoTreinoID);
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
                var historicos = await _historicoTreinoAplicacao.ListarAsync();
                return Ok(historicos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}