﻿using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.UserModels;
using FinanceManagmentApplication.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> UserManager;
        private DAL.Factories.IUnitOfWorkFactory UnitOfWorkFactory { get; }

        public UserService(IUnitOfWorkFactory unitOfWorkFactory, UserManager<User> userManager)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            UserManager = userManager;
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
                throw new NotEntityFoundException();
            }
        }

        public async Task<Response> Edit(EditUserModel model, ClaimsPrincipal User)
        {
            User user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.Username;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new Response { Status = StatusEnum.Accept, Message = "Редактирование информации о пользователе прошло успешно." };
                }
                else
                {
                    return new Response { Status = StatusEnum.Error, Message = "Такой логин уже существует" };
                }
            }
            else
            {
                return new Response { Status = StatusEnum.Error, Message = "Такого пользователя нет в базе данных" };
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
    }
}
