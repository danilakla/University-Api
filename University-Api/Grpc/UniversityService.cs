
using Grpc.Core;
using UniversityApi.Infrastructure.UnitOfWork;
using UniversityApi.Model;
using UniversityApi.Protos;

public class UniversityService:University.UniversityBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UniversityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async override Task<ManagerRespone> InitUniversity(ManagerRequest request, ServerCallContext context)
    {
        var manager = new Managers { Email = request.Email, LastName = request.LastName, Name=request.Name };
   var managerData= await _unitOfWork.ManagerRepository.Create(manager);
        return new ManagerRespone { ManagerId=2, UniversityId=6 };
    }
}