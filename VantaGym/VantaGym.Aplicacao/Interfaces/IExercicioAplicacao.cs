using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;

namespace VantaGym.Aplicacao
{
    public interface IExercicioAplicacao
    {
        Task<int> CriarAsync(ExercicioCriar exercicioCriar, int usuarioID);
        Task AtualizarAsync(ExercicioAtualizar exercicioAtualizar, int usuarioID);
        Task DeletarAsync(int exercicioID, int usuarioID);
        Task<IEnumerable<ExercicioResposta>> ListarAsync(int usuarioID);
        Task<ExercicioResposta> ObterAsync(int exercicioID, int usuarioID);
        List<GrupoMuscularResposta> ListarGruposMusculares();
    }
}