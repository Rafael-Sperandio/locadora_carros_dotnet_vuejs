using LocadoraCarros.Model.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LocadoraCarros.Model
{


    [Table("cliente")]
    public class Cliente : BaseEntity
    {

        [Column("nome")]
        [Required]
        [StringLength(150)]
        public string Nome {  get; set; }


        [Column("email")]
        [Required]
        [StringLength(150)]
        public string Email { get; set; }


        [Column("telefone")]
        [Required]
        [StringLength(40)]
        //rever 40 talvez seja mais mesmo para um numero internacional
        //https://faq.whatsapp.com/1294841057948784/?locale=pt_BR
        public string Telefone { get; set; }

        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }

        [Column("cnh")]
        [StringLength(40)]
        public string CNH { get; set; }

        [Column("validade_cnh")]
        public DateTime validadeCNH { get; set; }
    }
}
