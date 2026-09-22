using AutoMapper;

using Microsoft.EntityFrameworkCore;
using LocadoraCarros.Services.Interface;
using LocadoraCarros.Repository.Interface;
using LocadoraCarros.Data.Dtos.Locacao;
using LocadoraCarros.Models;
using LocadoraCarros.Models.Enums;
using LocadoraCarros.Repository;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Query;
using LocadoraCarros.Model;
using LocadoraCarrosBackEnd.Utils.constants;
using LocadoraCarros.Models.Enums.Carro;
using LocadoraCarrosBackEnd.Exceptions;

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
                throw new RegraNegocioException(
                    LocacaoErrors.CarroNaoEncontrado);
            //throw new NullReferenceException("Não existe Cliente com esse id");

            var cliente = await _clienteRepository.GetById(dto.ClienteId);

            if (cliente == null)
                throw new RegraNegocioException(
                    LocacaoErrors.ClienteNaoEncontrado);

            ValidarPeriodo(dto.DataInicio, dto.DataFim);

            var carroIndisponivel = StatusCarrosIndisponivel(carro) ||
                await _locacaoRepository.CarroPossuiLocacaoNoPeriodo(
                    dto.CarroId,
                    dto.DataInicio,
                    dto.DataFim);

            if (carroIndisponivel)
                throw new RegraNegocioException(LocacaoErrors.CarroIndisponivel);

            var locacao = _mapper.Map<Locacao>(dto);

            locacao.Status = StatusLocacao.Reservada;
            locacao.Cliente = cliente;
            // Guarda o preço praticado no momento da locação.
            locacao.Carro = carro;
            AtualizarPrecoLocacao(locacao);

            locacao = await _locacaoRepository.Add(locacao);
            await _locacaoRepository.SaveChangesAsync();

            return _mapper.Map<ResponseLocacaoDto>(locacao);
        }

        //regaras do update revisar
        // ao fazer update deve-se deixar o valor fornecido 

        public async Task<ResponseLocacaoDto?> Update(
            long id,
            UpdateLocacaoDto dto,
            bool atualizarPreco)
        {
            var locacao = await _locacaoRepository.GetById(
                id,
                includeCarro: true);

            if (locacao == null)
                return null;

            ValidarPeriodo(dto.DataInicio, dto.DataFim);
            var carroIndisponivel = StatusCarrosIndisponivel(locacao.Carro) ||
                await _locacaoRepository.CarroPossuiLocacaoNoPeriodo(
                locacao.CarroId,
                dto.DataInicio,
                dto.DataFim,
                id);


            if (carroIndisponivel)
                throw new RegraNegocioException(
                    LocacaoErrors.CarroIndisponivel);


            locacao.DataInicio = dto.DataInicio;
            locacao.DataFim = dto.DataFim;
            if (dto.DataDevolucao != null)
            {
                ValidarPeriodo(dto.DataInicio, dto.DataDevolucao.Value);//validação talvez deva mudar o texto do erro
                locacao.DataDevolucao = dto.DataDevolucao;
            }
            //pode ser usar o metodo
            if (!PodeAlterarStatus(locacao.Status, dto.Status))
                throw new RegraNegocioException(
                    LocacaoErrors.MudancaStatusIvalida);

            locacao.Status = dto.Status;

            if (atualizarPreco)
            {
                AtualizarPrecoLocacao(locacao);
            }

            locacao = await _locacaoRepository.Update(locacao);
            await _locacaoRepository.SaveChangesAsync();

            return _mapper.Map<ResponseLocacaoDto>(locacao);
            
        }

        private void AtualizarPrecoLocacao(Locacao locacao)
        {
            var quantidadeDias = (locacao.DataFim - locacao.DataInicio).Days+1;

            locacao.ValorDiaria = locacao.Carro.ValorDiaria;
            locacao.ValorTotal = locacao.ValorDiaria * quantidadeDias;
        }

        private void ValidarPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio >= dataFim)
            {
                throw new RegraNegocioException(LocacaoErrors.PeriodoInvalido);
            }
        }

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
            
            // Regras de transição de status
            if (!PodeAlterarStatus(locacao.Status, novoStatus))
                throw new RegraNegocioException(
                    LocacaoErrors.MudancaStatusIvalida);


            locacao.Status = novoStatus;

            await _locacaoRepository.Update(locacao);
            await _locacaoRepository.SaveChangesAsync();

            return _mapper.Map<ResponseLocacaoDto>(locacao);
        }

        private bool PodeAlterarStatus(StatusLocacao statusAtual, StatusLocacao statusNovo)
        {
           switch (statusNovo)
            {
                case StatusLocacao.Cancelada:
                    return statusAtual == StatusLocacao.Reservada ||
                           statusAtual == StatusLocacao.Ativa;
                case StatusLocacao.Finalizada:
                    return statusAtual == StatusLocacao.Ativa;
                default: 
                    return true;
            }
        }
        private bool StatusCarrosIndisponivel(Carro carro)
        {
            return carro.Status != StatusCarro.Disponivel;

        }
    }
}
