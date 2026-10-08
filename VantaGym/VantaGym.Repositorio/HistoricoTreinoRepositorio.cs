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
                }, parametros, splitOn: "FichaID,ExercicioSplitID", commandType: CommandType.StoredProcedure);

        return historicos.FirstOrDefault();
    }

    public async Task<IEnumerable<HistoricoTreino>> ListarAsync(int usuarioID)
    {
        var parametros = new { UsuarioID = usuarioID };

        return await _contexto.Database.GetDbConnection().QueryAsync<HistoricoTreino, FichaTreino, Exercicio, HistoricoTreino>(
                "sp_HistoricoTreino_Listar", (historico, fichaTreino, exercicio) =>
                {
                    historico.FichaTreino = fichaTreino;
                    historico.Exercicio = exercicio;
                    return historico;
                }, parametros, splitOn: "ID,ID", commandType: CommandType.StoredProcedure);
    }

    public async Task<decimal> ObterVolumeUltimoTreinoAsync(int usuarioID)
    {
        var parametros = new
        {
            UsuarioID = usuarioID
        };

        return await _contexto.Database.GetDbConnection().QueryFirstOrDefaultAsync<decimal>(
            @"SELECT ISNULL(SUM(
            dbo.fn_CalcularVolumeTreino(
                h.CargaKg,
                h.RepeticoesFeitas,
                h.SeriesFeitas
            )), 0)
                FROM HistoricoTreino h
                    INNER JOIN FichaTreino f
                    ON h.FichaTreinoID = f.FichaTreinoID
                        WHERE h.Ativo = 1
                            AND f.UsuarioID = @UsuarioID
                            AND h.DataExecucao = (
                            SELECT MAX(h2.DataExecucao)
                                FROM HistoricoTreino h2
                                    INNER JOIN FichaTreino f2
                                    ON h2.FichaTreinoID = f2.FichaTreinoID
                                        WHERE h2.Ativo = 1
                                            AND f2.UsuarioID = @UsuarioID)", parametros);
    }

    public async Task<decimal> ObterVolumeSemanaAsync(DateTime inicioSemana, int usuarioID)
    {
        var parametros = new
        {
            InicioSemana = inicioSemana,
            UsuarioID = usuarioID
        };

        return await _contexto.Database.GetDbConnection().QueryFirstOrDefaultAsync<decimal>(
            @"SELECT ISNULL(SUM(
        dbo.fn_CalcularVolumeTreino(h.CargaKg, h.RepeticoesFeitas, h.SeriesFeitas)), 0)
            FROM HistoricoTreino h
            INNER JOIN FichaTreino f
                ON h.FichaTreinoID = f.FichaTreinoID
                WHERE h.DataExecucao >= @InicioSemana
                    AND h.Ativo = 1
                    AND f.UsuarioID = @UsuarioID", parametros);
    }
}