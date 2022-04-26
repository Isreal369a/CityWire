using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class ImportantCreditLimitStatusProviderTests
    {
        ImportantCreditLimitStatusProvider sut;
        Mock<ICustomerCreditService> _customerCreditServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _customerCreditServiceMock = new Mock<ICustomerCreditService>();
            sut = new ImportantCreditLimitStatusProvider(_customerCreditServiceMock.Object);
        }

        [TestMethod]
        public void should_set_credit_limit()
        {
            int creditLimit = 5;
            int expectedCreditLimit = 10;
            bool expectedCreditStatus = true;
            _customerCreditServiceMock.Setup(x => x.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(creditLimit);

            CreditStatus status = sut.GetStatus("firname", "surname", DateTime.Now);

            Assert.AreEqual(expectedCreditLimit, status.CreditLimit);
            Assert.AreEqual(expectedCreditStatus, status.HasCreditLimit);
        }


        [TestMethod]
        public void should_get_credit_limit_using_credit_service()
        {
            string expectedFirstname = "firname";
            string expectedSurname = "surname";
            DateTime expectedDate = new DateTime(2022, 04, 24);
            _customerCreditServiceMock.Setup(x => x.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(5);

            CreditStatus status = sut.GetStatus(expectedFirstname, expectedSurname, expectedDate);

            _customerCreditServiceMock.Verify(x => x.GetCreditLimit(expectedFirstname, expectedSurname, expectedDate), Times.Once);
        }
    }
}
