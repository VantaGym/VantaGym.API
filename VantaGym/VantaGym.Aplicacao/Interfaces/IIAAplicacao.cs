namespace VantaGym.Aplicacao.Interfaces
{
    public interface IIAAplicacao
    {
        Task<TreinoIAResposta> GerarTreinoAsync(TreinoIACriar treino);
    }
}