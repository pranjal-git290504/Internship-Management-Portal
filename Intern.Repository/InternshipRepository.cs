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
    public class InternshipRepository : IInternshipRepository
    {
        private readonly AppSetting _appSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternshipRepository"/> class.
        /// </summary>
        /// <param name="appSetting">The application setting options.</param>
        public InternshipRepository(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public Internship GetInternshipById(int internshipId)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.QueryFirstOrDefault<Internship>(InternshipResource.GetInternshipById, new { InternshipId = internshipId });
            }
        }

        public bool Upsert(Internship internship)
        {

            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(InternshipResource.UpsertInternship, internship);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public bool AddInternship(Internship internship)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(InternshipResource.InsertInternship, internship);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public bool UpdateInternship(Internship internship)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(InternshipResource.UpdateInternship, internship);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public bool RemoveInternship(int internshipId)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(InternshipResource.RemoveInternship, new { Id = internshipId, UpdatedAt = DateTime.UtcNow });
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public List<Internship> GetAll(int pageNumber, int pageSize)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                var offset = (pageNumber - 1) * pageSize;
                return con.Query<Internship>(InternshipResource.GetAll, new { Offset = offset, PageSize = pageSize }).ToList();
            }
        }

        public int GetTotalCount()
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.ExecuteScalar<int>(InternshipResource.GetTotalCount);
            }
        }
    }
}
