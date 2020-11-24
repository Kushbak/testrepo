using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using FinanceManagmentApplication.Models.UserModels;
using FinanceManagmentApplication.Services;
using FinanceManagmentApplication.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using FinanceManagmentApplication.Models.ErrorModels;
using System.Security.Claims;

namespace FinanceManagmentApplication.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<User> _userManager;
        private IUserService UserService { get; }
        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userManager = userManager;
            UserService = userService;
        }
        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult<List<UserIndexModel>>> Index()
        {
            return await UserService.GetAll();
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserIndexModel>> Index(int Id)
        //{
        //    return await UserService.GetById(Id);
        //}
        [HttpGet]
        [Authorize]
        [Route("GetUser")]
        public async Task<ActionResult<UserIndexModel>> GetUser(UserIndexModel model)
        {
            var Result = await UserService.GetUser(model, User);
            return Ok(Result);
        }


        [HttpPut]
        [Authorize]
        [Route("Edit")]
        public async Task<IActionResult> Edit(EditUserModel model)
        {
            var Result = await UserService.Edit(model, User);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result);
            }
            return Ok(Result);
        }

        [HttpPut]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordUserModel model)
        {
            var Result = await UserService.ChangePassword(model, User);
            if (Result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result);
            }
            return Ok(Result);
        }

    }
}
