using UniversityApi.Dto;
using UniversityApi.Model;

namespace UniversityApi.Grpc.Client;

public interface ITeacherService
{
    Task<PayloadTeacherDto> InitTeacher(CreateTeacherDTO createManagerDTO);
    Task<PayloadTeacherDto> GetTeacherInfo(string email);

}
