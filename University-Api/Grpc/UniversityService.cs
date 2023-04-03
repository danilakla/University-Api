
using Grpc.Core;
using UniversityApi.Data;
using UniversityApi.Infrastructure.UnitOfWork;
using UniversityApi.Model;
using UniversityApi.Protos;

public class UniversityService:University.UniversityBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _applicationDbContext;

    public UniversityService(IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext)
    {
        _unitOfWork = unitOfWork;
        _applicationDbContext = applicationDbContext;
    }
    public async override Task<ManagerRespone> InitUniversity(ManagerRequest request, ServerCallContext context)
    {

        var manager = new Managers { Email = request.Email, LastName = request.LastName, Name=request.Name };
        var managerSaved = await _unitOfWork.ManagerRepository.Create(manager);
        var Univer = new Universitys { Address = request.University.Address, Name = request.University.Name };

        var univerSaved = await _unitOfWork.UniversityRepository.Create(Univer);

        manager.Universitys = univerSaved;
        await _applicationDbContext.SaveChangesAsync();

        return new ManagerRespone { ManagerId=manager.ManagerId, UniversityId=manager.Universitys.UniversityId};
    }
}