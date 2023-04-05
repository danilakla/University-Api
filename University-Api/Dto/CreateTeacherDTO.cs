namespace UniversityApi.Dto;

public class CreateTeacherDTO
{

    public string Name { get; set; }

    public string LastName { get; set; }
    public string Email { get; set; }

    public int UniversityId { get; set; }
}
