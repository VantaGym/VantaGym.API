using VantaGym.Dominio.Enumeradores;

namespace VantaGym.Aplicacao.DTOs.Requisicao
{
    public class ExercicioCriar
    {
        public string Nome { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public GrupoMuscular GrupoMuscular { get; set; }
        public int FichaTreinoID { get; set; }
    }
}