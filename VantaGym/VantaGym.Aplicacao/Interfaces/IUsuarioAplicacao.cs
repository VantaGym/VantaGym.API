using VantaGym.Dominio.Entidades;
using VantaGym.Aplicacao.Models.Requisicao;
using VantaGym.Api.Models.Resposta;
using VantaGym.Api.Models.Requisicao;

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