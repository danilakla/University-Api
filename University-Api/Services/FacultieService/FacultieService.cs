using static UniversityApi.Controllers.ManagerController;
using System.Security.Claims;
using UniversityApi.Data;
using UniversityApi.Model;
using Microsoft.EntityFrameworkCore;

namespace UniversityApi.Services.FacultieService;

public class FacultieService : IFacultieService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public FacultieService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    
    public async Task AddFacultie(string FacultieName,int universityId)
    {
        try
        {
            var facultie = new Faculties()
            {
                Name = FacultieName
            };


            var university = await _applicationDbContext.Universitys.Include(e => e.Faculties).Where(e => e.UniversityId == universityId).FirstOrDefaultAsync();
            university.Faculties.Add(facultie);
            await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception e )
        {

            throw e;
        }
    }

    public async Task<Faculties> FindByName(string nameOfFacultie)
    {
        try
        {
            var facultie=await _applicationDbContext.Faculties.Where(e=>e.Name==nameOfFacultie).FirstOrDefaultAsync();
             if(facultie is null)
            {
                throw new Exception("facultie is not founed");
            }
       return facultie;
        }
        catch (Exception e)
        {

            throw e;
        }
    }

    public async Task<List<string>> GetFacultiesAsync()
    {
        try
        {
            var faculties = await _applicationDbContext.Faculties.ToListAsync();
            return faculties.Select(e=>e.Name).ToList();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
