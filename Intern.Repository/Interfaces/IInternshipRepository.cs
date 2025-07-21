using Intern.Models;
using Intern.Models.ViewModels;

namespace Intern.Repository.Interfaces
{
    public interface IInternshipRepository
    {
        Internship GetInternshipById(int internshipId);
        bool Upsert(Internship internship);
        bool AddInternship(Internship internship);
        bool UpdateInternship(Internship internship);
        bool RemoveInternship(int internshipId);
        List<Internship> GetAll(int pageNumber, int pageSize);
        int GetTotalCount();
    }
}
