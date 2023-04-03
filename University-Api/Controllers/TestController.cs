using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Infrastructure.UnitOfWork;
using UniversityApi.Model;
using UniversityApi.Protos;

namespace UniversityApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _applicationDbContext;

    public TestController(IUnitOfWork unitOfWork , ApplicationDbContext applicationDbContext)
    {
        _unitOfWork = unitOfWork;
        _applicationDbContext = applicationDbContext;
    }
    [HttpPost]
    public async Task<Managers> test(ManagerRequest request)
    {
        try
        {
      var manager = new Managers { Email = request.Email, LastName = request.LastName, Name = request.Name };

        var result=await _unitOfWork.ManagerRepository.Create(manager);
    return result;
        }
        catch (Exception)
        {

            throw;
        }
  
    }
    [HttpPost("TEST")]
    public async Task<Managers> testmET(ManagerRequest request)
    {
        try
        {
            var Univer = new Universitys{ Address=request.University.Address, Name=request.University.Name};

            var result = await _unitOfWork.UniversityRepository.Create(Univer);
            var rres = await _applicationDbContext.Managers.Include(e=>e.Universitys).Where(e=>e.ManagerId==2).FirstOrDefaultAsync();
            rres.Universitys = result;
            await _applicationDbContext.SaveChangesAsync();
            return rres;
        }
        catch (Exception)
        {

            throw;
        }

    }
}
