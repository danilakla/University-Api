using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Infrastructure.UnitOfWork;
using UniversityApi.Model;
using UniversityApi.Protos;

namespace UniversityApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TestController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpPost]
    public async Task<Managers> test(ManagerRequest request)
    {
        var manager = new Managers { Email = request.Email, LastName = request.LastName, Name = request.Name };

        var result=await _unitOfWork.ManagerRepository.Create(manager);
    return result;
    }
}
