using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UniversityApi.Data;
using UniversityApi.Model;

namespace UniversityApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ManagerController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ManagerController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    [Authorize(Roles = "Manager")]

    [HttpPost("create-facultie")]
    public async Task<IActionResult> CreateFacultie(FacultieDto facultieDto)
    {

        var facultie= new Faculties() { 
        Name= facultieDto.FacultieName
        };

        var req = (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == "UniversityId").FirstOrDefault();


        var university=await _applicationDbContext.Universitys.Include(e => e.Faculties).Where(e => e.UniversityId == int.Parse(req.Value)).FirstOrDefaultAsync();
        university.Faculties.Add(facultie);
        await _applicationDbContext.SaveChangesAsync();
        return Ok(new
        {
            FacultieName = facultie.Name
        }) ;
    }
   public record class FacultieDto
    {
        public string FacultieName { get; set; }
    }
}
