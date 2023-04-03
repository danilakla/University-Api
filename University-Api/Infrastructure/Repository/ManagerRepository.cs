using UniversityApi.Data;
using UniversityApi.Model;

namespace UniversityApi.Infrastructure.Repository;

public class ManagerRepository : IRepository<Managers>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ManagerRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Managers> Create(Managers item)
    {
        try
        {
            await _applicationDbContext.AddAsync(item);
            await _applicationDbContext.SaveChangesAsync();
            return item;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Managers> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Managers>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Update(Managers item)
    {
        throw new NotImplementedException();
    }
}
