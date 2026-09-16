using Microsoft.AspNetCore.Mvc;
using VantaGym.Aplicacao;
using VantaGym.Aplicacao.DTOs.Requisicao;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace VantaGym.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ExercicioController : ControllerBase
    {
        private readonly IExercicioAplicacao _exercicioAplicacao;
        public ExercicioController(IExercicioAplicacao exercicioAplicacao)
        {
            _exercicioAplicacao = exercicioAplicacao;
        }

        [Authorize]
        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] ExercicioCriar exercicioCriar)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var exercicio = await _exercicioAplicacao.CriarAsync(exercicioCriar, usuarioID);
                return Ok(exercicio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Obter/{exercicioID}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int exercicioID)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var exercicioResposta = await _exercicioAplicacao.ObterAsync(exercicioID, usuarioID);
                return Ok(exercicioResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] ExercicioAtualizar exercicioAtualizar)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _exercicioAplicacao.AtualizarAsync(exercicioAtualizar, usuarioID);
                return Ok(exercicioAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Deletar/{exercicioID}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int exercicioID)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _exercicioAplicacao.DeletarAsync(exercicioID, usuarioID);
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
                var exercicios = await _exercicioAplicacao.ListarAsync(usuarioID);
                return Ok(exercicios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ListarGruposMusculares")]
        public ActionResult ListarGruposMusculares()
        {
            try
            {
                var gruposMusculares = _exercicioAplicacao.ListarGruposMusculares();
                return Ok(gruposMusculares);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}