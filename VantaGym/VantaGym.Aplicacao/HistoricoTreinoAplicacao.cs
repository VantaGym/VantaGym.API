using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;
using VantaGym.Dominio.Entidades;
using VantaGym.Aplicacao.DTOs.Resposta;

namespace VantaGym.Aplicacao;

public class HistoricoTreinoAplicacao : IHistoricoTreinoAplicacao
{
    readonly IHistoricoTreinoRepositorio _historicoTreinoRepositorio;
    readonly IExercicioRepositorio _exercicioRepositorio;
    public HistoricoTreinoAplicacao(IHistoricoTreinoRepositorio historicoTreinoRepositorio, IExercicioRepositorio exercicioRepositorio)
    {
        _historicoTreinoRepositorio = historicoTreinoRepositorio;
        _exercicioRepositorio = exercicioRepositorio;
    }
    public async Task<int> CriarAsync(HistoricoTreinoCriar historicoCriar)
    {
        if (historicoCriar == null)
            throw new Exception("Histórico não pode ser vazio!");

        var exercicio = await _exercicioRepositorio.ObterAsync(historicoCriar.ExercicioID);

        if (exercicio == null)
            throw new Exception("Exercício não encontrado!");

        var historico = new HistoricoTreino
        {
            SeriesFeitas = historicoCriar.SeriesFeitas,
            CargaKg = historicoCriar.CargaKg,
            RepeticoesFeitas = historicoCriar.RepeticoesFeitas,
            DataExecucao = DateTime.Now,
            ExercicioID = exercicio.ID,
            FichaTreinoID = exercicio.FichaTreinoID
        };

        return await _historicoTreinoRepositorio.SalvarAsync(historico);
    }
    public async Task<HistoricoTreinoResposta> ObterAsync(int historicoTreinoID)
    {
        var historicoTreino = await _historicoTreinoRepositorio.ObterAsync(historicoTreinoID);

        if (historicoTreino == null)
            throw new Exception("Histórico de treino não encontrado!");

        var historicoTreinoResposta = new HistoricoTreinoResposta
        {
            ID = historicoTreino.ID,
            NomeFicha = historicoTreino.FichaTreino.Nome,
            NomeExercicio = historicoTreino.Exercicio.Nome,
            SeriesFeitas = historicoTreino.SeriesFeitas,
            CargaKg = historicoTreino.CargaKg,
            RepeticoesFeitas = historicoTreino.RepeticoesFeitas,
            DataExecucao = historicoTreino.DataExecucao
        };

        return historicoTreinoResposta;
    }
    public async Task<IEnumerable<HistoricoTreinoResposta>> ListarAsync()
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync();

        var historicosResposta = historicos.Select(historicoTreino => new HistoricoTreinoResposta
        {
            ID = historicoTreino.ID,
            NomeFicha = historicoTreino.FichaTreino.Nome,
            NomeExercicio = historicoTreino.Exercicio.Nome,
            SeriesFeitas = historicoTreino.SeriesFeitas,
            CargaKg = historicoTreino.CargaKg,
            RepeticoesFeitas = historicoTreino.RepeticoesFeitas,
            DataExecucao = historicoTreino.DataExecucao
        }).ToList();

        return historicosResposta;
    }
    public async Task AtualizarAsync(HistoricoTreinoAtualizar historicoAtualizar)
    {
        var historicoDominio = await _historicoTreinoRepositorio
            .ObterAsync(historicoAtualizar.ID);

        if (historicoDominio == null)
            throw new Exception("Histórico de treino não encontrado!");

        historicoDominio.SeriesFeitas = historicoAtualizar.SeriesFeitas;
        historicoDominio.CargaKg = historicoAtualizar.CargaKg;
        historicoDominio.RepeticoesFeitas = historicoAtualizar.RepeticoesFeitas;

        await _historicoTreinoRepositorio.AtualizarAsync(historicoDominio);
    }
    public async Task DeletarAsync(int historicoTreinoID)
    {
        var historicoTreinoDominio = await _historicoTreinoRepositorio.ObterAsync(historicoTreinoID);

        if (historicoTreinoDominio == null)
            throw new Exception("Histórico de treino não encontrado!");

        await _historicoTreinoRepositorio.DeleteAsync(historicoTreinoDominio);
    }
    public async Task<int> ObterQuantidadeTreinosSemanaAsync()
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync();
        var inicioSemana = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        var treinos = historicos.Where(x => x.DataExecucao >= inicioSemana).GroupBy(x => new
        {
            x.DataExecucao.Date,
            x.FichaTreinoID
        });
        return treinos.Count();
    }
    public async Task<decimal> ObterVolumeUltimoTreinoAsync()
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync();

        var ultimoTreino = historicos.OrderByDescending(x => x.DataExecucao).FirstOrDefault();

        if (ultimoTreino == null)
            return 0;

        return await _historicoTreinoRepositorio.ObterVolumeUltimoTreinoAsync(ultimoTreino.DataExecucao, ultimoTreino.FichaTreinoID);
    }
    public async Task<HistoricoTreinoResposta> ObterUltimoTreinoAsync()
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync();

        var ultimoTreino = historicos.OrderByDescending(x => x.DataExecucao).FirstOrDefault();

        if (ultimoTreino == null)
            return null;

        return new HistoricoTreinoResposta
        {
            ID = ultimoTreino.ID,
            NomeFicha = ultimoTreino.FichaTreino.Nome,
            NomeExercicio = ultimoTreino.Exercicio.Nome,
            CargaKg = ultimoTreino.CargaKg,
            RepeticoesFeitas = ultimoTreino.RepeticoesFeitas,
            SeriesFeitas = ultimoTreino.SeriesFeitas,
            DataExecucao = ultimoTreino.DataExecucao
        };
    }
    public async Task<int> ObterQuantidadeExerciciosUltimoTreinoAsync()
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync();
        var ultimoTreino = historicos.OrderByDescending(x => x.DataExecucao).FirstOrDefault();

        if (ultimoTreino == null)
            return 0;

        return historicos.Count(x => x.DataExecucao.Date == ultimoTreino.DataExecucao.Date && x.FichaTreinoID == ultimoTreino.FichaTreinoID);
    }
    public async Task<decimal> ObterVolumeSemanaAsync()
    {
        var inicioSemana = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        return await _historicoTreinoRepositorio.ObterVolumeSemanaAsync(inicioSemana);
    }
    public async Task<int> ObterTempoDesdeUltimoTreinoAsync()
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync();

        var ultimoTreino = historicos.OrderByDescending(x => x.DataExecucao).FirstOrDefault();

        if (ultimoTreino == null)
            return 0;

        return (DateTime.Today - ultimoTreino.DataExecucao.Date).Days;
    }
}