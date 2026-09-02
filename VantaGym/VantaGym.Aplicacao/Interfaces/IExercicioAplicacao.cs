using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;

namespace VantaGym.Aplicacao
{
    public interface IExercicioAplicacao
    {
        Task<int> CriarAsync(ExercicioCriar exercicioCriar);
        Task AtualizarAsync(ExercicioAtualizar exercicioAtualizar);
        Task DeletarAsync(int exercicioID);
        Task<IEnumerable<ExercicioResposta>> ListarAsync();
        Task<ExercicioResposta> ObterAsync(int usuarioID);
    }
}