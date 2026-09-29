using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;
using Dapper;
using System.Data;

namespace DataAccess.Repositorio;

public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
{
    public UsuarioRepositorio(VantaGymContexto contexto) : base(contexto)
    {
    }

    public async Task<int> SalvarAsync(Usuario usuario)
    {
        await _contexto.Usuarios.AddAsync(usuario);
        await _contexto.SaveChangesAsync();

        return usuario.ID;
    }

    public async Task DeleteAsync(Usuario usuario)
    {
        usuario.Ativo = false;
        _contexto.Usuarios.Update(usuario);
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        _contexto.Usuarios.Update(usuario);
        await _contexto.SaveChangesAsync();
    }

    public async Task<Usuario> ObterAsync(int usuarioId)
    {
        var parametros = new { UsuarioID = usuarioId };
        return await _contexto.Database.GetDbConnection().QueryFirstOrDefaultAsync<Usuario>(
            "sp_Usuario_Obter", parametros, commandType: CommandType.StoredProcedure);
    }

    public async Task<Usuario> ObterPorEmailAsync(string email)
    {
        var parametros = new { Email = email };
        return await _contexto.Database.GetDbConnection().QueryFirstOrDefaultAsync<Usuario>(
            "sp_Usuario_ObterPorEmail", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task<bool> EmailExistenteAsync(string email)
    {
        return await _contexto.Usuarios.AnyAsync(u => u.Email == email);
    }
}