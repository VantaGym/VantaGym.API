using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;
using VantaGym.Dominio.Entidades;
using VantaGym.Aplicacao.DTOs.Resposta;

namespace VantaGym.Aplicacao;

public class FichaTreinoAplicacao : IFichaTreinoAplicacao
{
    readonly IFichaTreinoRepositorio _fichaTreinoRepositorio;
    readonly IExercicioRepositorio _exercicioRepositorio;
    public FichaTreinoAplicacao(IFichaTreinoRepositorio fichaTreinoRepositorio, IExercicioRepositorio exercicioRepositorio)
    {
        _fichaTreinoRepositorio = fichaTreinoRepositorio;
        _exercicioRepositorio = exercicioRepositorio;
    }
    public async Task<int> CriarAsync(FichaTreinoCriar fichaTreinoCriar, int usuarioID)
    {
        if (fichaTreinoCriar == null)
            throw new Exception("A ficha de treino não pode ser vazia!");

        var fichaTreino = new FichaTreino
        {
            Nome = fichaTreinoCriar.Nome,
            UsuarioID = usuarioID
        };

        return await _fichaTreinoRepositorio.SalvarAsync(fichaTreino);
    }
    public async Task<FichaTreinoResposta> ObterAsync(int fichaTreinoID, int usuarioID)
    {
        var fichaTreino = await _fichaTreinoRepositorio.ObterAsync(fichaTreinoID);

        if (fichaTreino == null)
            throw new Exception("Ficha de treino não encontrada!");

        if (fichaTreino.UsuarioID != usuarioID)
            throw new Exception("Ficha de treino não encontrada!");

        var fichaTreinoResposta = new FichaTreinoResposta
        {
            ID = fichaTreino.ID,
            Nome = fichaTreino.Nome,
            UsuarioID = fichaTreino.UsuarioID,
            Exercicios = fichaTreino.Exercicios.Select(exercicio => new ExercicioResposta
            {
                ID = exercicio.ID,
                Nome = exercicio.Nome,
                Series = exercicio.Series,
                Repeticoes = exercicio.Repeticoes,
                GrupoMuscular = exercicio.GrupoMuscular
            }).ToList()
        };
        return fichaTreinoResposta;
    }
    public async Task<IEnumerable<FichaTreinoListar>> ListarAsync(int usuarioID)
    {
        var fichas = await _fichaTreinoRepositorio.ListarAsync(usuarioID);
        var fichasResposta = new List<FichaTreinoListar>();

        foreach (var fichaTreino in fichas)
        {
            var exercicios = await _exercicioRepositorio.ListarPorFichaAsync(fichaTreino.ID);
            var gruposMusculares = exercicios
                .Select(x => x.GrupoMuscular.ToString())
                .Distinct()
                .ToList();
            fichasResposta.Add(new FichaTreinoListar
            {
                ID = fichaTreino.ID,
                Nome = fichaTreino.Nome,
                GruposMusculares = gruposMusculares
            });
        }
        return fichasResposta;
    }
    public async Task AtualizarAsync(FichaTreinoAtualizar fichaTreinoAtualizar, int usuarioID)
    {
        var fichaTreinoDominio = await _fichaTreinoRepositorio.ObterAsync(fichaTreinoAtualizar.ID);

        if (fichaTreinoDominio == null)
            throw new Exception("Ficha de treino não encontrada!");

        if (fichaTreinoDominio.UsuarioID != usuarioID)
            throw new Exception("Ficha de treino não encontrada!");

        if(fichaTreinoDominio.Nome == "")
            throw new Exception("O nome da ficha de treino não pode ser vazio!");

        fichaTreinoDominio.Nome = fichaTreinoAtualizar.Nome;

        await _fichaTreinoRepositorio.AtualizarAsync(fichaTreinoDominio);
    }
    public async Task DeletarAsync(int fichaTreinoID, int usuarioID)
    {
        var fichaTreinoDominio = await _fichaTreinoRepositorio.ObterAsync(fichaTreinoID);

        if (fichaTreinoDominio == null)
            throw new Exception("Ficha de treino não encontrada!");

        if (fichaTreinoDominio.UsuarioID != usuarioID)
            throw new Exception("Ficha de treino não encontrada!");

        await _fichaTreinoRepositorio.DeleteAsync(fichaTreinoDominio);
    }
}