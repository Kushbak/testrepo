using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.UserModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.WebModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FinanceManagmentApplication.BL.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;
        private readonly IConfiguration _configuration;
        private DAL.Factories.IUnitOfWorkFactory UnitOfWorkFactory { get; }

        public UserService(IUnitOfWorkFactory unitOfWorkFactory, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            UserManager = userManager;
            SignInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<List<UserIndexModel>> GetAll()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Users = await uow.Users.GetAllAsync();
                var Models = new List<UserIndexModel>();
                foreach (var User in Users)
                {

                    var Model = Mapper.Map<UserIndexModel>(User);
                    Models.Add(Model);
                }

                return Models;
            }
        }
        

        public async Task<EditUserModel> GetEditModel(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var User = await uow.Users.GetByIdAsync(Id);
                var Model = Mapper.Map<EditUserModel>(User);
                return Model;
            }
        }

        public async Task<UserDetailsModel> GetDetailsModel(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var User = await uow.Users.GetByIdAsync(Id);
                var Model = Mapper.Map<UserDetailsModel>(User);
                return Model;
            }
        }
        public async Task<UserIndexModel> GetUser(UserIndexModel model, ClaimsPrincipal User)
        {
            User user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var Model = Mapper.Map<UserIndexModel>(user);
                return Model;
            }
            else
            {
                return null;
            }
        }

        public async Task<TokenModel> Edit(EditUserModel model, ClaimsPrincipal User)
        {
            User user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                if(model.Email!=null)
                {
                    user.Email = model.Email;
                }
                if (model.Username != null)
                {
                    user.UserName = model.Username;
                }
                if (model.Surname != null)
                {
                    user.Surname = model.Surname;
                }
                if (model.Name != null)
                {
                    user.Name = model.Name;
                }
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return await GenerateToken(user);
                }
                else
                {
                    throw new Exception("Такой логин уже существует");
                }
            }
            else
            {
                throw new Exception("Такой логин уже существует");
            }
        }
        public async Task<Response> ChangePassword(ChangePasswordUserModel model, ClaimsPrincipal User)
        {
            User user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result =
                    await UserManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return new Response { Status = StatusEnum.Accept, Message = "Редактирование пароля прошло успешно." };
                }
                else
                {
                    return new Response { Status = StatusEnum.Error, Message = "Неверные данные" };
                }
            }
            else
            {
                return new Response { Status = StatusEnum.Error, Message = "Такого пользователя нет в базе данных" };
            }
        }

        private async Task<TokenModel> GenerateToken(User user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);



            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Surname, user.Surname),
                    new Claim(ClaimTypes.NameIdentifier, user.Name),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };


            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new TokenModel
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

        }
    }
}
