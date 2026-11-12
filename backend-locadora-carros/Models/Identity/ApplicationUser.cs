using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraCarrosBackEnd.Models.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {

        //Email retirado pois é propriedade ApplicationUser 
        //caso necessario um e-mail de contato, poderá adicionar EmailContato à entidade Usuario 
        //assim ele terá uma finalidade diferente do e-mail de autenticação.

        [Column("nome")]
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Column("telefone")]
        [Required]
        [StringLength(40)]
        public string Telefone { get; set; }

        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }

        [Column("endereco")]
        [StringLength(300)]
        public string? EnderecoCompleto { get; set; }

        //tamanho eh 11 para ter certeza usar 15
        [Column("cnh")]
        [StringLength(15)]
        public string CNH { get; set; }

        [Column("validade_cnh")]
        public DateTime ValidadeCNH { get; set; }

        /*        public long UsuarioId { get; set; }

                [ForeignKey(nameof(UsuarioId))]
                public Usuario Usuario { get; set; }*/


    }
}
