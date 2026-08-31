using VantaGym.Dominio.Entidades;

namespace VantaGym.Aplicacao;

public class FichaTreinoAplicacao
{
    readonly IFichaTreinoRepositorio _fichaTreinoRepositorio;
    public FichaTreinoAplicacao(IFichaTreinoRepositorio fichaTreinoRepositorio)
    {
        _fichaTreinoRepositorio = fichaTreinoRepositorio;
    }
    public async Task<int> CriarAsync(FichaTreino fichaTreino)
    {
        if (fichaTreino == null)
            throw new Exception("A ficha de treino não pode ser vazia!");

        return await _fichaTreinoRepositorio.SalvarAsync(fichaTreino);
    }
    public async Task<FichaTreino> ObterAsync(int fichaTreinoID)
    {
        var fichaTreinoDominio = await _fichaTreinoRepositorio.ObterAsync(fichaTreinoID);

        if (fichaTreinoDominio == null)
            throw new Exception("Ficha de treino não encontrada!");

        return fichaTreinoDominio;
    }
    public async Task<IEnumerable<FichaTreino>> ListarAsync()
    {
        return await _fichaTreinoRepositorio.ListarAsync();
    }
    public async Task AtualizarAsync(FichaTreino fichaTreino)
    {
        var fichaTreinoDominio = await _fichaTreinoRepositorio.ObterAsync(fichaTreino.ID);

        if (fichaTreinoDominio == null)
            throw new Exception("Ficha de treino não encontrada!");

        fichaTreinoDominio.Nome = fichaTreino.Nome;
        fichaTreino.Exercicios = fichaTreino.Exercicios;
        fichaTreino.UsuarioID = fichaTreino.UsuarioID;

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