using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterPartyController : ControllerBase
    {
        private ICounterPartyService CounterPartyService { get; }

        public CounterPartyController(ICounterPartyService counterPartyService)
        {
            CounterPartyService = counterPartyService;
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<CounterPartyCreateModel>> Create()
        {
            return await CounterPartyService.GetCreateModel();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CounterPartyCreateModel model)
        {
            var Result = await CounterPartyService.Create(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result);
            }

            return Ok(Result);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult<CounterPartyEditModel>> Edit(int Id)
        {
            try
            {
                var model = await CounterPartyService.GetEditModel(Id);
                return model;
            }
            catch (NotEntityFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status=StatusEnum.Error, Message="Нет контрагента с таким Id"});
            }
        }


        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(CounterPartyEditModel model)
        {
            var Result = await CounterPartyService.Edit(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result);
            }

            return Ok(Result);

        }


        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult<List<CounterPartyIndexModel>>> Index()
        {
            return await CounterPartyService.GetAll();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Result = await CounterPartyService.Delete(Id);
            if (Result.Status == StatusEnum.Accept)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, Result);
        }
    }
}
