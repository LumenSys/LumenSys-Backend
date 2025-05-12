using AutoMapper;
using LumenSys.WebAPI.Authentication;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Objects;
using LumenSys.WebAPI.Objects.DTOs.Entities;
using LumenSys.WebAPI.Objects.Models;
using LumenSys.WebAPI.Services.Interfaces;
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
        public async Task<UserDTO> GetByEmail(string email)
        {
            var userModel = await _userRepository.GetByEmail(email);

            if (userModel != null) userModel.Password = "";
            return _mapper.Map<UserDTO>(userModel);
        }

        public async Task<UserDTO> Login(Login login)
        {
            var userModel = await _userRepository.Login(login);

            if (userModel is not null) userModel.Password = "";
            return _mapper.Map<UserDTO>(userModel);
        }
    }
}