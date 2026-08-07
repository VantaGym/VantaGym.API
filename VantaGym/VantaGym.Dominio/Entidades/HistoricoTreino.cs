namespace VantaGym.Dominio.Entidades;
public class HistoricoTreino
{
    public int Id { get; set; }
    public decimal CargaKg { get; set; }
    public int RepeticoesFeitas { get; set; }
    
    public FichaTreino FichaTreino { get; set; }
    public int FichaTreinoID { get; set; }
    
    public Usuario Usuario { get; set; }
    public int UsuarioID { get; set; }
    
    public Exercicio Exercicio { get; set; } 
    public int ExercicioID { get; set; }
    
    public DateTime DataExecucao { get; set; }
}