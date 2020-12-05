using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Units.BL_Tests.RemittanceServiceTests
{
    public class RemittanceEditTest
    {
        public void RemittanceOneScoreEdit()
        {
            var RemittanceRepositoryMock = new Mock<RemittanceRepository>();
            var uow = new Mock<UnitOfWork>();
            var uowFactoryMock = new Mock<IUnitOfWorkFactory>();
            uow.Setup(i => i.Remittances).Returns(RemittanceRepositoryMock.Object);
            uowFactoryMock.Setup(i => i.Create()).Returns(uow.Object);
        }

        
    }
}
