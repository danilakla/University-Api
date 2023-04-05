using TeacherMicroservice.Protos;
using UniversityApi.Dto;
using UniversityApi.Model;

namespace UniversityApi.Grpc.Client;

public class TeacherService:ITeacherService
{
    private readonly Teacher.TeacherClient _teacherClient;

    public TeacherService(Teacher.TeacherClient teacherClient)
    {
        _teacherClient = teacherClient;
    }
    public  async Task<PayloadTeacherDto> InitTeacher(CreateTeacherDTO  createTeacherDTO)
    {
        try
        {
            var req = new TeacherRequest { Email = createTeacherDTO.Email, LastName = createTeacherDTO.LastName, UniversityId = createTeacherDTO.UniversityId, Name = createTeacherDTO.Name };
           var res= await _teacherClient.InitTeacherAsync(req);

            return  new PayloadTeacherDto { TeacherId=res.TeacherId, UniversityId=res.UniversityId } ;

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<PayloadTeacherDto> GetTeacherInfo(string email)
    {
        try
        {
            var res = await _teacherClient.GetTeacherInfoAsync(new EmailTeacher { Email = email });

            return new PayloadTeacherDto { TeacherId = res.TeacherId, UniversityId = res.UniversityId };
        }
        catch (Exception)
        {

            throw;
        }

    }

}
