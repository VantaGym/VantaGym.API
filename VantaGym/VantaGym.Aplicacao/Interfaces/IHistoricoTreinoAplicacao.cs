using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Dominio.Entidades;

namespace VantaGym.Aplicacao
{
    public interface IHistoricoTreinoAplicacao
    {
        Task<int> CriarAsync(HistoricoTreinoCriar historicoTreinoCriar);
        Task AtualizarAsync(HistoricoTreinoAtualizar historicoTreinoAtualizar);
        Task DeletarAsync(int historicoTreinoID);
        Task<IEnumerable<HistoricoTreinoResposta>> ListarAsync();
        Task<HistoricoTreinoResposta> ObterAsync(int historicoTreinoID);
        Task<int> ObterQuantidadeTreinosSemanaAsync();
        Task<decimal> ObterVolumeUltimoTreinoAsync();
        Task<HistoricoTreinoResposta> ObterUltimoTreinoAsync();
        Task<int> ObterQuantidadeExerciciosUltimoTreinoAsync();
        Task<decimal> ObterVolumeSemanaAsync();
        Task<int> ObterTempoDesdeUltimoTreinoAsync();
    }
}