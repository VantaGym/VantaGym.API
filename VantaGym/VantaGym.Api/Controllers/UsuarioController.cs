using Microsoft.AspNetCore.Mvc;
using VantaGym.Api.DTOs.Requisicao;
using VantaGym.Aplicacao;
using VantaGym.Aplicacao.DTOs.Requisicao;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        [Route("Obter")]
        public async Task<ActionResult> ObterAsync()
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var usuarioResposta = await _usuarioAplicacao.ObterAsync(usuarioID);
                return Ok(usuarioResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> AtualizarAsync([FromBody] UsuarioAtualizar usuarioAtualizar)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _usuarioAplicacao.AtualizarAsync(usuarioAtualizar, usuarioID);
                return Ok(usuarioAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("AlterarSenha")]
        public async Task<ActionResult> AlterarSenhaAsync([FromBody] UsuarioAlterarSenha usuarioAlterarSenha)
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _usuarioAplicacao.AlterarSenhaAsync(usuarioAlterarSenha, usuarioID);
                return Ok(usuarioAlterarSenha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Deletar")]
        public async Task<ActionResult> DeletarAsync()
        {
            try
            {
                var usuarioID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _usuarioAplicacao.DeletarAsync(usuarioID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] UsuarioLogin usuarioLogin)
        {
            try
            {
                var usuario = await _usuarioAplicacao.LoginAsync(usuarioLogin);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}