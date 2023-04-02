using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApi.Model;

public class Managers
{
    [Key]
    public int ManagerId { get; set; }
    [Required]
    public string Name { get; set; }

    [Required]

    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    

    public string PhoneNumber{ get; set; }

    public Universitys  Universitys{ get; set; }
}
