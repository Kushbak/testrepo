using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Controllers;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.WebModels.Wrappers;
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
    public class FinanceActionsControllerTests
    {
        [Fact]
        public async Task CounterPartyIndexTest()
        {
            var mock = new Mock<IFinanceActionService>();
            mock.Setup(repo => repo.FinanceActionPagination(null)).Returns(GetTestFinanceActionsIndexModels());
            var controller = new FinanceActionsController(mock.Object);

            // Act
            var result = await controller.Index(null);
            var testResult = await GetTestFinanceActionsIndexModels();
            // Assert
            var viewResult = Assert.IsType<ActionResult<PagedResponse<List<FinanceActiveIndexModel>>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<FinanceActiveIndexModel>>(viewResult.Value.Data);
            Assert.Equal(testResult.Data.Count, model.Count());
        }

        private async Task<PagedResponse<List<FinanceActiveIndexModel>>> GetTestFinanceActionsIndexModels()
        {
            return new PagedResponse<List<FinanceActiveIndexModel>>( new List<FinanceActiveIndexModel>() { new FinanceActiveIndexModel { Id = 1 }, new FinanceActiveIndexModel { Id = 2 } }, 1, 10);
        }
    }
}
