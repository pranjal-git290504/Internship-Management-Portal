using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intern.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int FkStudentId { get; set; }
        public int FkInternshipId { get; set; }
        public int FkApplicationStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
