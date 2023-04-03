using UniversityApi.Data;
using UniversityApi.Model;

namespace UniversityApi.Infrastructure;

public class UniversityRepository : IRepository<Universitys>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UniversityRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Universitys> Create(Universitys item)

    {

        try
        {
                 await _applicationDbContext.AddAsync(item);

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

    public Task<Universitys> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Universitys>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Update(Universitys item)
    {
        throw new NotImplementedException();
    }
}
