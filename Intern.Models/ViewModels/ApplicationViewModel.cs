using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intern.Models.ViewModels
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        public int FkStudentId { get; set; }
        public string Course { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int FkInternshipId { get; set; }
        public string InternshipTitle { get; set; }
        public int FkApplicationStatusId { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
