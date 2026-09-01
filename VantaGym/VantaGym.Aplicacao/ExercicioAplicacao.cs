using VantaGym.Dominio.Entidades;
using VantaGym.Aplicacao.Models.Requisicao;
using VantaGym.Api.Models.Resposta;

namespace VantaGym.Aplicacao;

public class ExercicioAplicacao : IExercicioAplicacao
{
    readonly IExercicioRepositorio _exercicioRepositorio;
    public ExercicioAplicacao(IExercicioRepositorio exercicioRepositorio)
    {
        _exercicioRepositorio = exercicioRepositorio;
    }
    public async Task<int> CriarAsync(ExercicioCriar exercicioCriar)
    {
        if (exercicioCriar == null)
            throw new Exception("Exercício não pode ser vazio!");

        var exercicio = new Exercicio
        {
            Nome = exercicioCriar.Nome,
            Series = exercicioCriar.Series,
            Repeticoes = exercicioCriar.Repeticoes,
            GrupoMuscular = exercicioCriar.GrupoMuscular,
            FichaTreinoID = exercicioCriar.FichaTreinoID
        };

        return await _exercicioRepositorio.SalvarAsync(exercicio);
    }
    public async Task<ExercicioResposta> ObterAsync(int exercicioID)
    {
        var exercicioDominio = await _exercicioRepositorio.ObterAsync(exercicioID);

        if (exercicioDominio == null)
            throw new Exception("Exercício não encontrado!");

        var exercicioResposta = new ExercicioResposta()
        {
            ID = exercicioDominio.ID,
            Nome = exercicioDominio.Nome,
            Series = exercicioDominio.Series,
            Repeticoes = exercicioDominio.Repeticoes,
            GrupoMuscular = exercicioDominio.GrupoMuscular,
            FichaTreinoNome = exercicioDominio.FichaTreino.Nome
        };

        return exercicioResposta;
    }
    public async Task<IEnumerable<ExercicioResposta>> ListarAsync()
    {
        var exercicios = await _exercicioRepositorio.ListarAsync();

        var exerciciosResposta = exercicios.Select(exercicio => new ExercicioResposta
        {
            ID = exercicio.ID,
            Nome = exercicio.Nome,
            Series = exercicio.Series,
            Repeticoes = exercicio.Repeticoes,
            GrupoMuscular = exercicio.GrupoMuscular,
            FichaTreinoNome = exercicio.FichaTreino.Nome
        }).ToList();

        return exerciciosResposta;
    }
    public async Task AtualizarAsync(ExercicioAtualizar exercicioAtualizar)
    {
        var exercicioDominio = await _exercicioRepositorio.ObterAsync(exercicioAtualizar.ID);

        if (exercicioDominio == null)
            throw new Exception("Exercício não encontrado!");

        exercicioDominio.Nome = exercicioAtualizar.Nome;
        exercicioDominio.GrupoMuscular = exercicioAtualizar.GrupoMuscular;
        exercicioDominio.Series = exercicioAtualizar.Series;
        exercicioDominio.Repeticoes = exercicioAtualizar.Repeticoes;
        exercicioDominio.FichaTreinoID = exercicioAtualizar.FichaTreinoID;

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