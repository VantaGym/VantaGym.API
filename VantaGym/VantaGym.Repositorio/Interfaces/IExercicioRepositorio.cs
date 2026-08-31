using VantaGym.Dominio.Entidades;
public interface IExercicioRepositorio
{
    Task<int> SalvarAsync(Exercicio exercicio);
    Task DeleteAsync(Exercicio exercicio);
    Task AtualizarAsync(Exercicio exercicio);
    Task<Exercicio> ObterAsync(int exercicioId);
    Task <IEnumerable<Exercicio>> ListarAsync();
}