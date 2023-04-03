
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Infrastructure.UnitOfWork;
using UniversityApi.IntegrationEvents;
using UniversityApi.IntegrationEvents.Events;
using UniversityApi.Model;
using UniversityApi.Protos;

public class UniversityService:University.UniversityBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IUniversityIntegrationEventService _universityService;

    public UniversityService(IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext, IUniversityIntegrationEventService universityService)
    {
        _unitOfWork = unitOfWork;
        _applicationDbContext = applicationDbContext;
        _universityService = universityService;
    }
    public async override Task<ManagerRespone> InitUniversity(ManagerRequest request, ServerCallContext context)
    {
        try
        {

            var manager = new Managers { Email = request.Email, LastName = request.LastName, Name = request.Name };
            var managerSaved = await _unitOfWork.ManagerRepository.Create(manager);
            var Univer = new Universitys { Address = request.University.Address, Name = request.University.Name };

            var univerSaved = await _unitOfWork.UniversityRepository.Create(Univer);

            manager.Universitys = univerSaved;
            await _applicationDbContext.SaveChangesAsync();
            CreateProfileBaseOnUniverDataIntegrationEvent managerforProfServiece = new()
            {
                BackPhoto = "",
                Email = manager.Email,
                LastName = manager.LastName,
                Name = manager.Name,
                Photo = "",
                ProfileId = manager.ManagerId,
                University = univerSaved.Name

            };
            await _universityService.CreateProfile(managerforProfServiece);
            return new ManagerRespone { ManagerId = manager.ManagerId, UniversityId = manager.Universitys.UniversityId };
        }
        catch (Exception)
        {

            throw;
        }

    }

    public async override Task<ManagerRespone> GetManagerInfo(Email request, ServerCallContext context)
    {

        try
        {
            var manager = await _applicationDbContext.Managers.Include(e => e.Universitys).Where(e => e.Email == request.Email_).SingleOrDefaultAsync();

            if(manager is null && manager.Universitys is null)
            {
                throw new Exception("manager or university is not founded");
            }
            return new ManagerRespone { ManagerId = manager.ManagerId, UniversityId = manager.Universitys.UniversityId };
        }
        catch (Exception)
        {

            throw;
        }
   
    }


}