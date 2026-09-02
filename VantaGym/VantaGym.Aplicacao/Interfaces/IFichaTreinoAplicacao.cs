using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Aplicacao.DTOs.Resposta;

namespace VantaGym.Aplicacao
{
    public interface IFichaTreinoAplicacao
    {
        Task<int> CriarAsync(FichaTreinoCriar fichaTreinoCriar);
        Task AtualizarAsync(FichaTreinoAtualizar fichaTreinoAtualizar);
        Task DeletarAsync(int fichaTreinoID);
        Task<IEnumerable<FichaTreinoResposta>> ListarAsync();
        Task<FichaTreinoResposta> ObterAsync(int fichaTreinoID);
    }
}