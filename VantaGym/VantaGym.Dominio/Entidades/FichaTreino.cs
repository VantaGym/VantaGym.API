namespace VantaGym.Dominio.Entidades;

public class FichaTreino
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public bool Origem { get; set; }
    public bool Ativa { get; set; }

    public Usuario Usuario { get; set; }
    public int UsuarioID { get; set; }

    public List<Exercicio> Exercicios { get; set; } = new List<Exercicio>();
}