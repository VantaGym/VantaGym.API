using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;

namespace DataAccess.Repositorio;

public class FichaTreinoRepositorio : BaseRepositorio, IFichaTreinoRepositorio
{
    public FichaTreinoRepositorio(VantaGymContexto contexto) : base(contexto)
    {
    }

    public async Task<int> SalvarAsync(FichaTreino fichaTreino)
    {
        await _contexto.FichaTreinos.AddAsync(fichaTreino);
        await _contexto.SaveChangesAsync();

        return fichaTreino.ID;
    }

    public async Task DeleteAsync(FichaTreino fichaTreino)
    {
        fichaTreino.Ativo = false;
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(FichaTreino fichaTreino)
    {
        _contexto.FichaTreinos.Update(fichaTreino);
        await _contexto.SaveChangesAsync();
    }

    public async Task<FichaTreino> ObterAsync(int fichaTreinoId)
    {
        return await _contexto.FichaTreinos
            .Where(u => u.ID == fichaTreinoId)
            .Where(u => u.Ativo)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<FichaTreino>> ListarAsync()
    {
        return await _contexto.FichaTreinos
            .AsNoTracking()
            .ToListAsync();
    }
}