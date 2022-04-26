using System;

namespace App
{
    public class ImportantCreditLimitStatusProvider : ICreditLimitStatusProvider
    {
        private readonly ICustomerCreditService _customerCreditService;

        public ImportantCreditLimitStatusProvider(ICustomerCreditService customerCreditService)
        {
            _customerCreditService = customerCreditService;
        }

        public CreditStatus GetStatus(string firstname, string surname, DateTime dateOfBirth)
        {
            var status = new CreditStatus();
            status.HasCreditLimit = true;
            var creditLimit = _customerCreditService.GetCreditLimit(firstname, surname, dateOfBirth);
            creditLimit = creditLimit * 2;
            status.CreditLimit = creditLimit;

            return status;
        }
    }
}