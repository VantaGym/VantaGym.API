using VantaGym.Dominio.Enumeradores;

namespace VantaGym.Aplicacao.DTOs.Requisicao
{
    public class ExercicioAtualizar
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public GrupoMuscular GrupoMuscular { get; set; }
        public int FichaTreinoID { get; set; }
    }
}