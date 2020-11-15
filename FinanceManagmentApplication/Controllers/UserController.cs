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

namespace FinanceManagmentApplication.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkFactory UnitOfWorkFactory;

        public UserController( IUnitOfWorkFactory unitOfWorkFactory)
        {   
            UnitOfWorkFactory = unitOfWorkFactory;
          
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationType>>> Get()
        {   
            
            using (var uow = UnitOfWorkFactory.Create())
            {
                return await uow.OperationTypes.GetAllAsync();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit()
        {   

            return Ok();
        }
    }
}
