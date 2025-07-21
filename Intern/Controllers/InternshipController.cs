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
    public class InternshipController : ControllerBase
    {
        private readonly AppSetting _appSetting;
        private readonly IInternshipRepository _internshipRepository;


        public InternshipController(IOptions<AppSetting> appSetting, IInternshipRepository internshipRepository)
        {
            _appSetting = appSetting.Value;
            _internshipRepository = internshipRepository;
        }

        [Route("GetAll")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            PaginatedResult<Internship> response = new();
            try
            {
                var internships = _internshipRepository.GetAll(pageNumber, pageSize);
                response.Data = internships;
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.Success = true;
                response.TotalCount = _internshipRepository.GetTotalCount();
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the error here
                return Ok(response);
            }
        }

        [Route("Upsert")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Upsert(Internship internship)
        {
            ApiResponse<bool> apiResponse = new();
            var result = _internshipRepository.Upsert(internship);
            apiResponse.Success = result;
            apiResponse.Data = result;
            apiResponse.Message = result ? $"{(internship.Id > 0 ? "Updated" : "Saved")} Succesfully" : "Error while saving the record";
            return Ok(apiResponse);
        }

        [Route("Remove/{id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Remove(int id)
        {
            ApiResponse<bool> apiResponse = new();
            var result = _internshipRepository.RemoveInternship(id);
            apiResponse.Success = result;
            apiResponse.Data = result;
            apiResponse.Message = result ? "Removed Succesfully" : "Error while deleting the record";
            return Ok(apiResponse);
        }
    }
}
