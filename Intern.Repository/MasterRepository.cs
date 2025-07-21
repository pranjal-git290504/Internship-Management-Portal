using Dapper;
using Intern.Models;
using Intern.Repository.Interfaces;
using Intern.Resources;
using Intern.Utility;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Intern.Repository
{
    public class MasterRepository : IMasterRepository
    {
        private readonly AppSetting _appSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="appSetting">The application setting options.</param>
        public MasterRepository(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public List<College> GetColleges()
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.Query<College>(MasterResource.GetAllColleges).ToList();
            }
        }

        public List<ApplicationStatus> GetApplicationStatuses()
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.Query<ApplicationStatus>(MasterResource.GetAllApplicationStatuses).ToList();
            }
        }
    }
}
