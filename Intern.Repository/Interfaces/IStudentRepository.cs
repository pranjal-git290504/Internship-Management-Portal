using Intern.Models;
using Intern.Models.ViewModels;

namespace Intern.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Student GetStudentById(int studentId);
        bool AddStudent(Student student);
        bool UpdateStudent(Student student);
        bool DeleteStudent(int studentId);
        List<StudentViewModel> GetAll(int pageNumber, int pageSize);
        int GetTotalCount();
    }
}
