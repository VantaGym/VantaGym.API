using VantaGym.Dominio.Entidades;
public interface IHistoricoTreinoRepositorio
{
    Task<int> SalvarAsync(HistoricoTreino historicoTreino);
    Task DeleteAsync(HistoricoTreino historicoTreino);
    Task AtualizarAsync(HistoricoTreino historicoTreino);
    Task<HistoricoTreino> ObterAsync(int historicoTreinoId);
    Task<IEnumerable<HistoricoTreino>> ListarAsync();
    Task<decimal> ObterVolumeUltimoTreinoAsync(DateTime dataExecucao, int fichaTreinoID);
}