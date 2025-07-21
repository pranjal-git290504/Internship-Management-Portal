using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intern.Models.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public int FkUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int FkCollegeId { get; set; }
        public string CollegeName { get; set; }
        public string Course { get; set; }
        public int YearOfStudy { get; set; }
        public string? ResumeBase64 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
