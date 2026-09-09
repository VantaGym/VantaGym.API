using VantaGym.Aplicacao.DTOs.Requisicao;
using VantaGym.Api.DTOs.Resposta;
using VantaGym.Dominio.Entidades;
using VantaGym.Aplicacao.DTOs.Resposta;

namespace VantaGym.Aplicacao;

public class FichaTreinoAplicacao : IFichaTreinoAplicacao
{
    readonly IFichaTreinoRepositorio _fichaTreinoRepositorio;
    public FichaTreinoAplicacao(IFichaTreinoRepositorio fichaTreinoRepositorio)
    {
        _fichaTreinoRepositorio = fichaTreinoRepositorio;
    }
    public async Task<int> CriarAsync(FichaTreinoCriar fichaTreinoCriar)
    {
        if (fichaTreinoCriar == null)
            throw new Exception("A ficha de treino não pode ser vazia!");

        var fichaTreino = new FichaTreino
        {
            Nome = fichaTreinoCriar.Nome,
            UsuarioID = fichaTreinoCriar.UsuarioID,
        };

        return await _fichaTreinoRepositorio.SalvarAsync(fichaTreino);
    }
    public async Task<FichaTreinoResposta> ObterAsync(int fichaTreinoID)
    {
        var fichaTreino = await _fichaTreinoRepositorio.ObterAsync(fichaTreinoID);

        if (fichaTreino == null)
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
    public async Task<IEnumerable<FichaTreinoListar>> ListarAsync()
    {
        var fichas = await _fichaTreinoRepositorio.ListarAsync();

        var fichasResposta = fichas.Select(fichaTreino => new FichaTreinoListar
        {
            ID = fichaTreino.ID,
            Nome = fichaTreino.Nome
        }).ToList();

        return fichasResposta;
    }
    public async Task AtualizarAsync(FichaTreinoAtualizar fichaTreinoAtualizar)
    {
        var fichaTreinoDominio = await _fichaTreinoRepositorio.ObterAsync(fichaTreinoAtualizar.ID);

        if (fichaTreinoDominio == null)
            throw new Exception("Ficha de treino não encontrada!");

        fichaTreinoDominio.Nome = fichaTreinoAtualizar.Nome;

        await _fichaTreinoRepositorio.AtualizarAsync(fichaTreinoDominio);
    }
    public async Task DeletarAsync(int fichaTreinoID)
    {
        var fichaTreinoDominio = await _fichaTreinoRepositorio.ObterAsync(fichaTreinoID);

        if (fichaTreinoDominio == null)
            throw new Exception("Ficha de treino não encontrada!");

        await _fichaTreinoRepositorio.DeleteAsync(fichaTreinoDominio);
    }
}