using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Model;

public class Faculties
{
    [Key]
    public int FacultieId { get; set; }

    public string Name { get; set; }


    public int UniversitysId { get; set; }
    public Universitys Universitys { get; set; }

    public int DeansId { get; set; }
    public Deans Deans{ get; set; }

    public IQueryable<Groups> Groups { get; set; }

    public IQueryable<Professions>  Professions{ get; set; }

}
