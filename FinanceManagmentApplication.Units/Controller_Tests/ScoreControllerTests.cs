using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Controllers;
using FinanceManagmentApplication.Models.ScoreModel;
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
    public class ScoreControllerTests
    {
        [Fact]
        public async Task ScoreIndexTest()
        {
            var mock = new Mock<IScoreService>();
            mock.Setup(repo => repo.GetAll()).Returns(GetScoreIndexModels());
            var controller = new ScoreController(mock.Object);

            // Act
            var result = await controller.Index();
            var testResult = await GetScoreIndexModels();
            // Assert
            var viewResult = Assert.IsType<ActionResult<List<ScoreIndexModel>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ScoreIndexModel>>(viewResult.Value);
            Assert.Equal(testResult.Count, model.Count());
        }

        private async Task<List<ScoreIndexModel>> GetScoreIndexModels()
        {
            return new List<ScoreIndexModel>()
                { 
                new ScoreIndexModel { Id = 1},
                new ScoreIndexModel {Id = 2}
            };
        }
    }
}
