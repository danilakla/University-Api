using System.ComponentModel.DataAnnotations;

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
    public Managers Managers { get; set; }

    public IQueryable<Faculties> Faculties { get; set; }
}
