using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService ProjectService { get; }

        public ProjectController(IProjectService projectService)
        {
            ProjectService = projectService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProjectIndexModel>>> Get()
        {  
            return await ProjectService.GetAll();
        }
        [HttpPost]
        public async Task<IActionResult> Post(ProjectCreateModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = StatusEnum.Error, Message = "Project already exists!" });
            }

            await ProjectService.Create(model);

            return Ok();
            
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await ProjectService.Delete(id);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProjectIndexModel model)
        {
            await ProjectService.Update(model);
            return Ok();
        }
    }
}
