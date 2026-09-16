using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Dominio.Entidades;

namespace VantaGym.Aplicacao
{
    public interface IHistoricoTreinoAplicacao
    {
        Task<int> CriarAsync(HistoricoTreinoCriar historicoTreinoCriar, int usuarioID);
        Task AtualizarAsync(HistoricoTreinoAtualizar historicoTreinoAtualizar, int usuarioID);
        Task DeletarAsync(int historicoTreinoID, int usuarioID);
        Task<IEnumerable<HistoricoTreinoResposta>> ListarAsync(int usuarioID);
        Task<HistoricoTreinoResposta> ObterAsync(int historicoTreinoID, int usuarioID);
        Task<int> ObterQuantidadeTreinosSemanaAsync(int usuarioID);
        Task<decimal> ObterVolumeUltimoTreinoAsync(int usuarioID);
        Task<HistoricoTreinoResposta> ObterUltimoTreinoAsync(int usuarioID);
        Task<int> ObterQuantidadeExerciciosUltimoTreinoAsync(int usuarioID);
        Task<decimal> ObterVolumeSemanaAsync(int usuarioID);
        Task<int> ObterTempoDesdeUltimoTreinoAsync(int usuarioID);
    }
}