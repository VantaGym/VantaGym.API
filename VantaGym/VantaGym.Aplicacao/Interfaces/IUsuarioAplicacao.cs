using VantaGym.Dominio.Entidades;
using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;
using VantaGym.Api.DTOs.Requisicao;

namespace VantaGym.Aplicacao
{
    public interface IUsuarioAplicacao
    {
        Task<int> CriarAsync(UsuarioCriar usuarioCriar);
        Task AlterarSenhaAsync(UsuarioAlterarSenha usuarioAlterarSenha);
        Task AtualizarAsync(UsuarioAtualizar usuarioAtualizar);
        Task DeletarAsync(int usuarioID);
        Task<IEnumerable<UsuarioResposta>> ListarAsync();
        Task<UsuarioResposta> ObterAsync(int usuarioID);
    }
}