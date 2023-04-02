using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Model;

public class Students
{
    [Key]
    public int StudentId { get; set; }
    public string  Name { get; set; }
    public string SecondName { get; set; }
    public string Email { get; set; }

    public int GroupsId { get; set; }
    public Groups Groups { get; set; }

}
