using System.ComponentModel.DataAnnotations;

namespace LocadoraCarrosBackEnd.Data.Dtos.Usuario
{
    public class UpdateUsuarioDto
    {
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required]
        [StringLength(40)]
        public string Telefone { get; set; }
    }
}