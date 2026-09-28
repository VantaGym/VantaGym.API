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
        if (treino.Altura <= 0)
            throw new Exception("Altura deve ser maior que 0!");

        if (treino.Peso <= 0)
            throw new Exception("Peso deve ser maior que 0!");

        if (treino.DiasPorSemana < 1 || treino.DiasPorSemana > 7)
            throw new Exception("Quantidade de dias da semana inválida!");

        if (treino.Idade <= 0)
            throw new Exception("Idade deve ser maior que 0!");

        if (treino.MinutosPorTreino <= 0)
            throw new Exception("Quantidade de minutos por treino deve ser maior que 0!");
            
        string especializacao = @"Você é um assistente especializado em montagem de fichas de treino de academia.
        Sua função é criar sugestões de treino simples, organizadas e adequadas às informações fornecidas pelo usuário.
        Não recomende medicamentos, suplementos ou substâncias.
        Não faça diagnósticos médicos.
        Priorize exercícios comuns de academia e uma quantidade de exercícios compatível com o tempo disponível.";

        string pergunta = $@"Monte fichas de treino utilizando as seguintes informações:
        Idade: {treino.Idade} anos
        Peso: {treino.Peso} kg
        Altura: {treino.Altura} cm
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

        Para o grupo muscular, utilize somente os seguintes valores:
        1 = Peito
        2 = Costas
        3 = Pernas
        4 = Ombro
        5 = Bíceps
        6 = Tríceps
        7 = Abdômen
        8 = Glúteo
        9 = Panturrilha

        O campo grupoMuscular deve ser um número inteiro entre 1 e 9.

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
                            ""grupoMuscular"": 1,
                            ""series"": 4,
                            ""repeticoes"": 10
                        }}
                    ]
                }}
            ]
        }}

        O objeto principal deve obrigatoriamente possuir a propriedade ""fichas"".
        Não retorne diretamente uma lista ou array.
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