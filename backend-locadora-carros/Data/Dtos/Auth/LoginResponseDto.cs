using System.ComponentModel.DataAnnotations;
using LocadoraCarrosBackEnd.Data.Dtos.Usuario;

namespace LocadoraCarrosBackEnd.Data.Dtos.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public DateTime DataExpiracao { get; set; }

        public ResponseUsuarioDto Usuario { get; set; } //rever
    }

}
