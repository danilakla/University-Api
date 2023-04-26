using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UniversityApi.Data;
using UniversityApi.Dto;
using UniversityApi.Model;

namespace UniversityApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfessionController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProfessionController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet("/get-professions")]
    public async Task<List<Professions>> GetProfessions()
    {
        try
        {
            var professions = await _applicationDbContext.Professions.ToListAsync();
            return professions;
        }
        catch (Exception)
        {

            throw;
        }
    }






}
