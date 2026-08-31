using VantaGym.Dominio.Entidades;

namespace VantaGym.Aplicacao
{
    public interface IUsuarioAplicacao
    {
        Task<int> CriarAsync(Usuario usuario);
        Task AlterarSenhaAsync(Usuario usuario, string novaSenha);
        Task AtualizarAsync(Usuario usuario);
        Task DeletarAsync(int usuarioID);
        Task<IEnumerable<Usuario>> ListarAsync();
        Task<Usuario> ObterAsync(int usuarioID);
        Task<Usuario> ObterPorEmailAsync(string email);
    }
}