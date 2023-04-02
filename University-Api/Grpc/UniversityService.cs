
using Grpc.Core;
using UniversityApi.Protos;

public class UniversityService:University.UniversityBase
{
    public async override Task<ManagerRespone> InitUniversity(ManagerRequest request, ServerCallContext context)
    {
        return new ManagerRespone { ManagerId=2, UniversityId=6 };
    }
}