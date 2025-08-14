using AutoMapper;
using LumenSys.WebAPI.Authentication;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Services.Utils;
using System.Threading;

namespace LumenSys.WebAPI.Services.Entities
{
    public class UserService : GenericService<User, UserDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }

        public override async Task<UserDTO> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user is null)
                throw new ArgumentNullException($"Usuário com o ID {id} não foi encontrado.");

            return _mapper.Map<UserDTO>(user);
        }

        public override async Task Create(UserDTO userDto)
        {
            if (userDto is null)
                throw new ArgumentNullException("O usuário não pode ser nulo.");

            if (await CheckDuplicate(u => u.Email, userDto.Email, 0))
                throw new InvalidOperationException("E-mail já está em uso.");

            if (await CheckDuplicate(u => u.Phone, userDto.Phone, 0))
                throw new InvalidOperationException("Telefone já está em uso.");

            if (await CheckDuplicate(u => u.Cpf, userDto.Cpf, 0))
                throw new InvalidOperationException("CPF já está em uso.");

            userDto.Password = OperatorUltilitie.GenerateHash(userDto.Password);
            await base.Create(userDto);
        }
        public override async Task Update(UserDTO userDto, int id)
        {
            if (userDto is null)
                throw new ArgumentNullException("O usuário não pode ser nulo.");

            if (userDto.Id != id)
                throw new ArgumentException("O ID do usuário deve corresponder ao ID informado.");

            if (await CheckDuplicate(u => u.Email, userDto.Email, id))
                throw new InvalidOperationException("E-mail já está em uso.");

            if (await CheckDuplicate(u => u.Phone, userDto.Phone, id))
                throw new InvalidOperationException("Telefone já está em uso.");

            if (await CheckDuplicate(u => u.Cpf, userDto.Cpf, id))
                throw new InvalidOperationException("CPF já está em uso.");

            userDto.Password = OperatorUltilitie.GenerateHash(userDto.Password);
            await base.Update(userDto, id);
        }

        public override async Task Delete(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user is null)
                throw new ArgumentNullException($"Usuário com o ID {id} não foi encontrado.");

            await base.Delete(id);
        }

        private async Task<bool> CheckDuplicate(Func<User, string?> selector, string? valor, int idIgnor)
        {
            var usuarios = await _userRepository.Get();
            return usuarios.Any(u =>
                u.Id != idIgnor &&
                !string.IsNullOrWhiteSpace(selector(u)) &&
                StringUtils.CompareString(selector(u)!, valor)
            );
        }
        public async Task<UserDTO> GetByEmail(string email)
        {
            var userModel = await _userRepository.GetByEmail(email);
            if (userModel == null)
                throw new InvalidOperationException("Não existe nenhum usuário com o e-mail informado.");

            userModel.Password = "";
            return _mapper.Map<UserDTO>(userModel);
        }

        public async Task<UserDTO> Login(Login login)
        {
            if (login == null)
                throw new ArgumentNullException("Dados de login não podem ser nulos.");

            var userModel = await _userRepository.GetByEmail(login.Email);
            if (userModel == null)
                throw new InvalidOperationException("E-mail ou senha inválidos!");

            if (!string.Equals(userModel.Password, login.Password, StringComparison.Ordinal))
                throw new InvalidOperationException("E-mail ou senha inválidos.");
  
            userModel.Password = "";
            return _mapper.Map<UserDTO>(userModel);
        }

    }
}