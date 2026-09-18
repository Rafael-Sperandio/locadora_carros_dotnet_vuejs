using LocadoraCarros.Model.Base;
using LocadoraCarros.Models.Enums.Carro;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace LocadoraCarros.Models
{
    /// <summary>
    /// Representa um veículo disponível para ser alugado pela locadora.
    /// </summary>
    [Table("carro")]
    public class Carro : BaseEntity
    {
        /// <summary>
        /// Fabricante do veículo.
        /// </summary>
        [Column("marca")]
        [Required]
        [StringLength(150)]
        public string Marca { get; set; }

        /// <summary>
        /// Modelo do veículo.
        /// </summary>
        [Column("modelo")]
        [Required]
        [StringLength(350)]
        public string Modelo { get; set; }

        /// <summary>
        /// Ano de fabricação ou modelo do veículo.
        /// </summary>
        [Column("ano")]
        public int Ano { get; set; }

        /// <summary>
        /// Placa de identificação do veículo.
        /// </summary>
        [Column("placa")]
        [Required]
        [StringLength(10)]
        public string Placa { get; set; }

        /// <summary>
        /// Categoria à qual o veículo pertence.
        /// </summary>
        [Column("categoria")]
        [Required]
        public CategoriaCarro Categoria { get; set; }

        /// <summary>
        /// Valor cobrado por dia de aluguel.
        /// </summary>
        [Column("valor_diaria", TypeName = "decimal(10,2)")]
        [Range(0.01, 50000)]
        public decimal ValorDiaria { get; set; }

        /// <summary>
        /// Situação atual do veículo na locadora.
        /// </summary>
        [Column("status")]
        public StatusCarro Status { get; set; }


    }
}
