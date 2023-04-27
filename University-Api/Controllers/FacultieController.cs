using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Services.FacultieService;

namespace UniversityApi.Controllers;
[Authorize]
public class FacultieController : ControllerBase
{
    private readonly IFacultieService _facultieService;
    private readonly ApplicationDbContext _applicationDbContext;

    public FacultieController(IFacultieService facultieService, ApplicationDbContext applicationDbContext)
    {
        _facultieService = facultieService;
        _applicationDbContext = applicationDbContext;
    }
    [HttpGet("/get-faculties")]
    public async Task<List<string>> GetFaculties()
    {
        try
        {
            var faculties = await _facultieService.GetFacultiesAsync();
            return faculties;
        }
        catch (Exception)
        {

            throw;
        }
    }


    [HttpGet("/get-facultiByName/{name}")]
    public async Task<IActionResult> GetFaculties(string name)
    {
        try
        {
            var facultieId = await _applicationDbContext.Faculties.Where(e=>e.Name==name).FirstOrDefaultAsync();
            return Ok(new { Id= facultieId.FacultieId });
        }
        catch (Exception)
        {

            throw;
        }
    }
}
