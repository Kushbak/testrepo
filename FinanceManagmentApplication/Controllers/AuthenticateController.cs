using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.WebModels;

namespace FinanceManagmentApplication.Controllers
{
    [Route("Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }

        /// <summary>
        /// Authorize a user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / Authorize a user
        ///     {
        ///    
        ///        "Username": "Employee",
        ///        "Password": "Password!1"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A token</returns>
        /// <response code="200">Returns the token</response>
        /// <response code="401">Wrong username or password</response>
        /// <response code="400">Model is null</response>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var Result = await authenticateService.Login(model);
            if (Result != null)
            {
                return Ok(new
                {
                    token = Result.token,
                    expiration = Result.expiration
                });
            }
            return Unauthorized();
        }


        /// <summary>
        /// Register a user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / Register a user
        ///     {
        ///      
        ///         "UserName": "jusupbekova",
        ///         "Password":"Password!1",
        ///         "Email" :"Aidana@gmail.com",
        ///         "Surname":"Jusupbekova",
        ///         "Name":"Aidana
    ///        }
    ///
    /// 
    /// </remarks>
    /// <param name="model"></param>
    /// <returns>success result "User created successfully!"</returns>
    /// <response code="200">Request is success</response>
    /// <response code="400">Model is null</response>
    /// <response code="500">Server error</response>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var Result = await authenticateService.Register(model);
            //if (Result.Status == StatusEnum.Error)
            //    return StatusCode(StatusCodes.Status500InternalServerError, Response);
            return Ok(Result);
        }

        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        //{
        //    var userExists = await userManager.FindByNameAsync(model.Username);
        //    if (userExists != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = StatusEnum.Error, Message = "User already exists!" });

        //    User user = new User()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Username
        //    };
        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = StatusEnum.Error, Message = "User creation failed! Please check user details and try again." });

        //    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await roleManager.CreateAsync(new Role { Name = UserRoles.Admin });
        //    if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //        await roleManager.CreateAsync(new Role {Name = UserRoles.User });

        //    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await userManager.AddToRoleAsync(user, UserRoles.Admin);
        //    }

        //    return Ok(new Response { Status = StatusEnum.Accept , Message = "User created successfully!" });
        //}

    }
}

 