using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.ScoreModel;
using FinanceManagmentApplication.Services.Contracts;
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

        [HttpGet]
        [Route("Scores")]
        public async Task<ActionResult<List<ScoreIndexModel>>> Index()
        {
            return await ScoreService.GetAll();
        }

        [HttpGet]
        [Route("ScoresDetails")]
        public async Task<ActionResult<List<ScoreDetailsModel>>> DetailsIndex()
        {
            return await ScoreService.GetAllDetails();
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<ScoreCreateModel>> Create()
        {
            return await ScoreService.GetCreateModel();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ScoreCreateModel model)
        {
            var Result = await ScoreService.Create(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result);
            }

            return Ok(Result);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult<ScoreEditModel>> Edit(int Id)
        {
            return Ok(await ScoreService.GetEditModel(Id));
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<ActionResult<ScoreEditModel>> Edit(ScoreEditModel model)
        {

            var Result = await ScoreService.Edit(model);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result);
            }

            return Ok(Result);
        }




    }
}
