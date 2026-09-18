using AutoMapper;

using Microsoft.EntityFrameworkCore;
using LocadoraCarros.Services.Interface;
using LocadoraCarros.Repository.Interface;
using LocadoraCarros.Data.Dtos.Locacao;
using LocadoraCarros.Models;
using LocadoraCarros.Models.Enums;
using LocadoraCarros.Repository;
using System.ComponentModel;

namespace LocadoraLocacaos.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly ICarroRepository _carroRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public LocacaoService(IMapper mapper,
            ILocacaoRepository locacaoRepository,
            ICarroRepository carroRepository,
            IClienteRepository clienteRepository
            )
        {
            _locacaoRepository = locacaoRepository;
            _carroRepository = carroRepository;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }
        public async Task<IEnumerable<ResponseLocacaoDto>> GetAll()
        {
            var locacaos = await _locacaoRepository.GetAll();
            return _mapper.Map<IEnumerable<ResponseLocacaoDto>>(locacaos);
        }

        public async Task<ResponseLocacaoDto> GetById(long id)
        {
            var locacao = await _locacaoRepository.GetById(id);
            return _mapper.Map<ResponseLocacaoDto>(locacao); ;
        }

        public async Task<IEnumerable<ResponseLocacaoDto>> GetByCliente(long clienteId)
        {
            var locacaos = await _locacaoRepository.GetByCarro(clienteId);
            return _mapper.Map<IEnumerable<ResponseLocacaoDto>>(locacaos);
        }

        public async Task<IEnumerable<ResponseLocacaoDto>> GetByCarro(long carroId)
        {
            var locacaos = await _locacaoRepository.GetByCarro(carroId);
            return _mapper.Map<IEnumerable<ResponseLocacaoDto>>(locacaos);
        }

        public async Task<ResponseLocacaoDto?> Create(CreateLocacaoDto dto)
        {
            var carro = await _carroRepository.GetById(dto.CarroId);

            if (carro == null)
                return null;
            //throw new NullReferenceException("Não existe Cliente com esse id");

            var cliente = await _clienteRepository.GetById(dto.ClienteId);

            if (cliente == null)
                return null;

            if (dto.DataInicio >= dto.DataFim)
                throw new InvalidOperationException(
                    "A data de início deve ser anterior à data de fim.");

            var possuiLocacao = await _locacaoRepository.CarroPossuiLocacaoNoPeriodo(
                dto.CarroId,
                dto.DataInicio,
                dto.DataFim);

            if (possuiLocacao)
                throw new InvalidOperationException(
                    "Já existe uma locação para este carro nesse período.");

            var locacao = _mapper.Map<Locacao>(dto);

            locacao.Status = StatusLocacao.Reservada;

            // Guarda o preço praticado no momento da locação.
            locacao.ValorDiaria = carro.ValorDiaria;

            locacao = await _locacaoRepository.Add(locacao);
            await _locacaoRepository.SaveChangesAsync();

            return _mapper.Map<ResponseLocacaoDto>(locacao);
        }

        //regaras do update revisar
        // ao fazer update deve-se deixar o valor fornecido 

        public async Task<ResponseLocacaoDto?> Update(
    long id,
    UpdateLocacaoDto dto)
        {
            var locacao = await _locacaoRepository.GetById(id);

            if (locacao == null)
                return null;



            if (dto.DataInicio >= dto.DataFim)
                throw new InvalidOperationException(
                    "A data de início deve ser anterior à data de fim.");

            var possuiLocacao = await _locacaoRepository.CarroPossuiLocacaoNoPeriodo(
                locacao.CarroId,
                dto.DataInicio,
                dto.DataFim,
                id);

            if (possuiLocacao)
                throw new InvalidOperationException(
                    "Já existe uma locação para este carro nesse período.");

            locacao.DataInicio = dto.DataInicio;
            locacao.DataFim = dto.DataFim;

            locacao = await _locacaoRepository.Update(locacao);
            await _locacaoRepository.SaveChangesAsync();

            return _mapper.Map<ResponseLocacaoDto>(locacao);
        }

        /*
                public async Task<ResponseLocacaoDto?> Update(long id,
                UpdateLocacaoDto dto)

                {
                    var locacao = await _locacaoRepository.GetById(id);

                    if (locacao == null)
                    {
                        return null;
                    }
                    locacao = _mapper.Map(dto, locacao);


                    var naoAlugar = await _locacaoRepository.CarroPossuiLocacaoNoPeriodo(locacao.CarroId,locacao.DataInicio,locacao.DataFim,id);
                    if (naoAlugar)
                    {
                        throw new InvalidOperationException("Já existe uma locação nesse periodo");
                    }
                    locacao = await _locacaoRepository.Update(locacao);
                    await _locacaoRepository.SaveChangesAsync();
                    return _mapper.Map<ResponseLocacaoDto>(locacao);
                }
        */
        //antigo para update

        //verificar se o carro está disponivel no horarrio

        //pode ser passado para um metodo auxiliar
        //NÃO é necessario atualizar carro
        /*            if(locacao.Status != StatusLocacao.Reservada)
                    { 
                        var carro = await _carroRepository.GetById(locacao.CarroId);
                   if (locacao.Status == StatusLocacao.Ativa)
                        {

                        }

                    }*/

        /*            if (locacao.Status != StatusLocacao.Ativa &&!(await CarroDisponivel(locacao)))
                    {
                        //melhor lancar um erro
                        //throw carro indisponivel
                        return null ;
                    }*/

        //fim antigo para update


        public async Task<ResponseLocacaoDto> DeleteById(long id)
        {
            var locacao = await _locacaoRepository.GetById(id);

            if (locacao == null)
            {
                return null;
            }

            var retorno = _mapper.Map<ResponseLocacaoDto>(locacao);
            await _locacaoRepository.Delete(locacao);
            await _locacaoRepository.SaveChangesAsync();
            return retorno;
        }

        public async Task<ResponseLocacaoDto?> SetStatusCancelar(long id)
        {
            return await ChangeStatus(id, StatusLocacao.Cancelada);
        }

        public async Task<ResponseLocacaoDto?> SetStatusFinalizar(long id)
        {
            return await ChangeStatus(id, StatusLocacao.Finalizada);
        }

        private async Task<ResponseLocacaoDto?> ChangeStatus(
            long id,
            StatusLocacao novoStatus)
        {
            var locacao = await _locacaoRepository.GetById(id);

            if (locacao == null)
                return null;

/*            // Regras de transição de status
            if (!PodeAlterarStatus(locacao.Status, novoStatus))
                throw new InvalidOperationException(
                    "Não é possível realizar essa alteração de status.");*/

            locacao.Status = novoStatus;

            await _locacaoRepository.Update(locacao);
            await _locacaoRepository.SaveChangesAsync();

            return _mapper.Map<ResponseLocacaoDto>(locacao);
        }



    }
}
