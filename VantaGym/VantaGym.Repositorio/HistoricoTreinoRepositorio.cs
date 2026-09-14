using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;
using Dapper;
using System.Data;

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
        _contexto.HistoricoTreinos.Update(historicoTreino);
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(HistoricoTreino historicoTreino)
    {
        _contexto.HistoricoTreinos.Update(historicoTreino);
        await _contexto.SaveChangesAsync();
    }

    public async Task<HistoricoTreino> ObterAsync(int historicoTreinoId)
    {
        var parametros = new { HistoricoTreinoID = historicoTreinoId };

        var historicos = await _contexto.Database.GetDbConnection().QueryAsync<HistoricoTreino, FichaTreino, Exercicio, HistoricoTreino>(
                "sp_HistoricoTreino_Obter", (historico, fichaTreino, exercicio) =>
                {
                    historico.FichaTreino = fichaTreino;
                    historico.Exercicio = exercicio;
                    return historico;
                }, parametros, splitOn: "ID,ID", commandType: CommandType.StoredProcedure);

        return historicos.FirstOrDefault();
    }

    public async Task<IEnumerable<HistoricoTreino>> ListarAsync()
    {
        return await _contexto.Database.GetDbConnection().QueryAsync<HistoricoTreino, FichaTreino, Exercicio, HistoricoTreino>(
                "sp_HistoricoTreino_Listar", (historico, fichaTreino, exercicio) =>
                {
                    historico.FichaTreino = fichaTreino;
                    historico.Exercicio = exercicio;
                    return historico;
                }, splitOn: "FichaID,ExercicioID", commandType: CommandType.StoredProcedure);
    }

    public async Task<decimal> ObterVolumeUltimoTreinoAsync(DateTime dataExecucao, int fichaTreinoID)
    {
        var parametros = new
        {
            DataExecucao = dataExecucao.Date,
            FichaTreinoID = fichaTreinoID
        };

        return await _contexto.Database.GetDbConnection().QueryFirstOrDefaultAsync<decimal>(
            @"SELECT ISNULL(SUM(
            dbo.fn_CalcularVolumeTreino(CargaKg, RepeticoesFeitas, SeriesFeitas)), 0)
                FROM HistoricoTreino
                    WHERE CAST(DataExecucao AS DATE) = @DataExecucao
                        AND FichaTreinoID = @FichaTreinoID
                            AND Ativo = 1", parametros);
    }

    public async Task<decimal> ObterVolumeSemanaAsync(DateTime inicioSemana)
    {
        var parametros = new { InicioSemana = inicioSemana };

        return await _contexto.Database.GetDbConnection().QueryFirstOrDefaultAsync<decimal>(
            @"SELECT ISNULL(SUM(
            dbo.fn_CalcularVolumeTreino(CargaKg, RepeticoesFeitas, SeriesFeitas)), 0)
                FROM HistoricoTreino
                    WHERE DataExecucao >= @InicioSemana
                        AND Ativo = 1", parametros);
    }
}