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
    public class StudentRepository : IStudentRepository
    {
        private readonly AppSetting _appSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentRepository"/> class.
        /// </summary>
        /// <param name="appSetting">The application setting options.</param>
        public StudentRepository(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public Student GetStudentById(int studentId)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.QueryFirstOrDefault<Student>(StudentResource.GetStudentById, new { StudentId = studentId });
            }
        }

        public bool AddStudent(Student student)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(StudentResource.InsertStudent, student);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public bool UpdateStudent(Student student)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(StudentResource.UpdateStudent, student);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public bool DeleteStudent(int studentId)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    var result = con.Execute(StudentResource.DeleteStudent, new { StudentId = studentId });
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return false;
        }

        public List<StudentViewModel> GetAll(int pageNumber, int pageSize)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                var offset = (pageNumber - 1) * pageSize;
                return con.Query<StudentViewModel>(StudentResource.GetAll, new { Offset = offset, PageSize = pageSize }).ToList();
            }
        }

        public int GetTotalCount()
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.ExecuteScalar<int>(StudentResource.GetTotalCount);
            }
        }
    }
}
