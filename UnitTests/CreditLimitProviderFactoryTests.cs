using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class CreditLimitProviderFactoryTests
    {
        CreditLimitProviderFactory sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new CreditLimitProviderFactory(null);
        }

        [TestMethod]
        public void should_return_VeryImportantProvider_when_name_is_very_important_client()
        {
            //giveb
            string companyName = "VeryImportantClient";

            //when
            var provider = sut.GetProvider(companyName);

            //then
            Assert.AreEqual(typeof(VeryImportantCreditLimitStatusProvider), provider.GetType());
        }

        [TestMethod]
        public void should_return_ImportantProvider_when_name_is_important_client()
        {
            //giveb
            string companyName = "ImportantClient";

            //when
            var provider = sut.GetProvider(companyName);

            //then
            Assert.AreEqual(typeof(ImportantCreditLimitStatusProvider), provider.GetType());
        }

        [TestMethod]
        public void should_return_DefaultProvider_when_name_is_doesnt_match()
        {
            //giveb
            string companyName = "RandomantClient";

            //when
            var provider = sut.GetProvider(companyName);

            //then
            Assert.AreEqual(typeof(DefaultCreditLimitStatusProvider), provider.GetType());
        }
    }
}
