namespace RealDougAPI.Models
{
    public class DadosDeUsuario
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public Enum TipoUsuario { get; set; }
    }
}
