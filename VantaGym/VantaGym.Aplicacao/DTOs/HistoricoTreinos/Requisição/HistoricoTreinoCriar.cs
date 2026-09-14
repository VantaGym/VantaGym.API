using VantaGym.Dominio.Entidades;

namespace VantaGym.Aplicacao.DTOs.Requisicao
{
    public class HistoricoTreinoCriar
    {
        public int? SeriesFeitas { get; set; }
        public decimal? CargaKg { get; set; }
        public int? RepeticoesFeitas { get; set; }
        public int ExercicioID { get; set; }
    }
}