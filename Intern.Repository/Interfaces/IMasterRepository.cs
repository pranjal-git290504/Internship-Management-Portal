using Intern.Models;

namespace Intern.Repository.Interfaces
{
    public interface IMasterRepository
    {
        List<College> GetColleges();
        List<ApplicationStatus> GetApplicationStatuses();
    }
}
