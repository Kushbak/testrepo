using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Services;
using FinanceManagmentApplication.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private IOperationService OperationService { get; }

        public OperationController(IOperationService operationService)
        {
            OperationService = operationService;
        }



        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult<List<OperationDetailsModel>>> Index()
        {
            return await OperationService.GetAll();
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(OperationCreateModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = StatusEnum.Error, Message = "Ничего не оптравлено на сервер!" });
            }

            var Result = await OperationService.Create(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result );
            }
            return Ok(Result);

        }
        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<OperationCreateModel>> Create()
        {
            return await OperationService.GetCreateModel();
        }
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
        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult<OperationEditModel>> Edit(int Id)
        {
            return await OperationService.GetEditModel(Id);
        }

    }
}
