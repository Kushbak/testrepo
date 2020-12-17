using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinanceManagmentApplication.Models.WebModels;

namespace FinanceManagmentApplication.Controllers
{
    [Route("Project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService ProjectService { get; }

        public ProjectController(IProjectService projectService)
        {
            ProjectService = projectService;
        }
        /// <summary>
        /// Get all project
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ProjectIndexModel>>> Get()
        {  
            return await ProjectService.GetAll();
        }


        /// <summary>
        /// Create a project
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     { 
        ///     "Name" : "Новый проект"  
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Post(ProjectCreateModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status200OK, new Response { Status = StatusEnum.Error, Message = "Project already exists!" });
            }

            await ProjectService.Create(model);

            return Ok();
            
        }


        /// <summary>
        /// Delete a project
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE
        ///     { 
        ///     "Id" : 4 
        ///     }
        /// </remarks>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await ProjectService.Delete(id);

            return Ok();
        }



        /// <summary>
        /// Edit a project
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     PUT
        ///     { 
        ///     "Id" : 4 ,
        ///     "Name" : "Другие проекты"
        ///     }    
        ///     </remarks>
        [HttpPut]
        public async Task<IActionResult> Put(ProjectIndexModel model)
        {
            await ProjectService.Update(model);
            return Ok();
        }
    }
}
