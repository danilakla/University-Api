using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityApi.Model;

public class Professions
{
    [Key]
    public int ProfessionId { get; set; }
    public string Name { get; set; }

    public int? FacultiesId { get; set; }
    [JsonIgnore]


    public Faculties? Faculties { get; set; }
    public List<Groups> Groups { get; set; } = new();
}
