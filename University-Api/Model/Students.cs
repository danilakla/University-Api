using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityApi.Model;

public class Students
{
    [Key]
    public int StudentId { get; set; }
    public string  Name { get; set; }
    public string SecondName { get; set; }
    public string Email { get; set; }

    public int GroupsId { get; set; }
    [JsonIgnore]

    public Groups Groups { get; set; }

}
