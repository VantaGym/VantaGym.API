namespace VantaGym.Api.DTOs.Requisicao
{
    public class UsuarioAlterarSenha
    {
        public int ID { get; set; }
        public string SenhaNova { get; set; }
        public string SenhaAntiga { get; set; }
    }
}