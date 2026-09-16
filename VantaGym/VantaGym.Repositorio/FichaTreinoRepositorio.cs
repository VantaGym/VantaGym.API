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
        try
        {
            await _contexto.FichaTreinos.AddAsync(fichaTreino);
            await _contexto.SaveChangesAsync();

            return fichaTreino.ID;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task DeleteAsync(FichaTreino fichaTreino)
    {
        fichaTreino.Ativo = false;
        _contexto.FichaTreinos.Update(fichaTreino);
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

    public async Task<IEnumerable<FichaTreino>> ListarAsync(int usuarioID)
    {
        var parametros = new { UsuarioID = usuarioID };
        return await _contexto.Database.GetDbConnection().QueryAsync<FichaTreino>(
            "sp_FichaTreino_Listar", parametros, commandType: CommandType.StoredProcedure);
    }
}