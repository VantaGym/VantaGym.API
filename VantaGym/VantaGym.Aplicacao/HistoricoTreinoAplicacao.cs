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
}