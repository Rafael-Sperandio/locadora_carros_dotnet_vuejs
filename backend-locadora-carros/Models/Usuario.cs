using global::LocadoraCarros.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LocadoraCarrosBackEnd.Models
{
    [Table("usuario")]
    public class Usuario : BaseEntity
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
    }
}