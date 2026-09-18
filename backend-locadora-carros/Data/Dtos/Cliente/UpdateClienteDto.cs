namespace LocadoraCarros.Data.Dtos.Cliente
{
    public class UpdateClienteDto
    {
        public UpdateClienteDto() { }

        public string Nome { get; set; }

        //regra de negocio de não poder atualizar o email
        //public string Email { get; set;}

        public string Telefone { get; set; }
    }
}
