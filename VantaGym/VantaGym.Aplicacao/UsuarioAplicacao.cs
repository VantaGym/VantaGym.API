using VantaGym.Dominio.Entidades;
using System.Net.Mail;

namespace VantaGym.Aplicacao;

public class UsuarioAplicacao : IUsuarioAplicacao
{
    readonly IUsuarioRepositorio _usuarioRepositorio;
    public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }
    public async Task<int> CriarAsync(Usuario usuario)
    {
        if (usuario == null)
            throw new Exception("Usuário não pode ser vazio!");

        ValidarInformacoesUsuario(usuario);

        if (string.IsNullOrEmpty(usuario.Senha))
            throw new Exception("Senha não pode ser vazia!");

        return await _usuarioRepositorio.SalvarAsync(usuario);
    }
    public async Task<Usuario> ObterAsync(int usuarioID)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuarioID);
        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        return usuarioDominio;
    }
    public async Task<Usuario> ObterPorEmailAsync(string email)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterPorEmailAsync(email);
        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        return usuarioDominio;
    }
    public async Task<IEnumerable<Usuario>> ListarAsync()
    {
        return await _usuarioRepositorio.ListarAsync();
    }
    public async Task AtualizarAsync(Usuario usuario)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuario.ID);

        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        ValidarInformacoesUsuario(usuario);

        usuarioDominio.Nome = usuario.Nome;
        usuarioDominio.Email = usuario.Email;

        await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
    }
    public async Task AlterarSenhaAsync(Usuario usuario, string senhaAntiga)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuario.ID);

        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        if (usuarioDominio.Senha != senhaAntiga)
            throw new Exception("Senha antiga inválida");

        usuarioDominio.Senha = usuario.Senha;
        await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
    }
    public async Task DeletarAsync(int usuarioID)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuarioID);

        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        await _usuarioRepositorio.DeleteAsync(usuarioDominio);
    }

    #region Util
    private static void ValidarInformacoesUsuario(Usuario usuario)
    {
        if (string.IsNullOrEmpty(usuario.Nome))
            throw new Exception("Nome não pode ser vazio!");

        if (string.IsNullOrEmpty(usuario.Email))
            throw new Exception("Email não pode ser vazio!");

        if (!EmailValido(usuario.Email))
            throw new Exception("Responsável deve ser um e-mail válido!");
    }

    private static bool EmailValido(string email)
    {
        try
        {
            var endereco = new MailAddress(email);
            return endereco.Address == email;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}