using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityApi.Model;

public class Universitys
{
    [Key]
    public int UniversityId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    public int ManagersId{ get; set; }

    [Required]
    [JsonIgnore]

    public Managers Managers { get; set; }

    public List<Faculties> Faculties { get; set; }
}
