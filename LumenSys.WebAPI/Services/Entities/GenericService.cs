using AutoMapper;
using LumenSys.WebAPI.Data.Interfaces;
using LumenSys.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace LumenSys.WebAPI.Services.Entities;

public class GenericService<T, Dto>: IGenericService<T, Dto> where T : class where Dto : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly IMapper _mapper;
    /*  
    private IProductGroupRepository repository;
    private IMapper mapper;
    */

    public GenericService(IGenericRepository<T> repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }
    /*
    public GenericService(IProductGroupRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;   
    }
    */
    public async Task<IEnumerable<Dto>> GetAll()
    {
        var entities = await _repository.Get();
        return _mapper.Map<IEnumerable<Dto>>(entities);
    }
    public async Task<Dto> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return _mapper.Map<Dto>(entity);
    }
    public async Task Create(Dto dto)
    {
        var entity = _mapper.Map<T>(dto);
        await _repository.Add(entity);
    }

    public async Task Update(Dto dto, int id)
    {
        var existing = await _repository.GetById(id);
        var entity = _mapper.Map<T>(dto);
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _repository.GetById(id);
        await _repository.Remove(entity);
    }
}
