using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Controllers;
using FinanceManagmentApplication.Models.ProjectModels;
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
    public class ProjectControllerTests
    {
        [Fact]
        public async Task ProjectIndexTest()
        {
            var mock = new Mock<IProjectService>();
            mock.Setup(repo => repo.GetAll()).Returns(GetProjectIndexModels());
            var controller = new ProjectController(mock.Object);

            // Act
            var result = await controller.Get();
            var testResult = await GetProjectIndexModels();
            // Assert
            var viewResult = Assert.IsType<ActionResult<List<ProjectIndexModel>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProjectIndexModel>>(viewResult.Value);
            Assert.Equal(testResult.Count, model.Count());
        }

        private async Task<List<ProjectIndexModel>> GetProjectIndexModels()
        {
            return new List<ProjectIndexModel>()
                { 
                    new ProjectIndexModel {Id = 1},
                    new ProjectIndexModel {Id = 2}
                };
        }
    }
}
