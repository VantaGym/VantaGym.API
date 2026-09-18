using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        string apiKey = _configuration["Gemini:ApiKey"];

        string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-3.6-flash:generateContent";
        _client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);

        var payload = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = $"{especializacao}\n\n{pergunta}"
                        }
                    }
                }
            }
        };

        string json = JsonConvert.SerializeObject(payload);
        var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage resposta = await _client.PostAsync(url, conteudo);
        resposta.EnsureSuccessStatusCode();

        string resultado = await resposta.Content.ReadAsStringAsync();
        GeminiResposta? respostaGemini = JsonConvert.DeserializeObject<GeminiResposta>(resultado);
        if (respostaGemini == null)
            throw new Exception("Erro ao obter resposta da IA.");
        return respostaGemini.Candidates[0].Content.Parts[0].Text;
    }
}

public class GeminiResposta
{
    public List<GeminiCandidate> Candidates { get; set; }
}

public class GeminiCandidate
{
    public GeminiContent Content { get; set; }
}

public class GeminiContent
{
    public List<GeminiPart> Parts { get; set; }
}

public class GeminiPart
{
    public string Text { get; set; }
}