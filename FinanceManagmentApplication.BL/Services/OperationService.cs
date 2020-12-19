using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Models.WebModels;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.OperationTypeModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.ErrorModels;

namespace FinanceManagmentApplication.BL.Services
{
    public class OperationService : IOperationService
    {

        private IUnitOfWorkFactory UnitOfWorkFactory { get; }
        public OperationService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
        }


        public async Task<List<OperationDetailsModel>> GetAll()
        {

            using (var Uow = UnitOfWorkFactory.Create())
            {
                var Operations = await Uow.Operations.GetAllAsync();
                return Mapper.Map<List<OperationDetailsModel>>(Operations);
            }
        }

        public async Task<Response> Create(OperationCreateModel model)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Operation = Mapper.Map<Operation>(model);
                if (model == null)
                    return new Response { Status = StatusEnum.Error, Message = "ничего на сервер не отправлено" };

                if (!uow.OperationTypes.Check(model.OperationTypeId))
                    return new Response { Status = StatusEnum.Error, Message = "Нет такого типа операций" };
                await uow.Operations.CreateAsync(Operation);
                return new Response { Status = StatusEnum.Accept, Message = "Запрос прошел успешно" };

            }
        }
        public async Task<OperationCreateModel> GetCreateModel()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Operation = new OperationCreateModel();
                Operation.operationTypes = Mapper.Map<List<OperationTypeIndexModel>>(await uow.OperationTypes.GetAllAsync());
                return Operation;

            }
        }
        public async Task<Response> Edit(OperationEditModel model)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Operation = Mapper.Map<Operation>(model);
                if (!uow.Operations.Check(model.Id))
                {
                    return new Response { Status = StatusEnum.Error, Message = "Такой операции в базе нет" };
                }
                if (model == null)
                    return new Response { Status = StatusEnum.Error, Message = "ничего на сервер не отправлено" };
                if (!uow.OperationTypes.Check(model.OperationTypeId))
                    return new Response { Status = StatusEnum.Error, Message = "Нет такого типа операций" };
                await uow.Operations.UpdateAsync(Operation);
                return new Response { Status = StatusEnum.Accept, Message = "Запрос прошел успешно" };

            }
        }
        public async Task<OperationEditModel> GetEditModel(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Operation = await uow.Operations.GetByIdAsync(Id);
                if (Operation == null)
                {
                    throw new NotEntityFoundException();
                }
                var Model = Mapper.Map<OperationEditModel>(Operation);
                Model.operationTypes = Mapper.Map<List<OperationTypeIndexModel>>(await uow.OperationTypes.GetAllAsync());
                return Model;

            }
        }

        public async Task<Response> Delete(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Operation = await uow.Operations.GetByIdAsync(Id);
                if (Operation == null)
                {
                    return new Response { Status = StatusEnum.Error, Message = "Нет такого типа операций" };
                }
                Operation.IsDelete = true;
                await uow.Operations.UpdateAsync(Operation);
                return new Response { Status = StatusEnum.Accept, Message = "Запрос прошел успешно" };
            }

        }
    }
        }
        
