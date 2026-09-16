using VantaGym.Dominio.Entidades;
public interface IUsuarioRepositorio
{
    Task<int> SalvarAsync(Usuario usuario);
    Task DeleteAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task<Usuario> ObterAsync(int usuarioID);
    Task<Usuario> ObterPorEmailAsync(string email);
}