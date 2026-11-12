using LocadoraCarros.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;
using LocadoraCarros.Models.Enums;
using LocadoraCarrosBackEnd.Models.Identity;

namespace LocadoraCarros.Models
{
    [Table("locacao")]
    public class Locacao : BaseEntity
    {
        [Column("usuario_id")]
        public long UsuarioId { get; set; }

        [Column("carro_id")]
        public long CarroId { get; set; }

        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Column("data_fim")]
        public DateTime DataFim { get; set; }

        [Column("data_devolucao")]
        public DateTime? DataDevolucao { get; set; }

        [Column("valor_diaria")]
        public decimal ValorDiaria { get; set; }

        [Column("valor_total")]
        public decimal ValorTotal { get; set; }

        [Column("status")]
        public StatusLocacao Status { get; set; }

        public ApplicationUser Usuario { get; set; }

        public Carro Carro { get; set; }
    }
}
