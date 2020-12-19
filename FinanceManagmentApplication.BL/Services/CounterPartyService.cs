using AutoMapper;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.WebModels;
using FinanceManagmentApplication.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.ErrorModels;

namespace FinanceManagmentApplication.BL.Services
{
    public class CounterPartyService: ICounterPartyService
    {   
        private UserManager<User> UserManager { get; }
        private IUnitOfWorkFactory UnitOfWorkFactory { get; }

        public CounterPartyService(IUnitOfWorkFactory unitOfWorkFactory, UserManager<User> userManager)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            UserManager = userManager;
        }

        public async Task<Response> Create(CounterPartyCreateModel model)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                if (model == null)
                {
                    return new Response { Status = StatusEnum.Error, Message = "Ничего не отправлено на сервер. Повторите попытку" };
                }

                if (model.UserId.HasValue)
                {
                    var User = UserManager.FindByIdAsync(model.UserId.Value.ToString());
                    if (User != null)
                        return new Response { Status = StatusEnum.Error, Message = "Нет пользователя в системе с таким Id" };

                }
                var CounterParty = Mapper.Map<CounterParty>(model);

                await uow.CounterParties.CreateAsync(CounterParty);

                return new Response { Status = StatusEnum.Accept };
            }
        }

        public async Task<CounterPartyCreateModel> GetCreateModel()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Model = new CounterPartyCreateModel();
                Model.Users = Mapper.Map<List<UserIndexModel>>(UserManager.Users.ToList());
                return Model;
            }
        }

        public async Task<Response> Edit(CounterPartyEditModel model)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                
                if (model == null)
                {
                    return new Response { Status = StatusEnum.Error, Message = "Ничего не отправлено на сервер. Повторите попытку" };
                }

                if (!uow.CounterParties.Check(model.Id))
                {
                    return new Response { Status = StatusEnum.Error, Message = "Такого контрагента в базе нет" };
                }

                if (model.UserId.HasValue)
                {
                    var User = UserManager.FindByIdAsync(model.UserId.Value.ToString());
                    if (User != null)
                        return new Response { Status = StatusEnum.Error, Message = "Нет пользователя в системе с таким Id" };
                }

                var CounterParty = Mapper.Map<CounterParty>(model);
                await uow.CounterParties.UpdateAsync(CounterParty);

                return new Response { Status = StatusEnum.Accept };
            }
        }

        public async Task<CounterPartyEditModel> GetEditModel(int id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var CounterParty = await uow.CounterParties.GetByIdAsync(id);
                if (CounterParty == null)
                {
                    throw new NotEntityFoundException();
                }
                var Model = Mapper.Map<CounterPartyEditModel>(CounterParty);
                Model.Users = Mapper.Map<List<UserIndexModel>>(UserManager.Users.ToList());
                return Model;
            }
        }

        public async Task<Response> Delete(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var CounterParty = await uow.CounterParties.GetByIdAsync(Id);
                CounterParty.IsDelete = true;
                await uow.CounterParties.UpdateAsync(CounterParty);

                return new Response { Status = StatusEnum.Error };
            }
        }

        public async Task<List<CounterPartyIndexModel>> GetAll()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var CounterParties = await uow.CounterParties.GetAllAsync();
                return Mapper.Map<List<CounterPartyIndexModel>>(CounterParties);
            }
        }

        
    }
}
