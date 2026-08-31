using VantaGym.Dominio.Entidades;

namespace VantaGym.Aplicacao;

public class ExercicioAplicacao
{
    readonly IExercicioRepositorio _exercicioRepositorio;
    public ExercicioAplicacao(IExercicioRepositorio exercicioRepositorio)
    {
        _exercicioRepositorio = exercicioRepositorio;
    }
    public async Task<int> CriarAsync(Exercicio exercicio)
    {
        if (exercicio == null)
            throw new Exception("Exercício não pode ser vazio!");

        return await _exercicioRepositorio.SalvarAsync(exercicio);
    }
    public async Task<Exercicio> ObterAsync(int exercicioID)
    {
        var exercicioDominio = await _exercicioRepositorio.ObterAsync(exercicioID);

        if (exercicioDominio == null)
            throw new Exception("Exercício não encontrado!");

        return exercicioDominio;
    }
    public async Task<IEnumerable<Exercicio>> ListarAsync()
    {
        return await _exercicioRepositorio.ListarAsync();
    }
    public async Task AtualizarAsync(Exercicio exercicio)
    {
        var exercicioDominio = await _exercicioRepositorio.ObterAsync(exercicio.ID);

        if (exercicioDominio == null)
            throw new Exception("Exercício não encontrado!");

        exercicioDominio.Nome = exercicio.Nome;
        exercicioDominio.GrupoMuscular = exercicio.GrupoMuscular;
        exercicioDominio.Series = exercicio.Series;
        exercicioDominio.Repeticoes = exercicio.Repeticoes;
        exercicioDominio.FichaTreinoID = exercicio.FichaTreinoID;

        await _exercicioRepositorio.AtualizarAsync(exercicioDominio);
    }
    public async Task DeletarAsync(int exercicioID)
    {
        var exercicioDominio = await _exercicioRepositorio.ObterAsync(exercicioID);

        if (exercicioDominio == null)
            throw new Exception("Exercício não encontrado!");

        await _exercicioRepositorio.DeleteAsync(exercicioDominio);
    }
}