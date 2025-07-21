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
    public class ApplicationController : ControllerBase
    {
        private readonly AppSetting _appSetting;
        private readonly IApplicationRepository _applicationRepository;


        public ApplicationController(IOptions<AppSetting> appSetting, IApplicationRepository applicationRepository)
        {
            _appSetting = appSetting.Value;
            _applicationRepository = applicationRepository;
        }

        [Route("GetAll")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            PaginatedResult<ApplicationViewModel> response = new();
            try
            {
                var applications = _applicationRepository.GetAll(pageNumber, pageSize);
                response.Data = applications;
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.Success = true;
                response.TotalCount = _applicationRepository.GetTotalCount();
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the error here
                return Ok(response);
            }
        }
    }
}
