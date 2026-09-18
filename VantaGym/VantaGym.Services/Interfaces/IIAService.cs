namespace VantaGym.Services.Interfaces;

public interface IIAService
{
    Task<string> EnviarPerguntaIA(string pergunta, string especializacao);
}