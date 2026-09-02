using VantaGym.Dominio.Enumeradores;

namespace VantaGym.Api.DTOs.Resposta
{
    public class ExercicioResposta
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public GrupoMuscular GrupoMuscular { get; set; }
        public string FichaTreinoNome { get; set; }
    }
}