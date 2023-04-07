using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityApi.Model;

public class Faculties
{
    [Key]
    public int FacultieId { get; set; }

    public string Name { get; set; }


    public int UniversitysId { get; set; }
    [JsonIgnore]

    public Universitys Universitys { get; set; }

    public int? DeansId { get; set; }
    [JsonIgnore]

    public Deans Deans{ get; set; }

    public List<Groups> Groups { get; set; } = new();

    public List<Professions>  Professions{ get; set; }

}
