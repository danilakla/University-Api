using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UniversityApi.Data;
using UniversityApi.Model;
using UniversityApi.Services.CryptoService;
using UniversityApi.Services.FacultieService;

namespace UniversityApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ManagerController : ControllerBase
{
    private readonly IFacultieService _facultieService;
    private readonly ICryptoService _cryptoService;

    public ManagerController(IFacultieService facultieService, ICryptoService cryptoService)
    {
        _facultieService = facultieService;
        _cryptoService = cryptoService;
    }

    private int GetUniversityId(ClaimsIdentity claimsIdentity)
    {
        try
        {
            var reqString= claimsIdentity.Claims.Where(c => c.Type == "UniversityId").FirstOrDefault();
            return int.Parse(reqString.Value);
        }
        catch (Exception e)
        {

            throw e;
        }
    }
    
    [Authorize(Roles = "Manager")]

    [HttpPost("create-facultie")]
    public async Task<IActionResult> CreateFacultie(FacultieDto facultieDto)
    {
        try
        {
 
            var universityId = GetUniversityId(User.Identities.First());

            await _facultieService.AddFacultie(facultieDto.FacultieName, universityId);
            return Ok(new
            {
                FacultieName = facultieDto.FacultieName
            });
        }
        catch (Exception)
        {

            throw;
        }
   
    }

    [Authorize(Roles = "Manager")]
    [HttpPost("generateTokend/dean")]
    public async Task<IActionResult> GenerateTokenDean(FacultieDto facultieDto)
    {
        try
        {


            var universityId = GetUniversityId(User.Identities.First());

            var facultie = await _facultieService.FindByName(facultieDto.FacultieName);
            var payload = new DeanTokenRegistraion { FacultieId = facultie.FacultieId, Role = "Dean", UniversityId = universityId };
            var deanTokenRegistration =  _cryptoService.EncryptSecretString(payload);
            return Ok(new { 
            DeanToken=deanTokenRegistration,
            
            });
        }
        catch (Exception)
        {

            throw;
        }
    }

    [Authorize(Roles = "Manager")]
    [HttpPost("generateTokend/teacher")]
    public async Task<IActionResult> GenerateTokenTeacher()
    {
        try
        {


            var universityId = GetUniversityId(User.Identities.First());

            var payload = new TeacherTokenRegistraion { Role = "Teacher", UniversityId = universityId };
            var TeacherTokenRegistration = _cryptoService.EncryptSecretString(payload);
            return Ok(new
            {
                TeacherToken = TeacherTokenRegistration,

            });
        }
        catch (Exception)
        {

            throw;
        }
    }
    public record class FacultieDto
    {
        public string FacultieName { get; set; }
    }
    public record class DeanTokenRegistraion
    {
        public string Role { get; set; }
        public int UniversityId { get; set; }
        public int FacultieId { get; set; }
    }

    public record class TeacherTokenRegistraion
    {
        public string Role { get; set; }
        public int UniversityId { get; set; }
    }


}
