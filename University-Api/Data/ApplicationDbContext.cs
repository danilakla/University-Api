using Microsoft.EntityFrameworkCore;
using UniversityApi.Model;

namespace UniversityApi.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {

    }

    public DbSet<Managers>  Managers{ get; set; }
    public DbSet<Universitys> Universitys { get; set; }
    public DbSet<Faculties> Faculties { get; set; }
    public DbSet<Deans> Deans { get; set; }
    public DbSet<Groups> Groups { get; set; }
    public DbSet<Students> Students { get; set; }
    public DbSet<Professions> Professions{ get; set; }


}

