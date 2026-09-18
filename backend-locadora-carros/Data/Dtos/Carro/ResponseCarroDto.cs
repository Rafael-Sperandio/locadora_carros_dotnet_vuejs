using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LocadoraCarros.Models.Enums.Carro;

namespace LocadoraCarros.Data.Dtos.Carro
{
    public class ResponseCarroDto
    {
        public long Id { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public long Ano { get; set; }

        public string Placa { get; set; }

        public CategoriaCarro Categoria { get; set; }

        public decimal ValorDiaria { get; set; }

        public StatusCarro Status { get; set; }
    }
}
