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

        Crie exatamente uma ficha para cada dia disponível.

        Todas as fichas devem ser objetos dentro do mesmo array ""fichas"".

        Nunca transforme uma ficha em texto ou string.

        Exemplo com 3 dias:
        {{
            ""fichas"": [
                {{
                    ""nome"": ""Dia 1"",
                    ""exercicios"": []
                }},
                {{
                    ""nome"": ""Dia 2"",
                    ""exercicios"": []
                }},
                {{
                    ""nome"": ""Dia 3"",
                    ""exercicios"": []
                }}
            ]
        }}

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

        

        Retorne exatamente UM objeto JSON.

        Gere todas as fichas solicitadas dentro do mesmo array ""fichas"".

        Cada ficha deve ser um objeto independente dentro do array.

        Para cada nova ficha, abra um novo objeto com {{ e feche com }} antes de iniciar a próxima ficha.

        Nunca escreva ""nome"" ou ""exercicios"" diretamente dentro do array ""fichas"".
        ""nome"" e ""exercicios"" pertencem exclusivamente ao objeto de cada ficha.

        Exemplo de duas fichas:
        {{
            ""fichas"": [
                {{
                    ""nome"": ""Dia 1"",
                    ""exercicios"": []
                }},
                {{
                    ""nome"": ""Dia 2"",
                    ""exercicios"": []
                }}
            ]
        }}

        O JSON DEVE começar com {{ e terminar com }}.

        A estrutura obrigatória é:
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

        IMPORTANTE:
        - O resultado NÃO pode ser um array.
        - NÃO coloque o objeto dentro de [].
        - O primeiro nível do JSON deve ser um objeto contendo ""fichas"".
        - ""fichas"" deve ser sempre um array de objetos.
        - Cada item dentro de ""fichas"" deve ser um objeto, nunca uma string.
        - Não coloque aspas ao redor de objetos.
        - Não transforme nenhum objeto em texto.
        - Cada item de ""exercicios"" deve ser um objeto contendo ""nome"", ""grupoMuscular"", ""series"" e ""repeticoes"".
        - Não escreva explicações antes ou depois do JSON.
        - Não utilize Markdown.
        - Não utilize ```json.
        - Respeite as restrições informadas pelo usuário.";
        
        string resposta = await _iaService.EnviarPerguntaIA(pergunta, especializacao);
        TreinoIAResposta? treinoResposta = JsonConvert.DeserializeObject<TreinoIAResposta>(resposta);

        if (treinoResposta == null)
            throw new Exception("Erro ao gerar treino.");

        return treinoResposta;
    }
}