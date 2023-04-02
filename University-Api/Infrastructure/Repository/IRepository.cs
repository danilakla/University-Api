namespace UniversityApi.Infrastructure;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T> Get(int id);
    Task<T> Create(T item);
    Task Update(T item);
    Task Delete(int id);
}