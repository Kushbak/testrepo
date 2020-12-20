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
using FinanceManagmentApplication.BL.Services;
using FinanceManagmentApplication.BL.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using FinanceManagmentApplication.Models.ErrorModels;
using System.Security.Claims;
using FinanceManagmentApplication.Models.WebModels;

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
        /// <summary>
        /// Get all user(just for testing).
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///
        /// </remarks>
        /// <returns>List of all user</returns>
        /// <response code="200">Return all user</response>
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
        



        /// <summary>
        /// Get a user in user personal page.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / Authorize: brear token
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A token</returns>
        /// <response code="200">Returns the user's information</response>
        /// <response code="401">Wrong token</response>
        
        [Authorize]
        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult<UserIndexModel>> GetUser([FromQuery] UserIndexModel model)
        {
            var Result = await UserService.GetUser(model, User);
            if(Result==null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, Result);
            }
            return Result;
        }


        /// <summary>
        ///Edit information about user
        ///Username is should be unique
        ///Можно изменять информацию выборочно
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / Authorize: brearer token
        ///     {
        ///    "username": "jusupbekova",
        ///    "email": "Aidana@gmail.com",
        ///    "surname": "Jusupbekova",
        ///    "name": "Aidana"
        ///}
    ///
    /// </remarks>
    /// <param name="model"></param>
    /// <returns>A token</returns>
    /// <response code="200">Request is success</response>
    /// <response code="401">Wrong token</response>
        [HttpPut]
        [Authorize]
        [Route("Edit")]
        public async Task<IActionResult> Edit(EditUserModel model)
        {
            try
            {
                var Result = await UserService.Edit(model, User);
                return Ok(Result);
            }
            catch (UserEditException e)
            {
                return Ok(new Response { Status = StatusEnum.Error, Message = e.Message });
            }
            catch
            {
                return Ok(new Response { Status = StatusEnum.Error, Message = "Неизвестная ошибка"});
            }

            
        }



        /// <summary>
        ///Edit user password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / Authorize: brearer token
        ///     {
        ///     "Oldpassword":"Aidana!1",
        ///     "Newpassword":"Aidana!1new"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A token</returns>
        /// <response code="200">Request is success</response>
        /// <response code="401">Wrong token</response>
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
