using EventBus.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Infrastructure.UnitOfWork;
using UniversityApi.IntegrationEvents;
using UniversityApi.IntegrationEvents.Events;
using UniversityApi.Model;
using UniversityApi.Protos;

namespace UniversityApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IUniversityIntegrationEventService _universityIntegrationEventService;

    public TestController(IUnitOfWork unitOfWork , ApplicationDbContext applicationDbContext, IUniversityIntegrationEventService universityIntegrationEventService)
    {
        _unitOfWork = unitOfWork;
        _applicationDbContext = applicationDbContext;
        _universityIntegrationEventService = universityIntegrationEventService;
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
    public async Task testmET(ManagerRequest request)
    {
        try
        {
            await _universityIntegrationEventService.CreateProfile(new CreateProfileBaseOnUniverDataIntegrationEvent
            {
                BackPhoto = "dsada",
                BroadcastMessage = "1",
                Email = "2",
                LastName = "3",
                Name = "4",
                Photo = "5",
                ProfileId = 6,
                Role = "7",
                University = "8"


            }); ;

            await _universityIntegrationEventService.CreateProfile(new CreateProfileBaseOnUniverDataIntegrationEvent
            {
                BackPhoto = "dsada",
                BroadcastMessage = "1",
                Email = "2",
                LastName = "3",
                Name = "4",
                Photo = "5",
                ProfileId = 6,
                Role = "7",
                University = "8"


            }); ;
        }
        catch (Exception)
        {

            throw;
        }

    }
   
}
