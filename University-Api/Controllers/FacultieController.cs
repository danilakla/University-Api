using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Services.FacultieService;

namespace UniversityApi.Controllers;
[Authorize]
public class FacultieController : ControllerBase
{
    private readonly IFacultieService _facultieService;

    public FacultieController(IFacultieService facultieService)
    {
        _facultieService = facultieService;
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
}
