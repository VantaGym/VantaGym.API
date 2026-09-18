public class FichaTreinoIAResposta
{
    public string Nome { get; set; } = string.Empty;
    public List<ExercicioIAResposta> Exercicios { get; set; } = new();
}