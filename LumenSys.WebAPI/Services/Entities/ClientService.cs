using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;

namespace LumenSys.WebAPI.Services.Entities
{
    public class ClientService : GenericService<Client, ClientDTO>, IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _clientRepository = repository;
            _mapper = mapper;
        }

        public override async Task<ClientDTO> GetById(int id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
                throw new ArgumentNullException($"Cliente com o ID {id} não foi encontrado.");

            return _mapper.Map<ClientDTO>(client);
        }

        public override async Task Create(ClientDTO clientDto)
        {
            if (clientDto == null)
                throw new ArgumentNullException("Cliente não pode ser nulo.");

            if (await CheckDuplicate(c => c.Cpf, clientDto.Cpf, 0))
                throw new InvalidOperationException("Já existe um cliente com esse CPF.");

            if (await CheckDuplicate(c => c.Email, clientDto.Email, 0))
                throw new InvalidOperationException("Já existe um cliente com esse e-mail.");

            if (!CpfCnpjValidator.IsValid(clientDto.Cpf))
                throw new ArgumentException("CPF inválido.");

            await base.Create(clientDto);
        }

        public override async Task Update(ClientDTO clientDto, int id)
        {
            if (clientDto == null)
                throw new ArgumentNullException("Cliente não pode ser nulo.");

            if (clientDto.Id != id)
                throw new ArgumentException("O ID do cliente deve corresponder ao ID informado.");

            if (await CheckDuplicate(c => c.Cpf, clientDto.Cpf, id))
                throw new InvalidOperationException("Já existe um cliente com esse CPF.");

            if (await CheckDuplicate(c => c.Email, clientDto.Email, id))
                throw new InvalidOperationException("Já existe um cliente com esse e-mail.");

            if (!CpfCnpjValidator.IsValid(clientDto.Cpf))
                throw new ArgumentException("CPF inválido.");

            await base.Update(clientDto, id);
        }

        public override async Task Delete(int id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
                throw new ArgumentNullException($"Cliente com o ID {id} não foi encontrado.");

            await base.Delete(id);
        }

        private async Task<bool> CheckDuplicate(Func<Client, string?> selector, string? valor, int idIgnorar)
        {
            var clientes = await _clientRepository.Get();
            return clientes.Any(c =>
                c.Id != idIgnorar &&
                !string.IsNullOrWhiteSpace(selector(c)) &&
                StringUtils.CompareString(selector(c)!, valor)
            );
        }
    }
}