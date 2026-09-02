using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;

namespace VantaGym.Repositorio;

public class HistoricoTreinoRepositorio : BaseRepositorio, IHistoricoTreinoRepositorio
{
    public HistoricoTreinoRepositorio(VantaGymContexto contexto) : base(contexto)
    {
    }

    public async Task<int> SalvarAsync(HistoricoTreino historicoTreino)
    {
        await _contexto.HistoricoTreinos.AddAsync(historicoTreino);
        await _contexto.SaveChangesAsync();

        return historicoTreino.ID;
    }

    public async Task DeleteAsync(HistoricoTreino historicoTreino)
    {
        historicoTreino.Ativo = false;
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(HistoricoTreino historicoTreino)
    {
        _contexto.HistoricoTreinos.Update(historicoTreino);
        await _contexto.SaveChangesAsync();
    }

    public async Task<HistoricoTreino> ObterAsync(int historicoTreinoId)
    {
        return await _contexto.HistoricoTreinos
            .Include(e => e.FichaTreino)
            .Include(e => e.Exercicio)
            .Where(u => u.ID == historicoTreinoId)
            .Where(u => u.Ativo)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<HistoricoTreino>> ListarAsync()
    {
        return await _contexto.HistoricoTreinos
            .Include(e => e.FichaTreino)
            .Include(e => e.Exercicio)
            .AsNoTracking()
            .ToListAsync();
    }
}