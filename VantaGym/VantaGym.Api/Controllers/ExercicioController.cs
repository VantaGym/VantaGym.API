using Microsoft.AspNetCore.Mvc;
using VantaGym.Aplicacao;
using VantaGym.Aplicacao.DTOs.Requisicao;

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

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] ExercicioCriar exercicioCriar)
        {
            try
            {
                var exercicio = await _exercicioAplicacao.CriarAsync(exercicioCriar);
                return Ok(exercicio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obter/{exercicioID}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int exercicioID)
        {
            try
            {
                var exercicioResposta = await _exercicioAplicacao.ObterAsync(exercicioID);
                return Ok(exercicioResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] ExercicioAtualizar exercicioAtualizar)
        {
            try
            {
                await _exercicioAplicacao.AtualizarAsync(exercicioAtualizar);
                return Ok(exercicioAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{exercicioID}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int exercicioID)
        {
            try
            {
                await _exercicioAplicacao.DeletarAsync(exercicioID);
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
                var exercicios = await _exercicioAplicacao.ListarAsync();
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