using Microsoft.EntityFrameworkCore;
using VantaGym.Dominio.Entidades;
using Dapper;
using System.Data;

namespace DataAccess.Repositorio;

public class FichaTreinoRepositorio : BaseRepositorio, IFichaTreinoRepositorio
{
    public FichaTreinoRepositorio(VantaGymContexto contexto) : base(contexto)
    {
    }

    public async Task<int> SalvarAsync(FichaTreino fichaTreino)
    {
        await _contexto.FichaTreinos.AddAsync(fichaTreino);
        await _contexto.SaveChangesAsync();

        return fichaTreino.ID;
    }

    public async Task DeleteAsync(FichaTreino fichaTreino)
    {
        fichaTreino.Ativo = false;
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(FichaTreino fichaTreino)
    {
        _contexto.FichaTreinos.Update(fichaTreino);
        await _contexto.SaveChangesAsync();
    }

    public async Task<FichaTreino> ObterAsync(int fichaTreinoId)
    {
        var parametros = new { FichaTreinoID = fichaTreinoId };

        using var resultado = await _contexto.Database.GetDbConnection().QueryMultipleAsync(
                "sp_FichaTreino_Obter", parametros, commandType: CommandType.StoredProcedure);

        var fichaTreino = await resultado.ReadFirstOrDefaultAsync<FichaTreino>();
        if (fichaTreino != null)
        {
            fichaTreino.Exercicios = (await resultado.ReadAsync<Exercicio>()).ToList();
        }
        return fichaTreino;
    }

    public async Task<IEnumerable<FichaTreino>> ListarAsync()
    {
        return await _contexto.Database.GetDbConnection().QueryAsync<FichaTreino>(
            "sp_FichaTreino_Listar", commandType: CommandType.StoredProcedure);
    }
}