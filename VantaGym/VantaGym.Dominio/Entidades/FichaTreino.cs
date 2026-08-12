namespace VantaGym.Dominio.Entidades;

public class FichaTreino
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public bool Ativo { get; set; } = true;

    public Exercicio Usuario { get; set; }
    public int UsuarioID { get; set; }

    public List<Exercicio> Exercicios { get; set; } = new List<Exercicio>();
}