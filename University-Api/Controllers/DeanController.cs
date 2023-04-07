using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static UniversityApi.Controllers.ManagerController;
using System.Data;
using System.Security.Claims;
using UniversityApi.Data;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Model;
using UniversityApi.Services.CryptoService;

namespace UniversityApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DeanController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ICryptoService _cryptoService;

    public DeanController(ApplicationDbContext applicationDbContext, ICryptoService cryptoService)
    {
        _applicationDbContext = applicationDbContext;
        _cryptoService = cryptoService;
    }
    [Authorize(Roles = "Dean")]

    [HttpPost("create-professions")]
    public async Task<IActionResult> CreateProfesstions(ProfessionDTO  professionDTO)
    {
        try
        {

            var facId = GetFacultieId(User.Identities.First());
            var facuiltie = await _applicationDbContext.Faculties.FindAsync(facId);
            Professions professions = new() {Name=professionDTO.Name };
            if(facuiltie.Professions is null) {
                facuiltie.Professions = new List<Professions> { professions };
            }
            else
            {
                facuiltie.Professions.Add(professions);
            }
            await _applicationDbContext.SaveChangesAsync();
            return Ok(new
            {
                profName = professions.Name
            }) ;
            
        }
        catch (Exception)
        {

            throw;
        }

    }

    [HttpPost("generateToken/student")]
    public async Task<IActionResult> CreateStudentToken(GroupDTO groupDTO)
    {
        try
        {

            var yearCome = DateTime.Now.Year - groupDTO.NumberCourse;
          var group= await _applicationDbContext.Groups
                .Where(e => (e.YearCome == yearCome) && (e.NumberGroup == groupDTO.NumberGroup) && (e.Professions.Name == groupDTO.ProfessionName))
                .FirstOrDefaultAsync();

      var studentTokenRegistration = _cryptoService.EncryptSecretString(new PayloadStudent{GroupId=group.GroupId, Role="Student" });

            return Ok(new
            {
                StudentAuthorizationToken = studentTokenRegistration
            });

        }
        catch (Exception)
        {

            throw;
        }

    }

    [HttpPost("create-group")]
    public async Task<IActionResult> CreateGroup(GroupDTO  groupDTO)
    {
        try
        {
            var group = new Groups {
                NumberGroup = groupDTO.NumberGroup,
                YearCome = DateTime.Now.Year-groupDTO.NumberCourse,

            };


            var facId = GetFacultieId(User.Identities.First());
            var facuiltie = await _applicationDbContext.Faculties.FindAsync(facId);
            var professions = await _applicationDbContext.Professions.Include(e=>e.Groups).Where(e=>e.Name==groupDTO.ProfessionName).FirstOrDefaultAsync();
            professions.Groups.Add(group);
             facuiltie.Groups.Add(group);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(new
            {
                profName = professions.Name
            });

        }
        catch (Exception)
        {

            throw;
        }

    }

    private int GetFacultieId(ClaimsIdentity claimsIdentity)
    {
        try
        {

            var reqString = claimsIdentity.Claims.Where(c => c.Type == "FacuiltieId").FirstOrDefault();
            
            return int.Parse(reqString.Value);
        }
        catch (Exception e)
        {

            throw e;
        }
    }
    public record class PayloadStudent
    {
        public int GroupId { get; set; }
        public string Role { get; set; }
    }
    public record class ProfessionDTO
    {
        public string Name { get; set; }
    }
    public record class GroupDTO
    {
        public int NumberCourse { get; set; }
        public int NumberGroup { get; set; }
        public string ProfessionName { get; set; }

    }
}
