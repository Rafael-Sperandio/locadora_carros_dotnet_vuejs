using AutoMapper;
using LocadoraCarros.Data.Dtos.Carro;
using LocadoraCarros.Data.Dtos.Cliente;
using LocadoraCarros.Data.Dtos.Locacao;
using LocadoraCarros.Model;
using LocadoraCarros.Models;

namespace LocadoraCarros.Data.AutoMapper
{
    public class LocadoraCarrosAutomapper : Profile
    {

        public LocadoraCarrosAutomapper()
        {
            //verificar enum para automapper
            //carro
            CreateMap<Carro, ResponseCarroDto>().ReverseMap();
            /*
                        .ForMember(
                            dest => dest.Categoria,
                            opt => opt.MapFrom(src => src.Categoria.ToString())
                        )
                        .ForMember(
                            dest => dest.Status,
                            opt => opt.MapFrom(src => src.Status.ToString())
                        )
            */
            ;

            CreateMap<CreateCarroDto, Carro>().ReverseMap();
            CreateMap<UpdateCarroDto, Carro>().ReverseMap();

            //cliente
            CreateMap<Cliente, ResponseClienteDto>().ReverseMap(); 
            CreateMap<UpdateClienteDto, Cliente>().ReverseMap();
            CreateMap<CreateClienteDto, Cliente>().ReverseMap();

            //TODO precisa ser visto include e informação das dtos do carro e cliente
            //locacacao
            CreateMap<Locacao, ResponseLocacaoDto>().ReverseMap();
            CreateMap<UpdateLocacaoDto, Locacao>().ReverseMap();
            CreateMap<CreateLocacaoDto, Locacao>().ReverseMap();
        }
    }
}
