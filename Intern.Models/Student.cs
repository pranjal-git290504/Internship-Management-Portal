using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intern.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int FkUserId { get; set; }
        [JsonIgnore]
        public User User{ get; set; }
        public int FkCollegeId { get; set; }
        public string Course { get; set; }
        public int YearOfStudy { get; set; }
        public string? ResumeBase64 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
