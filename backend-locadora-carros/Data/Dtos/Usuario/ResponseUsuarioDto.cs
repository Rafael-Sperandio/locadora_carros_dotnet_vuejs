namespace LocadoraCarrosBackEnd.Data.Dtos.Usuario
{
    public class ResponseUsuarioDto
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public IList<string> Roles { get; set; }
    }

}
