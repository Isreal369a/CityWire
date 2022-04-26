using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class CustomerFactoryTests
    {
        Mock<ICompanyRepository> _companyRepositoryMock;
        Mock<ICustomerCreditService> _customerCreditServiceMock;
        Mock<ICreditLimitStatusProvider> _creditLimitStatusProviderMock;
        Mock<ICreditLimitProviderFactory> _creditLimitProviderFactoryMock;
        CustomerFactory sut;

        [TestInitialize]
        public void Setup()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _customerCreditServiceMock = new Mock<ICustomerCreditService>();
            _creditLimitStatusProviderMock = new Mock<ICreditLimitStatusProvider>();
            _creditLimitProviderFactoryMock = new Mock<ICreditLimitProviderFactory>();
            sut = new CustomerFactory(_creditLimitProviderFactoryMock.Object, _companyRepositoryMock.Object);
            _creditLimitProviderFactoryMock.Setup(x => x.GetProvider(It.IsAny<string>())).Returns(new DefaultCreditLimitStatusProvider(_customerCreditServiceMock.Object));
        }

        [TestMethod]
        public void should_build_customer()
        {
            //given
            string firstname = "firstname";
            string surname = "surname";
            string email = "email@email";
            DateTime dateOfBirth = DateTime.Now; int companyId = 1;

            var company = new Company { Id = companyId, Name = "companyName" };
            _companyRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(company);
            
            //when
            Customer customer = sut.BuildCustomer(firstname, surname, email, dateOfBirth, companyId);

            //then
            Assert.AreEqual(firstname, customer.Firstname);
            Assert.AreEqual(surname, customer.Surname);
            Assert.AreEqual(email, customer.EmailAddress);
            Assert.AreEqual(dateOfBirth, customer.DateOfBirth);
            Assert.AreEqual(company, customer.Company);
        }

        [TestMethod]
        public void should_build_customer_with_company()
        {
            //given
            string firstname = "firstname"; 
            string surname = "surname"; 
            string email = "email@email"; 
            DateTime dateOfBirth = DateTime.Now; int companyId = 1;

            var company = new Company { Id = companyId, Name = "companyName" };
            _companyRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(company);
          
            //when
            Customer customer = sut.BuildCustomer(firstname, surname, email, dateOfBirth, companyId);

            //then
            Assert.AreEqual(company.Name, customer.Company.Name);
            Assert.AreEqual(company.Id, customer.Company.Id);
        }

        [TestMethod]
        public void should_build_customer_with_creditLimit()
        {
            //given
            string firstname = "firstname";
            string surname = "surname";
            string email = "email@email";
            DateTime dateOfBirth = DateTime.Now; int companyId = 1;            
            _companyRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Company { Id = companyId, Name = "companyName" });
            CreditStatus expectedCreditStatus = new CreditStatus() {  HasCreditLimit = true, CreditLimit = 2 };
            _creditLimitStatusProviderMock.Setup(x => x.GetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(expectedCreditStatus);
            _creditLimitProviderFactoryMock.Setup(x => x.GetProvider(It.IsAny<string>())).Returns(_creditLimitStatusProviderMock.Object);
            //when
            Customer customer = sut.BuildCustomer(firstname, surname, email, dateOfBirth, companyId);

            //then
            Assert.AreEqual(expectedCreditStatus.HasCreditLimit, customer.HasCreditLimit);
            Assert.AreEqual(expectedCreditStatus.CreditLimit, customer.CreditLimit);
        }

       /* [TestMethod]
        public void should_set_credit_limit_status_to_false_when_name_is_VeryImportantClient()
        {
            //given
            string firstname = "firstname";
            string surname = "surname";
            string email = "email@email";
            DateTime dateOfBirth = DateTime.Now; int companyId = 1;
            var expectedCreditLimit = new CreditStatus { HasCreditLimit = false };

            var company = new Company { Id = companyId, Name = "VeryImportantClient" };
            _companyRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(company);
           // _creditLimitStatusProviderMock.Setup(x => x.GetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(expectedCreditLimit);
            //when
            Customer customer = sut.BuildCustomer(firstname, surname, email, dateOfBirth, companyId);

            //then
            Assert.IsFalse(customer.HasCreditLimit);            
        }

        [TestMethod]
        public void should_set_credit_limit_status_to_true_when_name_is_ImportantClient()
        {
            //given
            string companyName = "ImportantClient";           
            _companyRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Company { Id = 1, Name = companyName });
            var expectedCreditLimit = new CreditStatus { HasCreditLimit = true };
          //  _creditLimitStatusProviderMock.Setup(x => x.GetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(expectedCreditLimit);
            //when
            Customer customer = sut.BuildCustomer("firstname", "surname", "email@email", DateTime.Now, 1);

            //then
            Assert.IsTrue(customer.HasCreditLimit);
        }

        [TestMethod]
        public void should_set_credit_limit_value_when_name_is_ImportantClient()
        {
            //given
            string companyName = "ImportantClient";
            int customerCreditLimit = 1;
            int expectedCreditLimit = 2;
            _companyRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Company { Id = 1, Name = companyName });
            _customerCreditServiceMock.Setup(x => x.GetCreditLimit("firstname", "surname", DateTime.Now)).Returns(customerCreditLimit);
            var creditStatus = new CreditStatus { HasCreditLimit = true, CreditLimit = expectedCreditLimit };
           // _creditLimitStatusProviderMock.Setup(x => x.GetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(creditStatus);
            //when
            Customer customer = sut.BuildCustomer("firstname", "surname", "email@email", DateTime.Now, 1);

            //then
            Assert.AreEqual(expectedCreditLimit, customer.CreditLimit);
        }

        [TestMethod]
        public void should_set_credit_limit_value_when_name_is_random()
        {
            //given        
            int customerCreditLimit = 1;
            int expectedCreditLimit = 1;
            _companyRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Company { Id = 1, Name = "Random" });
            _customerCreditServiceMock.Setup(x => x.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(customerCreditLimit);
            var creditStatus = new CreditStatus { HasCreditLimit = true, CreditLimit = expectedCreditLimit };
          //  _creditLimitStatusProviderMock.Setup(x => x.GetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(creditStatus);

            //when
            Customer customer = sut.BuildCustomer("firstname", "surname", "email@email", DateTime.Now, 1);

            //then
            Assert.AreEqual(expectedCreditLimit, customer.CreditLimit);
        }
       */
    }
}
