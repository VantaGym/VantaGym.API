using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Aplicacao.DTOs.Resposta;

namespace VantaGym.Aplicacao
{
    public interface IHistoricoTreinoAplicacao
    {
        Task<int> CriarAsync(HistoricoTreinoCriar historicoTreinoCriar);
        Task AtualizarAsync(HistoricoTreinoAtualizar historicoTreinoAtualizar);
        Task DeletarAsync(int historicoTreinoID);
        Task<IEnumerable<HistoricoTreinoResposta>> ListarAsync();
        Task<HistoricoTreinoResposta> ObterAsync(int historicoTreinoID);
    }
}