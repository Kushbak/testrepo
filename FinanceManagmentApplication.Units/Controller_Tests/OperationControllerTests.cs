using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Controllers;
using FinanceManagmentApplication.Models.OperationModels;
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
    public class OperationControllerTests
    {
        [Fact]
        public async Task OperationIndexTest()
        {
            var mock = new Mock<IOperationService>();
            mock.Setup(repo => repo.GetAll()).Returns(GetOperationsIndexModels());
            var controller = new OperationController(mock.Object);

            // Act
            var result = await controller.Index();
            var testResult = await GetOperationsIndexModels();
            // Assert
            var viewResult = Assert.IsType<ActionResult<List<OperationDetailsModel>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<OperationDetailsModel>>(viewResult.Value);
            Assert.Equal(testResult.Count, model.Count());
        }

        private async Task<List<OperationDetailsModel>> GetOperationsIndexModels()
        {
            return new List<OperationDetailsModel>
            {
                new OperationDetailsModel{Id = 1},
                new OperationDetailsModel{Id = 2}
            };
        }
    }
}
