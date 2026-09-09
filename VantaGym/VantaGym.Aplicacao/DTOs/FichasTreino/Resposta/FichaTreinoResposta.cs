using VantaGym.Api.DTOs.Resposta;

namespace VantaGym.Aplicacao.DTOs.Resposta
{
    public class FichaTreinoResposta
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int UsuarioID { get; set; }
        public List<ExercicioResposta> Exercicios { get; set; } = new List<ExercicioResposta>();
    }
}