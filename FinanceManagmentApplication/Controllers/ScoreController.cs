using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.ScoreModel;
using FinanceManagmentApplication.Models.WebModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApplication.Controllers
{
    [Route("Score")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private IScoreService ScoreService { get; set; }

        public ScoreController(IScoreService scoreService)
        {
            ScoreService = scoreService;
        }
        /// <summary>
        /// Get all scores
        /// </summary>
       
        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult<List<ScoreIndexModel>>> Index()
        {
            return await ScoreService.GetAll();
        }


        /// <summary>
        /// Get score details information
        /// </summary>
        [HttpGet]
        [Route("ScoresDetails")]
        public async Task<ActionResult<List<ScoreDetailsModel>>> DetailsIndex()
        {
            return await ScoreService.GetAllDetails();
        }



        /// <summary>
        /// Get create model for score(only for a back developers)
        /// </summary>
        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<ScoreCreateModel>> Create()
        {
            return await ScoreService.GetCreateModel();
        }



        /// <summary>
        /// Create a score
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///         "code": "123456789",
            ///  "paymentTypeId": 1,
        ///        "name": "NewScore",
        ///
        ///                            </remarks>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ScoreCreateModel model)
        {
            var Result = await ScoreService.Create(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status200OK, Result);
            }

            return Ok(Result);
        }


        /// <summary>
        /// Get edit model for score(only for a back developers)
        /// </summary>
        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult<ScoreEditModel>> Edit(int Id)
        {
            return Ok(await ScoreService.GetEditModel(Id));
        }


        /// <summary>
        /// Edit a score
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///     "id":4
        ///         "code": "123456789",
        ///         "paymentTypeId": 1,
        ///         "name": "NewScoreEdit",
        ///
        ///      </remarks>
        [HttpPut]
        [Route("Edit")]
        public async Task<ActionResult<ScoreEditModel>> Edit(ScoreEditModel model)
        {

            var Result = await ScoreService.Edit(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status200OK, Result);
            }

            return Ok(Result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<ScoreEditModel>> Delete(int? Id)
        {
            var Result = await ScoreService.Delete(Id.Value);
            return Ok(Result);
        }


    }
}
