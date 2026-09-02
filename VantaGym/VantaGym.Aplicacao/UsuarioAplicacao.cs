using VantaGym.Dominio.Entidades;
using VantaGym.Api.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;
using System.Net.Mail;
using VantaGym.Aplicacao.DTOs.Requisicao;

namespace VantaGym.Aplicacao;

public class UsuarioAplicacao : IUsuarioAplicacao
{
    readonly IUsuarioRepositorio _usuarioRepositorio;
    public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }
    public async Task<int> CriarAsync(UsuarioCriar usuarioCriar)
    {
        if (usuarioCriar == null)
            throw new Exception("Usuário não pode ser vazio!");

        var usuario = new Usuario
        {
            Nome = usuarioCriar.Nome,
            Email = usuarioCriar.Email,
            Senha = usuarioCriar.Senha
        };

        ValidarInformacoesUsuario(usuario);

        if (string.IsNullOrEmpty(usuario.Senha))
            throw new Exception("Senha não pode ser vazia!");

        return await _usuarioRepositorio.SalvarAsync(usuario);
    }
    public async Task<UsuarioResposta> ObterAsync(int usuarioID)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuarioID);
        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        var usuarioResposta = new UsuarioResposta()
        {
            Id = usuarioDominio.ID,
            Nome = usuarioDominio.Nome,
            Email = usuarioDominio.Email
        };

        return usuarioResposta;
    }
    public async Task<IEnumerable<UsuarioResposta>> ListarAsync()
    {
        var usuarios = await _usuarioRepositorio.ListarAsync();

        var usuariosResposta = usuarios.Select(usuario => new UsuarioResposta
        {
            Id = usuario.ID,
            Nome = usuario.Nome,
            Email = usuario.Email
        }).ToList();

        return usuariosResposta;
    }
    public async Task AtualizarAsync(UsuarioAtualizar usuarioAtualizar)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuarioAtualizar.ID);

        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        usuarioDominio.Nome = usuarioAtualizar.Nome;
        usuarioDominio.Email = usuarioAtualizar.Email;

        ValidarInformacoesUsuario(usuarioDominio);

        await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
    }
    public async Task AlterarSenhaAsync(UsuarioAlterarSenha usuarioAlterarSenha)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuarioAlterarSenha.ID);

        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        if (usuarioDominio.Senha != usuarioAlterarSenha.SenhaAntiga)
            throw new Exception("Senha antiga inválida");

        usuarioDominio.Senha = usuarioAlterarSenha.Senha;
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