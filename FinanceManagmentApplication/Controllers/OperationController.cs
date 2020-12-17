using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.BL.Services;
using FinanceManagmentApplication.BL.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinanceManagmentApplication.Models.WebModels;

namespace FinanceManagmentApplication.Controllers
{
    [Route("Operation")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private IOperationService OperationService { get; }

        public OperationController(IOperationService operationService)
        {
            OperationService = operationService;
        }


        /// <summary>
        /// All operations - Type of finance transactions
        /// </summary>
        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult<List<OperationDetailsModel>>> Index()
        {
            return await OperationService.GetAll();
        }

        /// <summary>
        /// Create a operation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {        
        ///     "name": "Какие-то доходы",
        ///     "operationTypeId":1
        ///     }                           </remarks>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(OperationCreateModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status200OK, new Response { Status = StatusEnum.Error, Message = "Ничего не отправлено на сервер!" });
            }

            var Result = await OperationService.Create(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status200OK, Result );
            }
            return Ok(Result);

        }
        /// <summary>
        /// Get create model for back testing
        /// </summary>
        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<OperationCreateModel>> Create()
        {
            return await OperationService.GetCreateModel();
        }


        /// <summary>
        /// Delete a operation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {        
        ///     "id": 24
        ///     }
        ///     </remarks>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var Result = await OperationService.Delete(id);

            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result);
            }
            return Ok(Result);
        }


        /// <summary>
        /// Edit a operation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT 
        ///     {        
        ///     "id": 12,
        ///     "Name" : "Измененный доход",
        ///     "OperationTypeId":1
        ///     }
        ///</remarks>
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(OperationEditModel model)
        {
            var Result = await OperationService.Edit(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result);
            }
            return Ok(Result);
        }

        /// <summary>
        /// Get edit model for a back testing
        /// </summary>
        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult<OperationEditModel>> Edit(int Id)
        {
            return await OperationService.GetEditModel(Id);
        }

    }
}
