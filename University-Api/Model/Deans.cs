using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityApi.Model;

public class Deans
{
    [Key]
    public int DeanId { get; set; }
    [Required]

    public string Email{ get; set; }
    [Required]
    public string Name { get; set; }
    [Required]

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }= "********";
    public Faculties Faculties{ get; set; }

}
