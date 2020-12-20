using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.DAL.Seed
{
    public class DataInitializer
    {
        public static async Task OperationTypeInitialize(IOperationTypeRepository repository)
        {
            var Values = new string[] { "Доход", "Расход", "Перевод" };

            var Types = await repository.GetAllAsync();

            foreach (var Value in Values)
            {
                if (Types.Where(i => i.Name == Value).ToList().Count == 0)
                {
                    await repository.CreateAsync(new OperationType { Name = Value });
                }
            }
        }

        public static async Task PaymentTypeInitialize(IPaymentTypeRepository Repository)
        {
            var Values = new string[] { "Наличный", "Безналичный" };

            var Types = await Repository.GetAllAsync();

            foreach (var Value in Values)
            {
                if (Types.Where(i => i.Name == Value).ToList().Count == 0)
                {
                    await Repository.CreateAsync(new PaymentType { Name = Value });
                }
            }
        }

        public static async Task UserInitialize(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Password!1";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new Role { Name = "admin" });
            }
            if (await roleManager.FindByNameAsync("employee") == null)
            {
                await roleManager.CreateAsync(new Role { Name = "employee" });
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = "Admin", Name="Айдар", Surname="Сырдыбаев"};
                admin.Surname = "Сырдыбаев";
                admin.Name = "Айдар";

                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
                User Employee = new User { Email = "employee@gmail.com", UserName = "employee", Name = "Бактыбек", Surname = "Рогович" };
                IdentityResult result2 = await userManager.CreateAsync(Employee, password);
                if (result2.Succeeded)
                {
                    await userManager.AddToRoleAsync(Employee, "employee");
                }
            }
        }

        public static async Task CounterPartyInitialize(ICounterPartyRepository Repository)
        {

            if (!await Repository.CheckCount())
            {
                await Repository.CreateAsync(new CounterParty { IsCompany = true, Name = "ОсОО Таргет" });
                await Repository.CreateAsync(new CounterParty { IsCompany = false, Name = "Аяна Каракаевна" });
                await Repository.CreateAsync(new CounterParty { IsCompany = false, Name = "Без контрагента" });
            }
        }

        public static async Task ScoreInitialize(IScoreRepository Repository)
        {
            if (!await Repository.CheckCount())
            {
                await Repository.CreateAsync(new Score { Name="KICB", PaymentTypeId = 2, Code = "123123123" , Balance = 10000 });
                await Repository.CreateAsync(new Score { Name="ELSOM", PaymentTypeId = 1, Code = "123123123" , Balance = 10000});
                await Repository.CreateAsync(new Score { Name="DemirBank", PaymentTypeId = 2, Code = "123123123" , Balance = 10000});
            }
        }

        public static async Task ProjectInitialize(IProjectRepository Repository)
        {   

            var Values = new string[] { "Neolabs", "Neobis Studio", "Neobis club", "Прочее" };

            var Projects = await Repository.GetAllAsync();

            foreach (var Value in Values)
            {
                if (Projects.Where(i => i.Name == Value).ToList().Count == 0)
                {
                    await Repository.CreateAsync(new Project { Name = Value });
                }
            }
        }

        public static async Task OperationInitialize(IOperationRepository Repository)
        {
            var IncomeValues = new string[]
                {

                    "Доход от реализации услуг",
                    "Доход от реализации продукции",
                    "Членский взнос за поступление",
                    "Прочие доходы",


                };
            var CostValues = new string[]
                {
                    "Выдача заработной платы сотруднику",
                    "Расходы на офисные принадлежности",
                    "Расходы на коммунальные услуги",
                    "Расходы на покупку оборудования",
                    "Расходы на ремонт оборудования",
                    "Прочие расходы",
                    "Расходы на оплату аренды"
                };

            var TransferValue = new string[]
                {
                    "Внутренние переводы"
                };

            var Operations = await Repository.GetAllAsync();


            foreach (var Value in IncomeValues)
            {
                if (Operations.Where(i => i.Name == Value).ToList().Count == 0)
                {
                    await Repository.CreateAsync(new Operation { Name = Value, OperationTypeId = 1 });
                }
            }

            foreach (var Value in CostValues)
            {
                if (Operations.Where(i => i.Name == Value).ToList().Count == 0)
                {
                    await Repository.CreateAsync(new Operation { Name = Value, OperationTypeId = 2 });
                }
            }

            foreach (var Value in TransferValue)
            {
                if (Operations.Where(i => i.Name == Value).ToList().Count == 0)
                {
                    await Repository.CreateAsync(new Operation { Name = Value, OperationTypeId = 3 });
                }
            }
        }

        public static async Task TransactionInitialize(ITransactionRepository repository)
        {
            Random rnd = new Random();
            if (!await repository.CheckCount())
            {
                foreach (int ProjectId in Enumerable.Range(1, 3))
                {
                    foreach (int Operation in Enumerable.Range(1, 11))
                    {
                        foreach (int i in Enumerable.Range(1, 2))
                        {

                            await repository.CreateAsync(new Transaction
                            {
                                ActionDate = DateTime.Now,
                                Description = "Тестовые данные",
                                Sum = rnd.Next(1, 10000),
                                ProjectId = ProjectId,
                                ScoreId = 3,
                                CounterPartyId = 1,
                                UserId = 1,
                                OperationId = Operation
                            });
                        }
                    }
                }
            }
        }
    }
}
