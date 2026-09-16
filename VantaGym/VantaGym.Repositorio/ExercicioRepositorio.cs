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
        var exercicioBanco = await _contexto.Exercicios.FirstOrDefaultAsync(x => x.ID == exercicio.ID);
        exercicioBanco.Ativo = false;
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Exercicio exercicio)
    {
        var exercicioBanco = await _contexto.Exercicios.FirstOrDefaultAsync(x => x.ID == exercicio.ID);

        exercicioBanco.Nome = exercicio.Nome;
        exercicioBanco.Series = exercicio.Series;
        exercicioBanco.Repeticoes = exercicio.Repeticoes;
        exercicioBanco.GrupoMuscular = exercicio.GrupoMuscular;

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

    public async Task<IEnumerable<Exercicio>> ListarAsync(int usuarioID)
    {
        var parametros = new { UsuarioID = usuarioID };

        var exercicios = await _contexto.Database.GetDbConnection().QueryAsync<Exercicio, FichaTreino, Exercicio>(
                "sp_Exercicio_Listar", (exercicio, fichaTreino) =>
                {
                    exercicio.FichaTreino = fichaTreino;
                    return exercicio;
                }, parametros, splitOn: "FichaID", commandType: CommandType.StoredProcedure);
        return exercicios;
    }

    public async Task<IEnumerable<Exercicio>> ListarPorFichaAsync(int fichaTreinoID)
    {
        var exercicios = await _contexto.Database.GetDbConnection().QueryAsync<Exercicio, FichaTreino, Exercicio>(
            "sp_Exercicio_ListarPorFicha", (exercicio, fichaTreino) =>
            {
                exercicio.FichaTreino = fichaTreino;
                return exercicio;
            }, new { FichaTreinoID = fichaTreinoID }, splitOn: "FichaID", commandType: CommandType.StoredProcedure
        );
        return exercicios;
    }
}