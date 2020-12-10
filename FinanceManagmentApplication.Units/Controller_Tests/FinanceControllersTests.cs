using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Controllers;
using FinanceManagmentApplication.Models.FinanceModels;
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
    public class FinanceControllersTests
    {
        [Fact]
        public async Task FinanceOperationIndexTest()
        {
            var mock = new Mock<IFinanceService>();
            mock.Setup(repo => repo.GetFinanceInformationToOperations()).Returns(GetFinanceIndexModels());
            var controller = new FinanceController(mock.Object);

            // Act
            var result = await controller.Operations();
            var testResult = await GetFinanceIndexModels();
            // Assert
            var viewResult = Assert.IsType<ActionResult<List<OperationFinanceModel>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<OperationFinanceModel>>(viewResult.Value);
            Assert.Equal(testResult.Count, model.Count());
        }

        private async Task<List<OperationFinanceModel>> GetFinanceIndexModels()
        {
            return new List<OperationFinanceModel>()
            {
                new OperationFinanceModel{ Name = "Test"},
                new OperationFinanceModel{Name = "Test1"}
            };
        }
    }
}