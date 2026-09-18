using LocadoraCarros.Model.Base;
using LocadoraCarros.Model;
using System.ComponentModel.DataAnnotations.Schema;
using LocadoraCarros.Models.Enums;

namespace LocadoraCarros.Data.Dtos.Locacao
{
    public class ResponseLocacaoDto
    {
        public ResponseLocacaoDto() { }

        public long Id { get; set; }

        public long ClienteId { get; set; }

        public long CarroId { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public decimal ValorDiaria { get; set; }

        public decimal ValorTotal { get; set; }
        public StatusLocacao Status { get; set; }

    }
}
