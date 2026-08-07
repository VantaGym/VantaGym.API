using VantaGym.Dominio.Enumeradores;

namespace VantaGym.Dominio.Entidades;

public class Exercicio
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public int Series { get; set; }
    public int Repeticoes { get; set; }
    public GrupoMuscular GrupoMuscular { get; set; }
}