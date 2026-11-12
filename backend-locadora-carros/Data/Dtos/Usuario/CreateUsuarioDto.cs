using System.ComponentModel.DataAnnotations;

namespace LocadoraCarrosBackEnd.Data.Dtos.Usuario
{
    public class CreateUsuarioDto
    {
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(40)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Senha { get; set; }

        //TODO criar regra no service para apenas adm criar
        [Required]
        public string Role { get; set; }
    }

}