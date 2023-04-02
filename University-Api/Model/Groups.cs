using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Model;

public class Groups
{
    [Key]
    public int GroupId { get; set; }
    [Required]

    public int NumberGroup { get; set; }
    [Required]

    public int YearCome { get; set; }

    public int FacultiesId { get; set; }
    public Faculties Faculties { get; set; }

    public IQueryable<Students> Students { get; set; }


    public int? ProfessionsId { get; set; }
    public Professions?  Professions{ get; set; }

}
