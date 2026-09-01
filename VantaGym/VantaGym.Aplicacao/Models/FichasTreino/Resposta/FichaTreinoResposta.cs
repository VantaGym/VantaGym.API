using VantaGym.Api.Models.Resposta;

namespace VantaGym.Aplicacao.Models.Resposta
{
    public class FichaTreinoResposta
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public List<ExercicioResposta> Exercicios { get; set; } = new List<ExercicioResposta>();
    }
}