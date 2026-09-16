using Microsoft.AspNetCore.Mvc;
using VantaGym.Aplicacao;
using VantaGym.Aplicacao.DTOs.Requisicao;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [Authorize]
        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] HistoricoTreinoCriar historicoTreinoCriar)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var historicoTreino = await _historicoTreinoAplicacao.CriarAsync(historicoTreinoCriar, usuarioID);
                return Ok(historicoTreino);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Obter/{historicoTreinoID}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int historicoTreinoID)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var historicoTreinoResposta = await _historicoTreinoAplicacao.ObterAsync(historicoTreinoID, usuarioID);
                return Ok(historicoTreinoResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] HistoricoTreinoAtualizar historicoTreinoAtualizar)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _historicoTreinoAplicacao.AtualizarAsync(historicoTreinoAtualizar, usuarioID);
                return Ok(historicoTreinoAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Deletar/{historicoTreinoID}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int historicoTreinoID)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _historicoTreinoAplicacao.DeletarAsync(historicoTreinoID, usuarioID);
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
                var historicos = await _historicoTreinoAplicacao.ListarAsync(usuarioID);
                return Ok(historicos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("ObterQuantidadeTreinosSemana")]
        public async Task<IActionResult> ObterQuantidadeTreinosSemanaAsync()
        {
            var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var quantidade = await _historicoTreinoAplicacao.ObterQuantidadeTreinosSemanaAsync(usuarioID);
            return Ok(quantidade);
        }

        [Authorize]
        [HttpGet]
        [Route("ObterVolumeUltimoTreino")]
        public async Task<IActionResult> ObterVolumeUltimoTreinoAsync()
        {
            var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var volume = await _historicoTreinoAplicacao.ObterVolumeUltimoTreinoAsync(usuarioID);
            return Ok(volume);
        }

        [Authorize]
        [HttpGet]
        [Route("ObterUltimoTreino")]
        public async Task<IActionResult> ObterUltimoTreinoAsync()
        {
            var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var ultimoTreino = await _historicoTreinoAplicacao.ObterUltimoTreinoAsync(usuarioID);
            return Ok(ultimoTreino);
        }

        [Authorize]
        [HttpGet]
        [Route("ObterQuantidadeExerciciosUltimoTreino")]
        public async Task<IActionResult> ObterQuantidadeExerciciosUltimoTreinoAsync()
        {
            var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var quantidade = await _historicoTreinoAplicacao.ObterQuantidadeExerciciosUltimoTreinoAsync(usuarioID);
            return Ok(quantidade);
        }

        [Authorize]
        [HttpGet]
        [Route("ObterVolumeSemana")]
        public async Task<IActionResult> ObterVolumeSemanaAsync()
        {
            var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var volume = await _historicoTreinoAplicacao.ObterVolumeSemanaAsync(usuarioID);
            return Ok(volume);
        }

        [Authorize]
        [HttpGet]
        [Route("ObterTempoDesdeUltimoTreino")]
        public async Task<IActionResult> ObterTempoDesdeUltimoTreinoAsync()
        {
            var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var tempo = await _historicoTreinoAplicacao.ObterTempoDesdeUltimoTreinoAsync(usuarioID);
            return Ok(tempo);
        }
    }
}