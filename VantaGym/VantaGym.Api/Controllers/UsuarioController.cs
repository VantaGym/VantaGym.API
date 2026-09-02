using Microsoft.AspNetCore.Mvc;
using VantaGym.Api.DTOs.Requisicao;
using VantaGym.Aplicacao;
using VantaGym.Aplicacao.DTOs.Requisicao;

namespace VantaGym.Api
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;
        public UsuarioController(IUsuarioAplicacao usuarioAplicacao)
        {
            _usuarioAplicacao = usuarioAplicacao;
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarAsync([FromBody] UsuarioCriar usuarioCriar)
        {
            try
            {
                var usuario = await _usuarioAplicacao.CriarAsync(usuarioCriar);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obter/{usuarioID}")]
        public async Task<ActionResult> ObterAsync([FromRoute] int usuarioID)
        {
            try
            {
                var usuarioResposta = await _usuarioAplicacao.ObterAsync(usuarioID);
                return Ok(usuarioResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] UsuarioAtualizar usuarioAtualizar)
        {
            try
            {
                await _usuarioAplicacao.AtualizarAsync(usuarioAtualizar);
                return Ok(usuarioAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("AlterarSenha")]
        public async Task<ActionResult> AlterarSenhaAsync([FromBody] UsuarioAlterarSenha usuarioAlterarSenha)
        {
            try
            {
                await _usuarioAplicacao.AlterarSenhaAsync(usuarioAlterarSenha);
                return Ok(usuarioAlterarSenha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Deletar/{usuarioID}")]
        public async Task<ActionResult> DeletarAsync([FromRoute] int usuarioID)
        {
            try
            {
                await _usuarioAplicacao.DeletarAsync(usuarioID);
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
                var usuarios = await _usuarioAplicacao.ListarAsync();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}