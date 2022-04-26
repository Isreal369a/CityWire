using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using App.DateTimeProvider;
using App.DataAccess;
using App.Validators;

namespace UnitTestProject1
{
   
    [TestClass]
    public class CustomerServiceTests
    {
        CustomerService sut;
        Mock<IDateTimeProvider> dateTimeProviderMock;
        Mock<ICustomerFactory> _customerFactoryMock;        
        Mock<ICustomerDataAccess> _customerDataAccessMock;
        CustomerValidator _customerValidator;

        [TestInitialize]
        public void Setup()
        {
            dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _customerFactoryMock = new Mock<ICustomerFactory>();            
            _customerDataAccessMock = new Mock<ICustomerDataAccess>();
            _customerValidator = new CustomerValidator(dateTimeProviderMock.Object);
            sut = new CustomerService(_customerValidator, _customerFactoryMock.Object, _customerDataAccessMock.Object);
        }

        private Customer BuildCustomer(DateTime dateOfBirth, string firstname = "firstname", string surname = "surname", string email = "email@email.com", int creditLimit = 2000, bool  hasCreditLimit = true, int companyId = 1)
        {
            return new Customer()
            {
                Firstname = firstname,
                Surname = surname,
                EmailAddress = email,
                CreditLimit = creditLimit,
                DateOfBirth = dateOfBirth,
                HasCreditLimit = hasCreditLimit,
                Company = new Company() { Id = companyId, Name = "Company" }
            };

        }

        [TestMethod]
        public void should_add_customer_when_validation_and_creditLimit_is_correct()
        {
            //given            
            var today = new DateTime(2022, 04, 24);
            var dateOfBirth = new DateTime(2001, 04, 24);
            var customer = BuildCustomer(dateOfBirth);
            dateTimeProviderMock.Setup(x => x.DateTimeNow).Returns(today);
            _customerFactoryMock.Setup(x => x.BuildCustomer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(customer);

            bool result = sut.AddCustomer("firstname", "surname", "email@email.com", dateOfBirth, 1);

            _customerDataAccessMock.Verify(x => x.AddCustomer(customer), Times.Once());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void should_return_false_when_firstname_is_empty()
        {
            string firstname = String.Empty;
            
            bool result = sut.AddCustomer(firstname, "surname", "firna@gmail.com", DateTime.Now, 1);            

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void should_return_false_when_firstname_is_null()
        {
            string firstname = null;
            
            bool result = sut.AddCustomer(firstname, "surname", "firna@gmail.com", DateTime.Now, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void should_return_false_when_year_is_correct_and_month_less_current()
        {
            //given
            var today = new DateTime(2022, 03, 24);
            var dateOfBirth = new DateTime(2001, 04, 24);
            dateTimeProviderMock.Setup(x => x.DateTimeNow).Returns(today);

            //when
            bool result = sut.AddCustomer("firname", "surname", "firna@gmail.com", dateOfBirth, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void should_return_false_when_year_and_month_is_correct_and_day_less_current()
        {
            //given
            var today = new DateTime(2022, 04, 23);
            var dateOfBirth = new DateTime(2001, 04, 24);
            dateTimeProviderMock.Setup(x => x.DateTimeNow).Returns(today);

            //when
            bool result = sut.AddCustomer("firname", "surname", "firna@gmail.com", dateOfBirth, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void should_return_false_when_age_less_than_21()
        {
            //given
            var today = new DateTime(2022, 04, 24);
            var dateOfBirth = new DateTime(2022, 04, 23);
            dateTimeProviderMock.Setup(x => x.DateTimeNow).Returns(today);

            //when
            bool result = sut.AddCustomer("firname", "surname", "firna@gmail.com", dateOfBirth, 1);

            Assert.IsFalse(result);
        }

        [DataRow("johnATgmailcom", false)]
        [DataRow("johnATgmailDOTcom", false)]
        [DataRow("", false)]
        [TestMethod]
        public void should_return_false_when_email_is_invalid(string email, bool expected)
        {
            //given                     
            var today = new DateTime(2022, 04, 24);
            var dateOfBirth = new DateTime(2001, 04, 24);
            dateTimeProviderMock.Setup(x => x.DateTimeNow).Returns(today);

            //when
            bool result = sut.AddCustomer("firname", "surname", email, dateOfBirth, 1);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void should_return_false_when_creditLimit_is_less_than_500()
        {
            //given            
            int creditLimit = 400;
            var today = new DateTime(2022, 04, 24);
            var dateOfBirth = new DateTime(2001, 04, 24);
            var customer = BuildCustomer(dateOfBirth, creditLimit: creditLimit);
            dateTimeProviderMock.Setup(x => x.DateTimeNow).Returns(today);
            _customerFactoryMock.Setup(x => x.BuildCustomer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(customer);

            bool result = sut.AddCustomer("firstname", "surname", "email@email.com", dateOfBirth, 1);

            
            Assert.IsFalse(result);
        }

        
       
    }
}
