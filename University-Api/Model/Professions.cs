using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Model;

public class Professions
{
    [Key]
    public int ProfessionId { get; set; }
    public string Name { get; set; }

    public int? FacultiesId { get; set; }

    public Faculties? Faculties { get; set; }
    public IQueryable<Groups>  Groups{ get; set; }
}
