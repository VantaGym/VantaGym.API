using VantaGym.Dominio.Entidades;
using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;
using VantaGym.Api.DTOs.Requisicao;

namespace VantaGym.Aplicacao
{
    public interface IUsuarioAplicacao
    {
        Task<int> CriarAsync(UsuarioCriar usuarioCriar);
        Task AlterarSenhaAsync(UsuarioAlterarSenha usuarioAlterarSenha, int usuarioID);
        Task AtualizarAsync(UsuarioAtualizar usuarioAtualizar, int usuarioID);
        Task DeletarAsync(int usuarioID);
        Task<UsuarioResposta> ObterAsync(int usuarioID);
        Task<UsuarioLoginResposta> LoginAsync(UsuarioLogin usuarioLogin);
    }
}