namespace VantaGym.Dominio.Entidades;

public class Usuario
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public bool Ativo { get; set; } = true;

    public List<FichaTreino> FichasTreino { get; set; } = new List<FichaTreino>();
    public List<HistoricoTreino> HistoricoTreino { get; set; } = new List<HistoricoTreino>();
}