using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class VeryImportantCreditLimitStatusProviderTests
    {
        VeryImportantCreditLimitStatusProvider sut;
        

        [TestInitialize]
        public void Setup()
        {            
            sut = new VeryImportantCreditLimitStatusProvider();
        }

        [TestMethod]
        public void should_set_credit_limit()
        {            
            int expectedCreditLimit = 0;
            bool expectedCreditStatus = false;
            

            CreditStatus status = sut.GetStatus("firname", "surname", DateTime.Now);

            Assert.AreEqual(expectedCreditLimit, status.CreditLimit);
            Assert.AreEqual(expectedCreditStatus, status.HasCreditLimit);
        }

    }
}
