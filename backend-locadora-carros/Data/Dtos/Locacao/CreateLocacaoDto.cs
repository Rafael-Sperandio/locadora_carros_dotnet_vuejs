using LocadoraCarros.Model.Base;
using LocadoraCarros.Model;
using System.ComponentModel.DataAnnotations.Schema;
using LocadoraCarros.Models.Enums;

namespace LocadoraCarros.Data.Dtos.Locacao
{
    public class CreateLocacaoDto
    {
        public CreateLocacaoDto() { }

        public long ClienteId { get; set; }

        public long CarroId { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

    }
}
