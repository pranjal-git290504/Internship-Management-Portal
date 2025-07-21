using Intern.Models;
using Intern.Models.ViewModels;
using Intern.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Intern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly AppSetting _appSetting;
        private readonly IStudentRepository _studentRepository;


        public StudentController(IOptions<AppSetting> appSetting, IStudentRepository studentRepository)
        {
            _appSetting = appSetting.Value;
            _studentRepository = studentRepository;
        }

        [Route("GetAll")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            PaginatedResult<StudentViewModel> response = new();
            try
            {
                var students = _studentRepository.GetAll(pageNumber, pageSize);
                response.Data = students;
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.Success = true;
                response.TotalCount = _studentRepository.GetTotalCount();
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the error here
                return Ok(response);
            }
        }

        //[Route("AddStudent")]
        //[HttpPost]
        //public IActionResult AddStudent(Student student)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }
}
