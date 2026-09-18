namespace LocadoraCarros.Data.Dtos.Cliente
{
    public class CreateClienteDto
    {
        public CreateClienteDto() { }

        public string Nome { get; set; }
        public string Email { get; set; }

        public string Telefone { get; set; }
    }
}
