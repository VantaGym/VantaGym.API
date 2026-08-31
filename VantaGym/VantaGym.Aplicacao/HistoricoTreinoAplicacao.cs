using VantaGym.Dominio.Entidades;

namespace VantaGym.Aplicacao;

public class HistoricoTreinoAplicacao
{
    readonly IHistoricoTreinoRepositorio _historicoTreinoRepositorio;
    public HistoricoTreinoAplicacao(IHistoricoTreinoRepositorio historicoTreinoRepositorio)
    {
        _historicoTreinoRepositorio = historicoTreinoRepositorio;
    }
    public async Task<int> CriarAsync(HistoricoTreino  historicoTreino)
    {
        if (historicoTreino == null)
            throw new Exception("O histórico de treino não pode ser vazio!");

        return await _historicoTreinoRepositorio.SalvarAsync(historicoTreino);
    }
    public async Task<HistoricoTreino> ObterAsync(int historicoTreinoID)
    {
        var historicoTreinoDominio = await _historicoTreinoRepositorio.ObterAsync(historicoTreinoID);

        if (historicoTreinoDominio == null)
            throw new Exception("Histórico de treino não encontrado!");

        return historicoTreinoDominio;
    }
    public async Task<IEnumerable<HistoricoTreino>> ListarAsync()
    {
        return await _historicoTreinoRepositorio.ListarAsync();
    }
    public async Task AtualizarAsync(HistoricoTreino historicoTreino)
    {
        var historicoTreinoDominio = await _historicoTreinoRepositorio.ObterAsync(historicoTreino.ID);

        if (historicoTreinoDominio == null)
            throw new Exception("Histórico de treino não encontrado!");

        historicoTreinoDominio.ExercicioID = historicoTreino.ExercicioID;
        historicoTreinoDominio.DataExecucao = historicoTreino.DataExecucao;
        historicoTreino.CargaKg = historicoTreino.CargaKg;
        historicoTreino.RepeticoesFeitas = historicoTreino.RepeticoesFeitas;

        await _historicoTreinoRepositorio.AtualizarAsync(historicoTreinoDominio);
    }
    public async Task DeletarAsync(int historicoTreinoID)
    {
        var historicoTreinoDominio = await _historicoTreinoRepositorio.ObterAsync(historicoTreinoID);

        if (historicoTreinoDominio == null)
            throw new Exception("Histórico de treino não encontrado!");

        await _historicoTreinoRepositorio.DeleteAsync(historicoTreinoDominio);
    }
}