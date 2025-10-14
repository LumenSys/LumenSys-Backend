using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Services.Interfaces;
using LumenSys.WebAPI.Objects;

namespace LumenSys.WebAPI.Services.Entities
{
    public class GenericService<T, TDto> : IGenericService<T, TDto>
        where T : class
        where TDto : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IMapper _mapper;
        private readonly ICompanyProvider _companyProvider;

        public GenericService(
            IGenericRepository<T> repository,
            IMapper mapper,
            ICompanyProvider companyProvider
        )
        {
            _repository = repository;
            _mapper = mapper;
            _companyProvider = companyProvider;
        }

        public virtual async Task<IEnumerable<TDto>> GetAll()
        {
            var entities = await _repository.Get();

            if (typeof(ICompanyScoped).IsAssignableFrom(typeof(T)))
            {
                var companyId = _companyProvider.GetCompanyId();
                var role = _companyProvider.GetUserRole();

                entities = role == "ADMINISTRATOR"
                    ? entities
                    : entities.Where(e => ((ICompanyScoped)e).CompanyId == companyId);
            }

            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> GetById(int id)
        {
            var entity = await _repository.GetById(id);

            if (entity is ICompanyScoped scoped)
            {
                var companyId = _companyProvider.GetCompanyId();
                var role = _companyProvider.GetUserRole();

                if (role != "ADMINISTRATOR" && scoped.CompanyId != companyId)
                    throw new UnauthorizedAccessException("Você não tem permissão para acessar este recurso.");
            }

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task Create(TDto entityDTO)
        {
            var entity = _mapper.Map<T>(entityDTO);
            if (entity is ICompanyScoped scoped)
            {
                var companyId = _companyProvider.GetCompanyId();
                scoped.CompanyId = companyId;
            }

            await _repository.Add(entity);
        }

        public virtual async Task Update(TDto entityDTO, int id)
        {
            var existingEntity = await _repository.GetById(id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Entidade com ID {id} não encontrada.");

            if (existingEntity is ICompanyScoped scoped)
            {
                var companyId = _companyProvider.GetCompanyId();
                var role = _companyProvider.GetUserRole();

                if (role != "ADMINISTRATOR" && scoped.CompanyId != companyId)
                    throw new UnauthorizedAccessException("Você não tem permissão para alterar este recurso.");
            }

            var entity = _mapper.Map<T>(entityDTO);
            await _repository.Update(entity);

        }

        public virtual async Task Delete(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entidade com ID {id} não encontrada.");

            if (entity is ICompanyScoped scoped)
            {
                var companyId = _companyProvider.GetCompanyId();
                var role = _companyProvider.GetUserRole();

                if (role != "ADMINISTRATOR" && scoped.CompanyId != companyId)
                    throw new UnauthorizedAccessException("Você não tem permissão para excluir este recurso.");
            }


            await _repository.Remove(entity);
        }
    }
}