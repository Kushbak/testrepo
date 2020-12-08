using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Controllers;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinanceManagmentApplication.Units.Controller_Tests
{
    public class CounterPartyControllerTests
    {
        [Fact]
        public async Task CounterPartyIndexTest()
        {
            var mock = new Mock<ICounterPartyService>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestCounterPartiesIndexModels());
            var controller = new CounterPartyController(mock.Object);

            // Act
            var result = await controller.Index();
            var testResult = await  GetTestCounterPartiesIndexModels();
            // Assert
            var viewResult = Assert.IsType<ActionResult<List<CounterPartyIndexModel>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CounterPartyIndexModel>>(viewResult.Value);
            Assert.Equal( testResult.Count, model.Count());
        }

        private async Task<List<CounterPartyIndexModel>> GetTestCounterPartiesIndexModels()
        {
            return new List<CounterPartyIndexModel>
            {
                new CounterPartyIndexModel{Id = 1 },
                new CounterPartyIndexModel {Id = 2}
            };
        }
    }
    
}
