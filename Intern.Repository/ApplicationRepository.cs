using Dapper;
using Intern.Models;
using Intern.Models.ViewModels;
using Intern.Repository.Interfaces;
using Intern.Resources;
using Intern.Utility;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Intern.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppSetting _appSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRepository"/> class.
        /// </summary>
        /// <param name="appSetting">The application setting options.</param>
        public ApplicationRepository(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public Application GetApplicationById(int applicationId)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.QueryFirstOrDefault<Application>(ApplicationResource.GetApplicationById, new { ApplicationId = applicationId });
            }
        }

        public bool AddApplication(Application application)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(ApplicationResource.InsertApplication, application);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public bool UpdateApplication(Application application)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(ApplicationResource.UpdateApplication, application);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public bool DeleteApplication(int applicationId)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(ApplicationResource.DeleteApplication, new { ApplicationId = applicationId });
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public List<ApplicationViewModel> GetAll(int pageNumber, int pageSize)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                var offset = (pageNumber - 1) * pageSize;
                return con.Query<ApplicationViewModel>(ApplicationResource.GetAll, new { Offset = offset, PageSize = pageSize }).ToList();
            }
        }

        public int GetTotalCount()
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.ExecuteScalar<int>(ApplicationResource.GetTotalCount);
            }
        }
    }
}
