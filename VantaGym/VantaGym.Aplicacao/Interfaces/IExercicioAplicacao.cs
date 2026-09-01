using VantaGym.Aplicacao.Models.Requisicao;
using VantaGym.Api.Models.Resposta;
using VantaGym.Api.Models.Requisicao;

namespace VantaGym.Aplicacao
{
    public interface IExercicioAplicacao
    {
        Task<int> CriarAsync(ExercicioCriar exercicioCriar);
        Task AtualizarAsync(ExercicioAtualizar exercicioAtualizar);
        Task DeletarAsync(int usuarioID);
        Task<IEnumerable<ExercicioResposta>> ListarAsync();
        Task<ExercicioResposta> ObterAsync(int usuarioID);
    }
}