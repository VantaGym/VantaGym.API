public abstract class BaseRepositorio
{
    protected readonly VantaGymContexto _contexto;

    protected BaseRepositorio(VantaGymContexto contexto)
    {
        _contexto = contexto;
    }
}