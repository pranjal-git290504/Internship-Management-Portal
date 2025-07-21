using Intern.Models;
using Intern.Models.ViewModels;

namespace Intern.Repository.Interfaces
{
    public interface IApplicationRepository
    {
        Application GetApplicationById(int applicationId);
        bool AddApplication(Application application);
        bool UpdateApplication(Application application);
        bool DeleteApplication(int applicationId);
        List<ApplicationViewModel> GetAll(int pageNumber, int pageSize);
        int GetTotalCount();
    }
}
