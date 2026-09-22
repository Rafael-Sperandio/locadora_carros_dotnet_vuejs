using LocadoraCarros.Model.Base;
using LocadoraCarros.Model;
using System.ComponentModel.DataAnnotations.Schema;
using LocadoraCarros.Models.Enums;

namespace LocadoraCarros.Data.Dtos.Locacao
{
    public class UpdateLocacaoDto
    {
        public UpdateLocacaoDto() { }

        //não pode ser alterado o cliente nem o carro
        //cria-se uma nova locação
        // no futuro a regra pode mudar

/*        
        public long ClienteId { get; set; }
        public long CarroId { get; set; }
*/

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public DateTime? DataDevolucao { get; set; }

/*
        public decimal ValorDiaria { get; set; }

        public decimal ValorTotal { get; set; }
*/

        public StatusLocacao Status { get; set; }

       // public bool Atualizarvalor

    }
}
