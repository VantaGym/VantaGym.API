using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Aplicacao.DTOs.Resposta;

namespace VantaGym.Aplicacao
{
    public interface IFichaTreinoAplicacao
    {
        Task<int> CriarAsync(FichaTreinoCriar fichaTreinoCriar, int usuarioID);
        Task AtualizarAsync(FichaTreinoAtualizar fichaTreinoAtualizar, int usuarioID);
        Task DeletarAsync(int fichaTreinoID, int usuarioID);
        Task<IEnumerable<FichaTreinoListar>> ListarAsync(int usuarioID);
        Task<FichaTreinoResposta> ObterAsync(int fichaTreinoID, int usuarioID);
    }
}