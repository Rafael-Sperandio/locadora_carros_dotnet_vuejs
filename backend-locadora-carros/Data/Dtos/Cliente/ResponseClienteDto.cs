namespace LocadoraCarros.Data.Dtos.Cliente
{
    public class ResponseClienteDto
    {
        public ResponseClienteDto() { }
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public string Telefone { get; set; }
    }
}
