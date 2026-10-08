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
    public async Task<int> CriarAsync(HistoricoTreinoCriar historicoCriar, int usuarioID)
    {
        if (historicoCriar == null)
            throw new Exception("Histórico não pode ser vazio!");

        if (historicoCriar.SeriesFeitas <= 0)
            throw new Exception("A quantidade de séries não pode ser nula ou negativa!");

        if (historicoCriar.RepeticoesFeitas <= 0)
            throw new Exception("A quantidade de repetições não pode ser nula ou negativa!");

        if (historicoCriar.CargaKg <= 0)
            throw new Exception("A carga não pode ser nula ou negativa!");

        var exercicio = await _exercicioRepositorio.ObterAsync(historicoCriar.ExercicioID);

        if (exercicio == null)
            throw new Exception("Exercício não encontrado!");

        if (exercicio.FichaTreino.UsuarioID != usuarioID)
            throw new Exception("Exercício não encontrado!");

        var historico = new HistoricoTreino
        {
            SeriesFeitas = historicoCriar.SeriesFeitas.Value,
            CargaKg = historicoCriar.CargaKg.Value,
            RepeticoesFeitas = historicoCriar.RepeticoesFeitas.Value,
            DataExecucao = historicoCriar.DataExecucao,
            ExercicioID = exercicio.ID,
            FichaTreinoID = exercicio.FichaTreinoID
        };

        return await _historicoTreinoRepositorio.SalvarAsync(historico);
    }
    public async Task<HistoricoTreinoResposta> ObterAsync(int historicoTreinoID, int usuarioID)
    {
        var historicoTreino = await _historicoTreinoRepositorio.ObterAsync(historicoTreinoID);

        if (historicoTreino == null)
            throw new Exception("Histórico de treino não encontrado!");

        if (historicoTreino.FichaTreino.UsuarioID != usuarioID)
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
    public async Task<IEnumerable<HistoricoTreinoResposta>> ListarAsync(int usuarioID)
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync(usuarioID);

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
    public async Task AtualizarAsync(HistoricoTreinoAtualizar historicoAtualizar, int usuarioID)
    {
        var historicoDominio = await _historicoTreinoRepositorio
            .ObterAsync(historicoAtualizar.ID);

        if (historicoDominio == null)
            throw new Exception("Histórico de treino não encontrado!");

        if (historicoDominio.FichaTreino.UsuarioID != usuarioID)
            throw new Exception("Histórico de treino não encontrado!");

        historicoDominio.SeriesFeitas = historicoAtualizar.SeriesFeitas;
        historicoDominio.CargaKg = historicoAtualizar.CargaKg;
        historicoDominio.RepeticoesFeitas = historicoAtualizar.RepeticoesFeitas;

        await _historicoTreinoRepositorio.AtualizarAsync(historicoDominio);
    }
    public async Task DeletarAsync(int historicoTreinoID, int usuarioID)
    {
        var historicoTreinoDominio = await _historicoTreinoRepositorio.ObterAsync(historicoTreinoID);

        if (historicoTreinoDominio == null)
            throw new Exception("Histórico de treino não encontrado!");

        if (historicoTreinoDominio.FichaTreino.UsuarioID != usuarioID)
            throw new Exception("Histórico de treino não encontrado!");

        await _historicoTreinoRepositorio.DeleteAsync(historicoTreinoDominio);
    }
    public async Task<int> ObterQuantidadeTreinosSemanaAsync(int usuarioID)
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync(usuarioID);
        var inicioSemana = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        var treinos = historicos.Where(x => x.DataExecucao >= inicioSemana).GroupBy(x => x.DataExecucao);
        return treinos.Count();
    }
    public async Task<decimal> ObterVolumeUltimoTreinoAsync(int usuarioID)
    {
        return await _historicoTreinoRepositorio.ObterVolumeUltimoTreinoAsync(usuarioID);
    }
    public async Task<HistoricoTreinoResposta> ObterUltimoTreinoAsync(int usuarioID)
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync(usuarioID);

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
    public async Task<int> ObterQuantidadeExerciciosUltimoTreinoAsync(int usuarioID)
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync(usuarioID);
        var ultimoTreino = historicos.OrderByDescending(x => x.DataExecucao).FirstOrDefault();

        if (ultimoTreino == null)
            return 0;

        return historicos.Count(x => x.DataExecucao == ultimoTreino.DataExecucao);
    }
    public async Task<decimal> ObterVolumeSemanaAsync(int usuarioID)
    {
        var inicioSemana = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        return await _historicoTreinoRepositorio.ObterVolumeSemanaAsync(inicioSemana, usuarioID);
    }
    public async Task<int> ObterTempoDesdeUltimoTreinoAsync(int usuarioID)
    {
        var historicos = await _historicoTreinoRepositorio.ListarAsync(usuarioID);

        var ultimoTreino = historicos.OrderByDescending(x => x.DataExecucao).FirstOrDefault();

        if (ultimoTreino == null)
            return 0;

        return (DateTime.Today - ultimoTreino.DataExecucao.Date).Days;
    }
}