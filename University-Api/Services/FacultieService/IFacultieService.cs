using UniversityApi.Model;

namespace UniversityApi.Services.FacultieService;

public interface IFacultieService
{
    public Task AddFacultie(string nameOfFacultie, int universityId);
    public Task<Faculties> FindByName(string nameOfFacultie);

}
