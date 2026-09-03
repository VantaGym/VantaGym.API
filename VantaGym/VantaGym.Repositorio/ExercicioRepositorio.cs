using Dapper;
using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;
using System.Data;

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
        var parametros = new { ExercicioID = exercicioId };

        var exercicios = await _contexto.Database.GetDbConnection().QueryAsync<Exercicio, FichaTreino, Exercicio>(
                "sp_Exercicio_Obter", (exercicio, fichaTreino) =>
                {
                    exercicio.FichaTreino = fichaTreino;
                    return exercicio;
                }, parametros, splitOn: "FichaID", commandType: CommandType.StoredProcedure);

        return exercicios.FirstOrDefault();
    }

    public async Task<IEnumerable<Exercicio>> ListarAsync()
    {
        var exercicios = await _contexto.Database.GetDbConnection().QueryAsync<Exercicio, FichaTreino, Exercicio>(
                "sp_Exercicio_Listar", (exercicio, fichaTreino) =>
                {
                    exercicio.FichaTreino = fichaTreino;
                    return exercicio;
                }, splitOn: "FichaID", commandType: CommandType.StoredProcedure);

        return exercicios;
    }
}