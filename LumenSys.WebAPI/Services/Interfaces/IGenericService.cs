public interface IGenericService<T, Dto> where T : class where Dto : class
{
    Task<IEnumerable<Dto>> GetAll();
    Task<Dto> GetById(int id);
    Task Create(Dto dto);
    Task Update(Dto dto, int id);
    Task Delete(int id);
}
