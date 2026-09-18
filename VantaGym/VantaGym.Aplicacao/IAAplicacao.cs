using VantaGym.Aplicacao.Interfaces;
using VantaGym.Aplicacao.DTOs;
using VantaGym.Services.Interfaces;
using Newtonsoft.Json;

namespace VantaGym.Aplicacao;

public class IAAplicacao : IIAAplicacao
{
    private readonly IIAService _iaService;

    public IAAplicacao(IIAService iaService)
    {
        _iaService = iaService;
    }

    public async Task<TreinoIAResposta> GerarTreinoAsync(TreinoIACriar treino)
    {
        string especializacao = @"Você é um assistente especializado em montagem de fichas de treino de academia.
        Sua função é criar sugestões de treino simples, organizadas e adequadas às informações fornecidas pelo usuário.
        Não recomende medicamentos, suplementos ou substâncias.
        Não faça diagnósticos médicos.
        Priorize exercícios comuns de academia e uma quantidade de exercícios compatível com o tempo disponível.";

        string pergunta = $@"Monte fichas de treino utilizando as seguintes informações:
        Idade: {treino.Idade} anos
        Peso: {treino.Peso} kg
        Objetivo: {treino.Objetivo}
        Nível de experiência: {treino.NivelExperiencia}
        Dias disponíveis por semana: {treino.DiasPorSemana}
        Tempo disponível por treino: {treino.MinutosPorTreino} minutos
        Restrições ou preferências: {treino.Restricoes}

        Crie uma ficha de treino para cada dia disponível.

        Para cada ficha informe:
        - Nome da ficha
        - Exercícios

        Para cada exercício informe:
        - Nome
        - Grupo muscular
        - Número de séries
        - Número de repetições

        Utilize apenas estes grupos musculares:
        Peito, Costas, Ombros, Biceps, Triceps, Pernas e Abdomen.

        Séries e repetições devem ser números inteiros.
        Não utilize intervalos como 8 a 12 repetições. Escolha apenas um número.

        Retorne somente um JSON válido seguindo exatamente esta estrutura:
        {{
            ""fichas"": [
                {{
                    ""nome"": ""Superior 1"",
                    ""exercicios"": [
                        {{
                            ""nome"": ""Supino Reto"",
                            ""grupoMuscular"": ""Peito"",
                            ""series"": 4,
                            ""repeticoes"": 10
                        }}
                    ]
                }}
            ]
        }}

        Não escreva explicações antes ou depois do JSON.
        Não utilize Markdown.
        Não utilize ```json.
        Respeite as restrições informadas pelo usuário.";

        string resposta = await _iaService.EnviarPerguntaIA(pergunta, especializacao);
        TreinoIAResposta? treinoResposta = JsonConvert.DeserializeObject<TreinoIAResposta>(resposta);
        if (treinoResposta == null)
            throw new Exception("Erro ao gerar treino.");
        return treinoResposta;
    }
}