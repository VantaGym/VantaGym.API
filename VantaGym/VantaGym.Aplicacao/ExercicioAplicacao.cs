using VantaGym.Dominio.Entidades;
using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;
using VantaGym.Dominio.Enumeradores;

namespace VantaGym.Aplicacao;

public class ExercicioAplicacao : IExercicioAplicacao
{
    readonly IExercicioRepositorio _exercicioRepositorio;
    readonly IFichaTreinoRepositorio _fichaTreinoRepositorio;
    public ExercicioAplicacao(IExercicioRepositorio exercicioRepositorio, IFichaTreinoRepositorio fichaTreinoRepositorio)
    {
        _exercicioRepositorio = exercicioRepositorio;
        _fichaTreinoRepositorio = fichaTreinoRepositorio;
    }
    public async Task<int> CriarAsync(ExercicioCriar exercicioCriar, int usuarioID)
    {
        if (exercicioCriar == null)
            throw new Exception("Exercício não pode ser vazio!");

        if (exercicioCriar.Series <= 0)
            throw new Exception("A quantidade de séries não pode ser nula ou negativa!");

        if (exercicioCriar.Repeticoes <= 0)
            throw new Exception("A quantidade de repetições não pode ser nula ou negativa!");

        var fichaTreino = await _fichaTreinoRepositorio.ObterAsync(exercicioCriar.FichaTreinoID);

        if (fichaTreino == null || fichaTreino.UsuarioID != usuarioID)
            throw new Exception("Ficha de treino não encontrada!");

        var exercicio = new Exercicio
        {
            Nome = exercicioCriar.Nome,
            Series = exercicioCriar.Series.Value,
            Repeticoes = exercicioCriar.Repeticoes.Value,
            GrupoMuscular = exercicioCriar.GrupoMuscular,
            FichaTreinoID = exercicioCriar.FichaTreinoID
        };

        return await _exercicioRepositorio.SalvarAsync(exercicio);
    }
    public async Task<ExercicioResposta> ObterAsync(int exercicioID, int usuarioID)
    {
        var exercicioDominio = await _exercicioRepositorio.ObterAsync(exercicioID);

        if (exercicioDominio == null)
            throw new Exception("Exercício não encontrado!");

        if (exercicioDominio.FichaTreino.UsuarioID != usuarioID)
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
    public async Task<IEnumerable<ExercicioResposta>> ListarAsync(int usuarioID)
    {
        var exercicios = await _exercicioRepositorio.ListarAsync(usuarioID);

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
    public async Task AtualizarAsync(ExercicioAtualizar exercicioAtualizar, int usuarioID)
    {
        var exercicioDominio = await _exercicioRepositorio.ObterAsync(exercicioAtualizar.ID);

        if (exercicioDominio == null)
            throw new Exception("Exercício não encontrado!");

        if (exercicioDominio.FichaTreino.UsuarioID != usuarioID)
            throw new Exception("Exercício não encontrado!");

        if (exercicioAtualizar.Series <= 0)
            throw new Exception("A quantidade de séries não pode ser nula ou negativa!");

        if (exercicioAtualizar.Repeticoes <= 0)
            throw new Exception("A quantidade de repetições não pode ser nula ou negativa!");

        exercicioDominio.Nome = exercicioAtualizar.Nome;
        exercicioDominio.GrupoMuscular = exercicioAtualizar.GrupoMuscular;
        exercicioDominio.Series = exercicioAtualizar.Series.Value;
        exercicioDominio.Repeticoes = exercicioAtualizar.Repeticoes.Value;

        await _exercicioRepositorio.AtualizarAsync(exercicioDominio);
    }
    public async Task DeletarAsync(int exercicioID, int usuarioID)
    {
        var exercicioDominio = await _exercicioRepositorio.ObterAsync(exercicioID);

        if (exercicioDominio == null)
            throw new Exception("Exercício não encontrado!");

        if (exercicioDominio.FichaTreino.UsuarioID != usuarioID)
            throw new Exception("Exercício não encontrado!");

        await _exercicioRepositorio.DeleteAsync(exercicioDominio);
    }
    public List<GrupoMuscularResposta> ListarGruposMusculares()
    {
        return Enum.GetValues(typeof(GrupoMuscular)).Cast<GrupoMuscular>()
            .Select(grupo => new GrupoMuscularResposta
            {
                ID = (int)grupo,
                Nome = grupo.ToString()
            }).ToList();
    }
}