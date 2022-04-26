using App.DateTimeProvider;
using App.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class CustomerValidatorTests
    {
        CustomerValidator sut;
        Mock<IDateTimeProvider> _dateTimeProviderMock;

        [TestInitialize]
        public void Setup()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            sut = new CustomerValidator(_dateTimeProviderMock.Object);
        }

        [TestMethod]
        public void should_return_false_when_email_invalid()
        {
            var email = "johnAThohn";

            bool result = sut.HasValidEmail(email);

            Assert.IsFalse (result);
        }

        [TestMethod]
        public void should_return_true_when_email_invalid()
        {
            var email = "johnAThohn.com";

            bool result = sut.HasValidEmail(email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void should_return_false_when_name_is_invalid()
        {
            var firstname = "";

            bool result = sut.HasValidFirstNameAndSurName(firstname, "surname");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void should_return_true_when_name_is_valid()
        {
            bool result = sut.HasValidFirstNameAndSurName("firstname", "surname");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void should_return_true_when_age_is_valid()
        {
            var today = new DateTime(2022, 04, 24);
            var dateOfBirth = new DateTime(2001, 04, 24);
            _dateTimeProviderMock.Setup(x => x.DateTimeNow).Returns(today);

            bool result = sut.HasValidAge(dateOfBirth);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void should_return_false_when_age_is_invalid()
        {
            var today = new DateTime(2022, 04, 24);
            var dateOfBirth = new DateTime(2021, 04, 24);
            _dateTimeProviderMock.Setup(x => x.DateTimeNow).Returns(today);

            bool result = sut.HasValidAge(dateOfBirth);

            Assert.IsFalse(result);
        }
    }
}
