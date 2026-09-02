using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;

namespace DataAccess.Repositorio;

public class ExercicioRepositorio : BaseRepositorio, IExercicioRepositorio
{
    public ExercicioRepositorio(VantaGymContexto contexto) : base(contexto)
    {
    }

    public async Task<int> SalvarAsync(Exercicio exercicio)
    {
        await _contexto.Exercicios.AddAsync(exercicio);
        await _contexto.SaveChangesAsync();

        return exercicio.ID;
    }

    public async Task DeleteAsync(Exercicio exercicio)
    {
        exercicio.Ativo = false;
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Exercicio exercicio)
    {
        _contexto.Exercicios.Update(exercicio);
        await _contexto.SaveChangesAsync();
    }

    public async Task<Exercicio> ObterAsync(int exercicioId)
    {
        return await _contexto.Exercicios
            .Include(e => e.FichaTreino)
            .Where(e => e.ID == exercicioId)
            .FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<Exercicio>> ListarAsync()
    {
        return await _contexto.Exercicios
            .Include(e => e.FichaTreino)
            .AsNoTracking()
            .ToListAsync();
    }
}