using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UniversityApi.Data;
using UniversityApi.IntegrationEvents;
using UniversityApi.IntegrationEvents.Events;
using UniversityApi.Model;
using UniversityApi.Protos;

namespace UniversityApi.Grpc;

public class StudentGrpcServer:Student.StudentBase
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IUniversityIntegrationEventService _universityIntegrationEventService;

    public StudentGrpcServer(ApplicationDbContext applicationDbContext, IUniversityIntegrationEventService universityIntegrationEventService)
    {
        _applicationDbContext = applicationDbContext;
        _universityIntegrationEventService = universityIntegrationEventService;
    }
    public override async Task<StudentResponse> GetStudentInfo(EmailStudent request, ServerCallContext context)
    {
        try
        {
            var student = await _applicationDbContext.Students.Include(e => e.Groups).Where(e => e.Email == request.Email).FirstOrDefaultAsync();
            var facuiltie = await _applicationDbContext.Faculties.FindAsync(student.Groups.FacultiesId);
            var response = new StudentResponse
            {
                StudentId = student.StudentId,
                GroupId = student.Groups.GroupId,
                FacultieId = facuiltie.FacultieId,
                ProfessionId = (int)student.Groups.ProfessionsId,
                UniversityId = facuiltie.UniversitysId,

            };
            return response;
        }
        catch (Exception)
        {

            throw;
        }
      
    }
    public override async Task<StudentResponse> InitStudent(StudentRequest request, ServerCallContext context)
    {


        try
        {
            var groups = await _applicationDbContext.Groups.FindAsync(request.StudentIds.GroupId);
            var facuiltie = await _applicationDbContext.Faculties.FindAsync(groups.FacultiesId);
            var universitys = await _applicationDbContext.Universitys.FindAsync(facuiltie.UniversitysId);

            var student = new Students
            {
                Email = request.Email,
                Name = request.Name,
                SecondName = request.LastName
            };
            groups.Students.Add(student);
            await _applicationDbContext.SaveChangesAsync();
            CreateProfileBaseOnUniverDataIntegrationEvent StudentforProfServiece = new()
            {
                BackPhoto = "",
                Email = request.Email,
                LastName = request.LastName,
                Name = request.Name,
                Photo = "",
                ProfileId = student.StudentId,
                University = universitys.Name,
                Role = "Student",


            };
            await _universityIntegrationEventService.CreateProfile(StudentforProfServiece);

            var response = new StudentResponse
            {
                StudentId = student.StudentId,
                GroupId = groups.GroupId,
                FacultieId = facuiltie.FacultieId,
                ProfessionId = (int)groups.ProfessionsId,
                UniversityId = universitys.UniversityId,

            };
            return response;
        }
        catch (Exception)
        {

            throw;
        }

        
    }


}
