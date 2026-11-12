using System.ComponentModel.DataAnnotations;

namespace LocadoraCarrosBackEnd.Data.Dtos.Auth
{
    public class RegisterUsuarioDto
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

        [Required]
        [StringLength(20, MinimumLength = 9)]
        public string CNH { get; set; }
        //ex: 12345678901


        /* ex 
        {
          "nome": "cliente silva",
          "email": "cliente@cliente.com",
          "telefone": "+55 11 9XXXX-YYYY",
          "senha": "Pilar@123",
          "cnh": "12345678901"
        }
         */
    }

}
