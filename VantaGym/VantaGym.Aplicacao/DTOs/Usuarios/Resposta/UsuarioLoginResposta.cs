namespace VantaGym.Api.DTOs.Resposta
{
    public class UsuarioLoginResposta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}