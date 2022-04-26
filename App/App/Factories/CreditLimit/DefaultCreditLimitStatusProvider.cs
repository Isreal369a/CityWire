using System;

namespace App
{
    public class DefaultCreditLimitStatusProvider : ICreditLimitStatusProvider
    {
        private readonly ICustomerCreditService _customerCreditService;

        public DefaultCreditLimitStatusProvider(ICustomerCreditService customerCreditService)
        {
            _customerCreditService = customerCreditService;
        }

        public CreditStatus GetStatus(string firstname, string surname, DateTime dateOfBirth)
        {
            var status = new CreditStatus();


            status.HasCreditLimit = true;

            var creditLimit = _customerCreditService.GetCreditLimit(firstname, surname, dateOfBirth);
            status.CreditLimit = creditLimit;


            return status;
        }
    }
}