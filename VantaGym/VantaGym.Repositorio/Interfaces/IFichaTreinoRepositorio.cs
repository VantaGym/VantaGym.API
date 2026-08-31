using VantaGym.Dominio.Entidades;
public interface IFichaTreinoRepositorio
{
    Task<int> SalvarAsync(FichaTreino fichaTreino);
    Task DeleteAsync(FichaTreino fichaTreino);
    Task AtualizarAsync(FichaTreino fichaTreino);
    Task<FichaTreino> ObterAsync(int fichaTreinoId);
    Task <IEnumerable<FichaTreino>> ListarAsync();
}