using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.IntegrationEvents;
using UniversityApi.IntegrationEvents.Events;
using UniversityApi.Model;
using UniversityApi.Protos;

namespace UniversityApi.Grpc;

public class DeanServicesGrpc:Dean.DeanBase
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IUniversityIntegrationEventService _universityIntegrationEventService;

    public DeanServicesGrpc(ApplicationDbContext applicationDbContext, IUniversityIntegrationEventService universityIntegrationEventService)
    {
        _applicationDbContext = applicationDbContext;
        _universityIntegrationEventService = universityIntegrationEventService;
    }
    public async override Task<DeanResponse> InitDean(DeanRequest request, ServerCallContext context)
    {
        try
        {
            var dean = new Deans { Email = request.Email, Name = request.Name, PhoneNumber = "***", LastName = request.LastName };

            await _applicationDbContext.Deans.AddAsync(dean);

            var facultie = await _applicationDbContext.Faculties.FindAsync(request.DeansId.FacultieId);
            var university = await _applicationDbContext.Faculties.FindAsync(facultie.UniversitysId);

            dean.Faculties = facultie;
            await _applicationDbContext.SaveChangesAsync();
            var profileReq = CreateProfileRequset(request, dean.DeanId, university.Name);
            _universityIntegrationEventService.CreateProfile(profileReq);
            return new DeanResponse { DeanId = dean.DeanId, FacultieId = facultie.FacultieId, UniversityId = facultie.UniversitysId };
        }
        catch (Exception)
        {

            throw;
        }


    
    }

    public override Task<DeanResponse> GetDeanInfo(EmailDean request, ServerCallContext context)
    {

        return base.GetDeanInfo(request, context);
    }






    private CreateProfileBaseOnUniverDataIntegrationEvent CreateProfileRequset(DeanRequest request, int deanId, string univName)
    {
        CreateProfileBaseOnUniverDataIntegrationEvent managerforProfServiece = new()
        {
            BackPhoto = "",
            Email = request.Email,
            LastName = request.LastName,
            Name = request.Name,
            Photo = "",
            ProfileId = deanId,
            University = univName

        };
        return managerforProfServiece;
    }
}
