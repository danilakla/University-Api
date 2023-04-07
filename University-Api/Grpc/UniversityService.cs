
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Dto;
using UniversityApi.Grpc.Client;
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
    private readonly ITeacherService _teacherService;

    public UniversityService(IUnitOfWork unitOfWork, 
        ApplicationDbContext applicationDbContext,
        IUniversityIntegrationEventService universityService,
        ITeacherService teacherService
        )
    {
        _unitOfWork = unitOfWork;
        _applicationDbContext = applicationDbContext;
        _universityService = universityService;
        _teacherService = teacherService;
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
                University = univerSaved.Name,
                Role="Manager",

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

            if (manager is null && manager.Universitys is null)
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

    public async override Task<TeacherResponse> GetTeacherInfo(Email request, ServerCallContext context)
    {
        try
        {
            var response = await _teacherService.GetTeacherInfo(email: request.Email_);
            var castToRespones = new TeacherResponse { TeacherId = response.TeacherId, UniversityId = response.UniversityId };
         
            return castToRespones;
        }
        catch (Exception e)
        {

            throw e;
        }
    }
    public async override Task<TeacherResponse> InitTeacher(TeacherRequest request, ServerCallContext context)
    {
        try
        {
            var teacherRequest = new CreateTeacherDTO { UniversityId=request.UniversityId, Email=request.Email, LastName=request.LastName, Name=request.Name };
             var response=    await _teacherService.InitTeacher(teacherRequest);
            var university=await _applicationDbContext.Universitys.FindAsync(response.UniversityId);
            CreateProfileBaseOnUniverDataIntegrationEvent teacherforProfServiece = new()
            {
                BackPhoto = "",
                Email = request.Email,
                LastName = request.LastName,
                Name = request.Name,
                Photo = "",
                ProfileId = response.TeacherId,
                University = university.Name,
                Role="Teacher"

            };
            await _universityService.CreateProfile(teacherforProfServiece);

            var castToRespones = new TeacherResponse { TeacherId = response.TeacherId, UniversityId = response.UniversityId };
            return castToRespones;
        }
        catch (Exception)
        {

            throw;
        }
    }

}