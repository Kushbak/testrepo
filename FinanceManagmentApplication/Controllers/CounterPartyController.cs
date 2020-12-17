using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.WebModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinanceManagmentApplication.Models.ErrorModels;

namespace FinanceManagmentApplication.Controllers
{
    [Route("CounterParty")]
    [ApiController]
    public class CounterPartyController : ControllerBase
    {
        private ICounterPartyService CounterPartyService { get; }

        public CounterPartyController(ICounterPartyService counterPartyService)
        {
            CounterPartyService = counterPartyService;
        }
        /// <summary>
        /// Get create model for a counterparty(only for a backend testing!!!)
        /// </summary>
        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<CounterPartyCreateModel>> Create()
        {
            return await CounterPartyService.GetCreateModel();
        }
        /// <summary>
        /// Create a counterparty
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
       ///         "name": "lalala",
       ///         "isCompany": true,
       ///         "userId": null}
    ///
    ///                            </remarks>
    /// <param name="model"></param>
    /// <returns>A token</returns>
    /// <response code="200">Returns the token</response>
    /// <response code="401">Wrong username or password</response>
    /// <response code="400">Model is null</response>
    [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CounterPartyCreateModel model)
        {
            var Result = await CounterPartyService.Create(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status200OK, Result);
            }

            return Ok(Result);
        }
        /// <summary>
        /// Get edit model for a counterparty(only for a backend testing!!!)
        /// </summary>
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
                return StatusCode(StatusCodes.Status200OK, new Response { Status=StatusEnum.Error, Message="Нет контрагента с таким Id"});
            }
        }

        /// <summary>
        /// Edit a counterparty
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT
        ///     {
///                 "id": 3,
///                 "name": "Lalalala",
///                 "isCompany": true,
///                 "userId": null
///             }  </remarks>
      [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(CounterPartyEditModel model)
        {
            var Result = await CounterPartyService.Edit(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status200OK, Result);
            }

            return Ok(Result);

        }

        /// <summary>
        /// Get all counterparty
        /// </summary>
        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult<List<CounterPartyIndexModel>>> Index()
        {
            return await CounterPartyService.GetAll();
        }
        /// <summary>
        /// Delete a counterparty
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     DELETE 
        ///     {
        ///         "id": 3
        ///     }
        /// </remarks>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Result = await CounterPartyService.Delete(Id);
            if (Result.Status == StatusEnum.Accept)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status200OK, Result);
        }
    }
}
