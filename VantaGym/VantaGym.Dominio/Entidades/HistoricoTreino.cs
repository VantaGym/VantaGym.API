namespace VantaGym.Dominio.Entidades;

public class HistoricoTreino
{
    public int ID { get; set; }
    public decimal CargaKg { get; set; }
    public int RepeticoesFeitas { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime DataExecucao { get; set; }

    public FichaTreino FichaTreino { get; set; }
    public int FichaTreinoID { get; set; }

    public Exercicio Usuario { get; set; }
    public int UsuarioID { get; set; }

    public Exercicio Exercicio { get; set; }
    public int ExercicioID { get; set; }

}