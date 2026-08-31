using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;

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
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        _contexto.Usuarios.Update(usuario);
        await _contexto.SaveChangesAsync();
    }

    public async Task<Usuario> ObterAsync(int usuarioId)
    {
        return await _contexto.Usuarios
            .Where(u => u.ID == usuarioId)
            .Where(u => u.Ativo)
            .FirstOrDefaultAsync();
    }

    public async Task<Usuario> ObterPorEmailAsync(string email)
    {
        return await _contexto.Usuarios
            .Where(u => u.Email == email)
            .Where(u => u.Ativo)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Usuario>> ListarAsync()
    {
        return await _contexto.Usuarios
            .AsNoTracking()
            .ToListAsync();
    }
}