using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using VantaGym.Services.Interfaces;

namespace VantaGym.Services;

public class IAService : IIAService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public IAService(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    public async Task<string> EnviarPerguntaIA(string pergunta, string especializacao)
    {
        string apiKey = _configuration["Groq:ApiKey"];
        string url = "https://api.groq.com/openai/v1/chat/completions";

        var payload = new
        {
            model = "openai/gpt-oss-20b",
            messages = new[]
            {
                new
                {
                    role = "system",
                    content = especializacao
                },
                new
                {
                    role = "user",
                    content = pergunta
                }
            },
            response_format = new
            {
                type = "json_schema",
                json_schema = new
                {
                    name = "treino_schema",
                    strict = true,

                    schema = new
                    {
                        type = "object",
                        properties = new
                        {
                            fichas = new
                            {
                                type = "array",
                                items = new
                                {
                                    type = "object",
                                    properties = new
                                    {
                                        nome = new
                                        {
                                            type = "string"
                                        },

                                        exercicios = new
                                        {
                                            type = "array",
                                            items = new
                                            {
                                                type = "object",
                                                properties = new
                                                {
                                                    nome = new
                                                    {
                                                        type = "string"
                                                    },

                                                    grupoMuscular = new
                                                    {
                                                        type = "integer"
                                                    },

                                                    series = new
                                                    {
                                                        type = "integer"
                                                    },

                                                    repeticoes = new
                                                    {
                                                        type = "integer"
                                                    }
                                                },

                                                required = new[]
                                                    {
                                                        "nome",
                                                        "grupoMuscular",
                                                        "series",
                                                        "repeticoes"
                                                    },

                                                additionalProperties = false
                                            }
                                        }
                                    },

                                    required = new[]
                                        {
                                            "nome",
                                            "exercicios"
                                        },

                                    additionalProperties = false
                                }
                            }
                        },

                        required = new[]
            {
                "fichas"
            },

                        additionalProperties = false
                    }
                }
            },
            reasoning_effort = "low"
        };

        string json = JsonConvert.SerializeObject(payload);
        var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        HttpResponseMessage resposta = await _client.PostAsync(url, conteudo);

        string resultado = await resposta.Content.ReadAsStringAsync();
        if (!resposta.IsSuccessStatusCode)
            throw new Exception(resultado);
        GroqResposta? respostaGroq = JsonConvert.DeserializeObject<GroqResposta>(resultado);
        if (respostaGroq == null)
            throw new Exception("Erro ao obter resposta da IA.");
        return respostaGroq.Choices[0].Message.Content;
    }
}
public class GroqResposta
{
    public List<GroqChoice> Choices { get; set; } = new();
}

public class GroqChoice
{
    public GroqMessage Message { get; set; } = new();
}

public class GroqMessage
{
    public string Content { get; set; } = string.Empty;
}