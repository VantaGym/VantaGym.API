using VantaGym.Dominio.Entidades;
using VantaGym.Api.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;
using System.Net.Mail;
using VantaGym.Aplicacao.DTOs.Requisicao;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace VantaGym.Aplicacao;

public class UsuarioAplicacao : IUsuarioAplicacao
{
    readonly IUsuarioRepositorio _usuarioRepositorio;
    readonly IConfiguration _configuration;
    public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _configuration = configuration;
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

        if (await _usuarioRepositorio.EmailExistenteAsync(usuario.Email))
            throw new Exception("Já existe uma conta com este email!");

        if (usuario.Senha.Length < 6 || usuario.Senha.Length > 15)
            throw new Exception("A senha deve ter entre 6 e 15 caracteres!");

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
    public async Task AtualizarAsync(UsuarioAtualizar usuarioAtualizar, int usuarioID)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuarioID);

        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        usuarioDominio.Nome = usuarioAtualizar.Nome;
        usuarioDominio.Email = usuarioAtualizar.Email;

        ValidarInformacoesUsuario(usuarioDominio);

        await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
    }
    public async Task AlterarSenhaAsync(UsuarioAlterarSenha usuarioAlterarSenha, int usuarioID)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuarioID);

        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        if (usuarioDominio.Senha != usuarioAlterarSenha.SenhaAntiga)
            throw new Exception("Senha antiga inválida");

        usuarioDominio.Senha = usuarioAlterarSenha.SenhaNova;
        await _usuarioRepositorio.AtualizarAsync(usuarioDominio);
    }
    public async Task DeletarAsync(int usuarioID)
    {
        var usuarioDominio = await _usuarioRepositorio.ObterAsync(usuarioID);

        if (usuarioDominio == null)
            throw new Exception("Usuário não encontrado!");

        await _usuarioRepositorio.DeleteAsync(usuarioDominio);
    }
    public async Task<UsuarioLoginResposta> LoginAsync(UsuarioLogin usuarioLogin)
    {
        if (usuarioLogin == null)
            throw new Exception("Login não pode ser vazio!");

        if (string.IsNullOrEmpty(usuarioLogin.Email))
            throw new Exception("Email não pode ser vazio!");

        if (string.IsNullOrEmpty(usuarioLogin.Senha))
            throw new Exception("Senha não pode ser vazia!");

        var usuarioDominio = await _usuarioRepositorio.ObterPorEmailAsync(usuarioLogin.Email);

        if (usuarioDominio == null || usuarioDominio.Senha != usuarioLogin.Senha)
            throw new Exception("Email ou senha inválidos!");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuarioDominio.ID.ToString()),
            new Claim(ClaimTypes.Name, usuarioDominio.Nome)
        };

        var chave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
        );

        var credenciais = new SigningCredentials(
            chave,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpirationHours"])),
            signingCredentials: credenciais
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new UsuarioLoginResposta
        {
            Id = usuarioDominio.ID,
            Nome = usuarioDominio.Nome,
            Email = usuarioDominio.Email,
            Token = tokenString
        };
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